using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_camera : MonoBehaviour
{
    public Vector2 turn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        //transform.localRotation = Quaternion.Euler(-90,turn.x,0);
        //transform.localRotation = Quaternion.Euler(-turn.y, turn.x,0);
    }
}
