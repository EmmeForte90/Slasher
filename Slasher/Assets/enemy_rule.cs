using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_rule : MonoBehaviour
{
    public string id_nemico;
    public hero_rule hero_rule;
    public float danno=0.01f;

    private Rigidbody rb;
    public Camera cam_r;
    public Transform cam;
    public Transform hero;
    public float velocita_movimento=1;
    private float x_start_hero_screen;

    float input_horizontal;
    bool bool_dir_dx=true;
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Vector3 screenPos_2 = cam_r.WorldToScreenPoint(hero.position);
        x_start_hero_screen=screenPos_2.x;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = hero.position - transform.position;

        transform.LookAt(cam.transform.position);   //il pupo mostra sempre la stessa faccia TELECAMERA
        //transform.LookAt(hero.transform.position);   //il pupo mostra sempre la stessa faccia HERO
        Flip();
    }
    void FixedUpdate(){
        rb.MovePosition(transform.position+moveDir*0.01f*velocita_movimento);
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
            case "eroe":{hero_rule.danneggia_eroe(danno);break;}
        }
    }

    void OnTriggerStay(Collider collision){
        switch (collision.gameObject.name){
            case "scia_di_fuoco":{danneggia_nemico("scia_di_fuoco",hero_rule.lista_danni_abilita["scia_di_fuoco"]);break;}
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
            case "laser_collider_trigger":{danneggia_nemico("laser",hero_rule.lista_danni_abilita["shuriken"]);break;}
            case "sfera_orbitale":{danneggia_nemico("sfera_orbitale",hero_rule.lista_danni_abilita["sfera_orbitale"]);break;}
        }
    }

    public void danneggia_nemico(string tipo,float danni){
        print ("stò danneggiando il nemico di "+danni+" del tipo "+tipo);
    }
}
