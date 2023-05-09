using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_scia_di_fuoco : MonoBehaviour
{
    public float dmg=0.1f;
    public int durata=3;
    public bool bool_attiva=false;
    public GameObject GO_scia_singola;
    public Transform hero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bool_attiva){
            GameObject go_temp;
            go_temp=Instantiate(GO_scia_singola);
            go_temp.name="scia_di_fuoco";
            go_temp.transform.localPosition = new Vector3(hero.position.x, hero.position.y, hero.position.z);
            go_temp.SetActive(true);
        }
    }

    public void setta_livello(int livello){
        durata=3+(1*livello);
        dmg=0.1f+(0.025f*livello);
        //rotationSpeed=120+(10*livello);
    }
}