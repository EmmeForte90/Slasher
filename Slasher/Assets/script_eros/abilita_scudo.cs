using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_scudo : MonoBehaviour
{
    public float dmg=1f;
    public int durata=3;
    public Transform hero;
    public float per_protezione=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attiva_scudo(){
        gameObject.SetActive(true);
    }

    public void disattiva_scudo(){
        gameObject.SetActive(false);
    }

    public void setta_livello(int livello){
        durata=3+(1*livello);
        dmg=(0.05f*livello);
        per_protezione=(5*livello);
        //rotationSpeed=120+(10*livello);
    }
}
