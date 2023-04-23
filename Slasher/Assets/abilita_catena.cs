using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_catena : MonoBehaviour
{
    private float rotationSpeed=120;
    public float dmg=0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void setta_livello(int livello){
        dmg=0.1f+(0.025f*livello);
        //rotationSpeed=120+(10*livello);
    }
}
