using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_comuni : MonoBehaviour
{
    public Dictionary<string, float> lista_abilita_cooldown = new Dictionary<string, float>();
    public Dictionary<string, float> lista_abilita_durata = new Dictionary<string, float>();
    // Start is called before the first frame update
    void Start()
    {
        lista_abilita_cooldown.Add("catena",10);

        lista_abilita_durata.Add("catena",3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
