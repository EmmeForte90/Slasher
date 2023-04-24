using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_comuni : MonoBehaviour
{
    //blocco legato ai nemici
    //public Dictionary<string, float> lista_danni_nemici = new Dictionary<string, float>();

    //blocco legato alle abilita
    public Dictionary<string, float> lista_abilita_cooldown = new Dictionary<string, float>();
    public Dictionary<string, float> lista_abilita_durata = new Dictionary<string, float>();

    void Start()
    {
        lista_abilita_cooldown.Add("catena",10);
        lista_abilita_cooldown.Add("shuriken",5);

        lista_abilita_durata.Add("catena",3);
        lista_abilita_durata.Add("shuriken",1);



        //lista_danni_nemici.Add("",0.1f);
        //lista_danni_nemici.Add("Homunculus2",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
