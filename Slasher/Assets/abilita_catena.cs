using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_catena : MonoBehaviour
{
    float rotationSpeed=120;
    Vector3 currentEulerAngles;
    float z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
