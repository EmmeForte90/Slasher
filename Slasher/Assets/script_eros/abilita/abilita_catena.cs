using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_catena : MonoBehaviour
{
    private float rotationSpeed=120;
    public float dmg=0.1f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void setta_livello(int livello){
        dmg=1+(0.25f*livello);
        rotationSpeed=120+(10*livello);
        transform.localScale=new Vector3(1+(0.1f*livello),1,1);
    }
}
