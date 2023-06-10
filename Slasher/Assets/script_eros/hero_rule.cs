using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_rule : MonoBehaviour
{
    public info_comuni info_comuni;

    private Dictionary<string, GameObject> lista_GO_abilita = new Dictionary<string, GameObject>();
    public GameObject cont_lista_abilita;
    public Dictionary<string, int> lista_abilita_personaggio = new Dictionary<string, int>();   //abilità, livello
    public Dictionary<string, float> lista_danni_abilita = new Dictionary<string, float>();   //abilità, danno

    private Rigidbody rb;

    public float velocita_movimento=1;

    public Transform camPivot;
    float heading=0;
    public Transform cam;
    Vector2 input;
    public Transform img_hero;

    float input_horizontal;
    bool bool_dir_dx=true;

    Vector3 camF,camR,moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        foreach (Transform child in cont_lista_abilita.transform) {
            lista_GO_abilita.Add(child.name,child.gameObject);
            lista_danni_abilita.Add(child.name,0f);
        }

        raccogli_info_file();   //funzione chiamata in verità dalle funzioni XML in futuro (cioè da mettere sulla funzione che raccogli da xml)

        foreach(KeyValuePair<string,int> attachStat in lista_abilita_personaggio){
            aggiorna_abilita_livello(attachStat.Key,1);
            StartCoroutine(attiva_abilita_coroutine(attachStat.Key));
        }
    }

    // Update is called once per frame
    void Update()
    {

        heading += Input.GetAxis("Mouse X")*Time.deltaTime*120;
        camPivot.rotation=Quaternion.Euler(0,heading,0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input=Vector2.ClampMagnitude(input, 1);

        camF = cam.forward;
        camR = cam.right;

        camF.y=0;
        camR.y=0;
        camF=camF.normalized;
        camR=camR.normalized;

        moveDir=(camR*input.x+camF*input.y);

        //Vector3 targetMovePosition = transform.position + moveDir*Time.deltaTime;
        //transform.position=targetMovePosition;
        

        //transform.position += moveDir*Time.deltaTime*velocita_movimento;
        //print (transform.position+" - "+camR*input.x+" - "+camF*input.y+" - "+(camR*input.x+camF*input.y));

        //cam.transform.LookAt(transform.position);   //telecamera inquadra sempre il pupo
        img_hero.transform.LookAt(cam.transform.position);   //il pupo mostra sempre la stessa faccia

        //funzione relativa al flip
        input_horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
    }

    void FixedUpdate(){
        rb.MovePosition(transform.position+moveDir*0.1f*velocita_movimento);
        //rb.AddForce(moveDir.normalized * velocita_movimento * 10f, ForceMode.Force);
        //rb.velocity = new Vector3(dirX,rb.velocity.y,dirZ);
        //rb.position += (camF*input.y + camR*input.x)*Time.deltaTime*velocita_movimento;
    }

    /*
    void OnCollisionStay(Collision collision){
        print ("eroe: collido con "+collision.gameObject.name+" ("+collision.gameObject.tag+")");
        if (collision.gameObject.tag=="nemico"){
            danneggia_personaggio(info_comuni.lista_danni_nemici[])
        }
    }
    */

    public void danneggia_eroe(float danni){
        //print ("stò danneggiando l'eroe di "+danni);
    }

    private void Flip()
    {
        if (bool_dir_dx && input_horizontal > 0f || !bool_dir_dx && input_horizontal < 0f)
        {
            bool_dir_dx = !bool_dir_dx;
            Vector3 localScale = img_hero.localScale;
            localScale.x *= -1f;
            img_hero.localScale = localScale;
        }
    }

    public void raccogli_info_file(){
        //lista_abilita_personaggio.Add("catena",1);
        //lista_abilita_personaggio.Add("shuriken",1);
        //lista_abilita_personaggio.Add("laser",1);
        //lista_abilita_personaggio.Add("sfera_orbitale",1);
        //lista_abilita_personaggio.Add("scia_di_fuoco",1);
        //lista_abilita_personaggio.Add("boccetta_di_acido",1);
        //lista_abilita_personaggio.Add("meteore",1);
        lista_abilita_personaggio.Add("scudo",1);
    }

    private IEnumerator attiva_abilita_coroutine(string abilita){
        yield return new WaitForSeconds(info_comuni.lista_abilita_cooldown[abilita]);
        attiva_abilita(abilita);
    }

    private void attiva_abilita(string abilita){
        //print ("attivo l'abilita "+abilita);
        switch (abilita){
            case "shuriken":{
                lista_GO_abilita[abilita].GetComponent<abilita_shuriken>().lancia_shuriken();
                break;
            }
            case "laser":{
                lista_GO_abilita[abilita].transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                lista_GO_abilita[abilita].SetActive(true);
                break;
            }
            case "sfera_orbitale":
            case "catena":{
                lista_GO_abilita[abilita].SetActive(true);
                break;
            }
            case "scia_di_fuoco":{
                lista_GO_abilita[abilita].GetComponent<abilita_scia_di_fuoco>().bool_attiva=true;
                break;
            }
            case "boccetta_di_acido":{
                lista_GO_abilita[abilita].GetComponent<abilita_boccetta_di_acido>().lancia_boccetta();
                break;
            }
            case "meteore":{
                lista_GO_abilita[abilita].GetComponent<abilita_meteore>().lancia_meteora();
                break;
            }
            case "scudo":{
                lista_GO_abilita[abilita].GetComponent<abilita_scudo>().attiva_scudo();
                break;
            }
        }
        StartCoroutine(disattiva_abilita(abilita));
    }

    private IEnumerator disattiva_abilita(string abilita){
        yield return new WaitForSeconds(info_comuni.lista_abilita_durata[abilita]);
        //print ("disattivo l'abilita "+abilita);
        switch (abilita){
            case "scia_di_fuoco":{
                lista_GO_abilita[abilita].GetComponent<abilita_scia_di_fuoco>().bool_attiva=false;
                break;
            }
            case "meteore":
            case "boccetta_di_acido":
            case "shuriken":{break;}    //non và disattivata
            case "sfera_orbitale":
            case "catena":{
                lista_GO_abilita[abilita].SetActive(false);break;
            }
            case "scudo":{
                lista_GO_abilita[abilita].GetComponent<abilita_scudo>().disattiva_scudo();
                break;
            }
        }
        StartCoroutine(attiva_abilita_coroutine(abilita));
    }

    private void aggiorna_abilita_livello(string abilita, int livello){
        switch (abilita){
            case "catena":{
                lista_GO_abilita[abilita].GetComponent<abilita_catena>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_catena>().dmg;
                break;
            }
            case "shuriken":{
                lista_GO_abilita[abilita].GetComponent<abilita_shuriken>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_shuriken>().dmg;
                break;
            }
            case "laser":{
                lista_GO_abilita[abilita].GetComponent<abilita_laser>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_laser>().dmg;
                break;
            }
            case "sfera_orbitale":{
                lista_GO_abilita[abilita].GetComponent<abilita_sfera_orbitale>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_sfera_orbitale>().dmg;
                break;
            }
            case "scia_di_fuoco":{
                lista_GO_abilita[abilita].GetComponent<abilita_scia_di_fuoco>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_scia_di_fuoco>().dmg;
                break;
            }
            case "boccetta_di_acido":{
                lista_GO_abilita[abilita].GetComponent<abilita_boccetta_di_acido>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_boccetta_di_acido>().dmg;
                break;
            }
            case "meteore":{
                lista_GO_abilita[abilita].GetComponent<abilita_meteore>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_meteore>().dmg;
                break;
            }
            case "scudo":{
                lista_GO_abilita[abilita].GetComponent<abilita_scudo>().setta_livello(livello);
                lista_danni_abilita[abilita]=lista_GO_abilita[abilita].GetComponent<abilita_scudo>().dmg;
                break;
            }
        }
    }
}
