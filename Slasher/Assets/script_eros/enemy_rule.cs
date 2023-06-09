using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class enemy_rule : MonoBehaviour
{
    public effetti effetti;

    private float last_y=0;
    private float max_difference_y=50;

    public MeshRenderer mesh;

    private bool bool_colpibile=true;

    public bool bool_fantasma=false;

    public GameObject pf_vfx_destroy;

    public SkeletonAnimation skeletonAnimation;

    public hero_rule hero_rule;
    public float danno=1f;
    public float velocita_movimento=1;
    public float vitalita_max=1;

    private Rigidbody rb;
    public Camera cam_r;
    public Transform cam;
    public Transform hero;
    private float x_start_hero_screen;

    private float vitalita;

    float input_horizontal;
    bool bool_dir_dx=true;
    Vector3 moveDir;

    public GameObject obj_exp;

    private bool bool_morto=false;

    // Start is called before the first frame update
    void Start(){
        vitalita=vitalita_max;
        if (!bool_fantasma){
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        Vector3 screenPos_2 = cam_r.WorldToScreenPoint(hero.position);
        x_start_hero_screen=screenPos_2.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (bool_morto){return;}
        if (!bool_fantasma){
            moveDir = (hero.position - transform.position).normalized;
        }

        transform.LookAt(cam.transform.position);   //il pupo mostra sempre la stessa faccia TELECAMERA
        //transform.LookAt(hero.transform.position);   //il pupo mostra sempre la stessa faccia HERO
        Flip();
    }
    void FixedUpdate(){
        if (bool_morto){return;}

        if (hero_rule.tempo_freeze==0){
            if (!bool_fantasma){
                if (gameObject.transform.position.y>50){
                    rb.velocity = Vector3.zero;
                }
                rb.MovePosition(transform.position+moveDir * velocita_movimento * Time.deltaTime);
            } else {
                transform.position = Vector3.MoveTowards(transform.position, hero.position, (velocita_movimento*0.02f));
            }
        }
    }

    private void Flip()
    {
        Vector3 screenPos = cam_r.WorldToScreenPoint(transform.position);
        if (screenPos.x>x_start_hero_screen){
            input_horizontal=1;
        } else {input_horizontal=-1;}

        if (bool_dir_dx && input_horizontal > 0f || !bool_dir_dx && input_horizontal < 0f)
        {
            bool_dir_dx = !bool_dir_dx;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionStay(Collision collision){
        //print ("nemico: collido con "+collision.gameObject.name+" ("+collision.gameObject.tag+")");
        switch (collision.gameObject.name){
            case "Eroe":{
                if (hero_rule.tempo_invincibilita<=0){
                    hero_rule.check_danneggia_eroe(danno,"nemici","");
                } else {
                    danneggia_nemico("invulnerabilita_eroe",0.5f);
                }
                break;
            }
        }
    }

    void OnTriggerStay(Collider collision){
        switch (collision.gameObject.name){
            case "scia_di_fuoco":{danneggia_nemico("scia_di_fuoco",hero_rule.lista_danni_abilita["scia_di_fuoco"]);break;}
            case "pozza_acido":{danneggia_nemico("pozza_acido",hero_rule.lista_danni_abilita["boccetta_di_acido"]);break;}
        }
    }

    /*
    void OnCollisionEnter(Collision collision){
        print ("nemico: entro in collisione con "+collision.gameObject.name+" ("+collision.gameObject.tag+")");
    }
    */

    void OnTriggerEnter(Collider collision){
        //print ("nemico: entro in collisione trigger con "+collision.gameObject.name+" ("+collision.gameObject.tag+")");
        switch (collision.gameObject.name){
            case "catena_collider_trigger":{danneggia_nemico("catena",hero_rule.lista_danni_abilita["catena"]);break;}
            case "shuriken":{danneggia_nemico("shuriken",hero_rule.lista_danni_abilita["shuriken"]);break;}
            case "laser_collider_trigger":{danneggia_nemico("laser",hero_rule.lista_danni_abilita["laser"]);break;}
            case "sfera_orbitale":{danneggia_nemico("sfera_orbitale",hero_rule.lista_danni_abilita["sfera_orbitale"]);break;}
            case "esplosione_meteora":{danneggia_nemico("meteora",hero_rule.lista_danni_abilita["meteore"]);break;}
            case "scudo":{
                //if (!bool_fantasma){print (-rb.velocity);}
                danneggia_nemico("scudo",hero_rule.lista_danni_abilita["scudo"]);
                //rb.AddForce(-Vector3.forward * 1000 * Time.deltaTime,ForceMode.Impulse);
                //rb.AddForce(new Vector3 (-rb.velocity.x, 0, - rb.velocity.z)*300*Time.deltaTime,ForceMode.Impulse);
                break;
            }
        }
    }

    public void danneggia_nemico(string tipo,float danni){
        if (bool_morto){return;}
        if (!bool_colpibile){return;}
        //print ("stò danneggiando il nemico di "+danni+" del tipo "+tipo);

        effetti.effetto_hit_rosso(transform.position.x,transform.position.y,transform.position.z);

        switch (tipo){
            default:{//viene danneggiato dall'eroe
                if (hero_rule.lista_abilita_passive["danno"]>0){
                    danni+=(danni*hero_rule.lista_abilita_passive["danno"]*5/100);
                }
                break;
            }
        }
        vitalita-=danni;

        //StartCoroutine(anim_dmg_nemico());

        bool_colpibile=false;
        StartCoroutine(ritorna_colpibile_coroutine());

        if (vitalita<=0){
            attiva_morte_nemico();
        }
    }

    private IEnumerator ritorna_colpibile_coroutine(){
        yield return new WaitForSeconds(0.2f);
        bool_colpibile=true;
    }

    public void attiva_morte_nemico(){
        if (bool_morto){return;}
        bool_morto=true;
        GameObject go_temp;
        go_temp=Instantiate(obj_exp);
        go_temp.transform.position=new Vector3(transform.position.x,1,transform.position.z);
        mesh.enabled=false;
        GameObject go_temp_2;
        go_temp_2=Instantiate(pf_vfx_destroy);
        go_temp_2.transform.position=new Vector3(transform.position.x,1,transform.position.z);
        StartCoroutine(distruggi_gameobject());
    }

    private IEnumerator distruggi_gameobject(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private IEnumerator anim_dmg_nemico(){
        Color colore_rosso=new Color(255,0,0);
        skeletonAnimation.Skeleton.SetColor(colore_rosso);
        yield return new WaitForSeconds(0.1f);
        Color colore_bianco=new Color(255,255,255);
        skeletonAnimation.Skeleton.SetColor(colore_bianco);
    }
}
