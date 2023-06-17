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

    public Dictionary<string, string> lista_abilita_nome = new Dictionary<string, string>();
    public Dictionary<string, string> lista_abilita_descrizione_generica = new Dictionary<string, string>();

    void Start()
    {
        lista_abilita_nome.Add("catena","Catena");
        lista_abilita_nome.Add("shuriken","Shuriken");
        lista_abilita_nome.Add("laser","Laser");
        lista_abilita_nome.Add("sfera_orbitale","Sfera Orbitale");
        lista_abilita_nome.Add("scia_di_fuoco","Scia di fuoco");
        lista_abilita_nome.Add("boccetta_di_acido","Boccetta d'acido");
        lista_abilita_nome.Add("meteore","Meteroe");
        lista_abilita_nome.Add("scudo","Scudo");

        lista_abilita_descrizione_generica.Add("catena","Descrizione di Catena");
        lista_abilita_descrizione_generica.Add("shuriken","Descrizione di Shuriken");
        lista_abilita_descrizione_generica.Add("laser","Descrizione di Laser");
        lista_abilita_descrizione_generica.Add("sfera_orbitale","Descrizione di Sfera Orbitale");
        lista_abilita_descrizione_generica.Add("scia_di_fuoco","Descrizione di Scia di fuoco");
        lista_abilita_descrizione_generica.Add("boccetta_di_acido","Descrizione di Boccetta d'acido");
        lista_abilita_descrizione_generica.Add("meteore","Descrizione di Meteroe");
        lista_abilita_descrizione_generica.Add("scudo","Descrizione di Scudo");

        lista_abilita_cooldown.Add("catena",5);
        lista_abilita_cooldown.Add("shuriken",5);
        lista_abilita_cooldown.Add("laser",3);
        lista_abilita_cooldown.Add("sfera_orbitale",3);
        lista_abilita_cooldown.Add("scia_di_fuoco",2);
        lista_abilita_cooldown.Add("boccetta_di_acido",2);
        lista_abilita_cooldown.Add("meteore",2);
        lista_abilita_cooldown.Add("scudo",2);

        lista_abilita_durata.Add("catena",3);
        lista_abilita_durata.Add("shuriken",1);
        lista_abilita_durata.Add("laser",0.1f);
        lista_abilita_durata.Add("sfera_orbitale",3);
        lista_abilita_durata.Add("scia_di_fuoco",5);
        lista_abilita_durata.Add("boccetta_di_acido",5);
        lista_abilita_durata.Add("meteore",5);
        lista_abilita_durata.Add("scudo",20);

        //lista_danni_nemici.Add("",0.1f);
        //lista_danni_nemici.Add("Homunculus2",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
