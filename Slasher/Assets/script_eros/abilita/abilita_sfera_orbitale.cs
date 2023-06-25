using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_sfera_orbitale : MonoBehaviour
{
    public float dmg=0.1f;
    public Transform hero;
    private float rotation_speed=120;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(hero.position, Vector3.up, rotation_speed * Time.deltaTime);    
    }

    public void setta_livello(int livello){
        dmg=0.1f+(0.025f*livello);
        //rotation_speed=120+(10*livello);
    }
}
