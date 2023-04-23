using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_rule : MonoBehaviour
{
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
}
