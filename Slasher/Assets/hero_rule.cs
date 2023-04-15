using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero_rule : MonoBehaviour
{
    private Rigidbody rb;

    public float velocita_movimento=1;

    public Transform camPivot;
    float heading=0;
    public Transform cam;
    Vector2 input;
    public float range_ray=10;

    float input_horizontal;
    bool bool_dir_dx=true;

    Vector3 camF,camR,moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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
        transform.LookAt(cam.transform.position);   //il pupo mostra sempre la stessa faccia

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
