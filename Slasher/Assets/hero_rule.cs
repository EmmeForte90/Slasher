using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_rule : MonoBehaviour
{
    //private Rigidbody rb;
    //public float velocita_movimento=1;
    //private float dirX, dirZ;

    public float velocita_movimento=10;

    public Transform camPivot;
    float heading=0;
    public Transform cam;
    Vector2 input;

    float input_horizontal;
    bool bool_dir_dx=true;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //dirX= Input.GetAxis("Horizontal")*velocita_movimento;
        //dirZ= Input.GetAxis("Vertical")*velocita_movimento;

        heading += Input.GetAxis("Mouse X")*Time.deltaTime*180;
        camPivot.rotation=Quaternion.Euler(0,heading,0);

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input=Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;

        camF.y=0;
        camR.y=0;
        camF=camF.normalized;
        camR=camR.normalized;

        transform.position += (camF*input.y + camR*input.x)*Time.deltaTime*velocita_movimento;

        cam.transform.LookAt(transform.position);   //telecamera inquadra semple il pupo
        transform.LookAt(cam.transform.position);   //il pupo mostra sempre la stessa faccia

        input_horizontal = Input.GetAxisRaw("Horizontal");

        Flip();
    }

    void FixedUpdate(){
        //rb.velocity = new Vector3(dirX,rb.velocity.y,dirZ);
    }

    private void Flip()
    {
        if (bool_dir_dx && input_horizontal > 0f || !bool_dir_dx && input_horizontal < 0f)
        {
            bool_dir_dx = !bool_dir_dx;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
