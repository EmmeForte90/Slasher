using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class enemy_rule : MonoBehaviour
{
    public GameObject pf_vfx_destroy;

    public SkeletonAnimation skeletonAnimation;

    public string id_nemico;
    public hero_rule hero_rule;
    public float danno=1f;

    private Rigidbody rb;
    public Camera cam_r;
    public Transform cam;
    public Transform hero;
    public float velocita_movimento=1;
    private float x_start_hero_screen;

    private float vitalita;
    public float vitalita_max=1;

    float input_horizontal;
    bool bool_dir_dx=true;
    Vector3 moveDir;

    public GameObject obj_exp;

    private bool bool_morto=false;

    // Start is called before the first frame update
    void Start(){
        vitalita=vitalita_max;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Vector3 screenPos_2 = cam_r.WorldToScreenPoint(hero.position);
        x_start_hero_screen=screenPos_2.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (bool_morto){return;}
        moveDir = (hero.position - transform.position).normalized;

        transform.LookAt(cam.transform.position);   //il pupo mostra sempre la stessa faccia TELECAMERA
        //transform.LookAt(hero.transform.position);   //il pupo mostra sempre la stessa faccia HERO
        Flip();
    }
    void FixedUpdate(){
        if (bool_morto){return;}

        //rb.MovePosition(transform.position+moveDir*0.01f*velocita_movimento);
        if (hero_rule.tempo_freeze==0){
            rb.MovePosition(transform.position+moveDir * velocita_movimento * Time.deltaTime);
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
                hero_rule.check_danneggia_eroe(danno,"nemici","");
                //hero_rule.danneggia_eroe(danno);
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
                print (-rb.velocity);
                danneggia_nemico("scudo",hero_rule.lista_danni_abilita["scudo"]);
                //rb.AddForce(-Vector3.forward * 1000 * Time.deltaTime,ForceMode.Impulse);
                //rb.AddForce(new Vector3 (-rb.velocity.x, 0, - rb.velocity.z)*300*Time.deltaTime,ForceMode.Impulse);
                break;
            }
        }
    }

    public void danneggia_nemico(string tipo,float danni){
        if (bool_morto){return;}
        print ("stò danneggiando il nemico di "+danni+" del tipo "+tipo);
        switch (tipo){
            default:{//viene danneggiato dall'eroe
                if (hero_rule.lista_abilita_passive["danno"]>0){
                    danni+=(danni*hero_rule.lista_abilita_passive["danno"]*5/100);
                }
                break;
            }
        }
        vitalita-=danni;

        StartCoroutine(anim_dmg_nemico());

        if (vitalita<=0){
            attiva_morte_nemico();
        }
    }

    public void attiva_morte_nemico(){
        bool_morto=true;
        GameObject go_temp;
        go_temp=Instantiate(obj_exp);
        go_temp.transform.position=new Vector3(transform.position.x,1,transform.position.z);
        StartCoroutine(anim_morte_nemico());

    }

    private IEnumerator anim_morte_nemico(){
        gameObject.SetActive(false);
        GameObject go_temp_2;
        go_temp_2=Instantiate(pf_vfx_destroy);
        go_temp_2.transform.position=new Vector3(transform.position.x,1,transform.position.z);
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
