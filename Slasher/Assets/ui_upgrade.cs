using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ui_upgrade : MonoBehaviour
{
    public hero_rule hero_rule;
    public info_comuni info_comuni;

    public GameObject schermata_upgrade;
    public GameObject img_upgrade_1;
    public GameObject img_upgrade_2;
    public GameObject img_upgrade_3;
    public GameObject liv_upgrade_1;
    public GameObject liv_upgrade_2;
    public GameObject liv_upgrade_3;
    public GameObject desc_upgrade_1;
    public GameObject desc_upgrade_2;
    public GameObject desc_upgrade_3;
    public GameObject nome_upgrade_1;
    public GameObject nome_upgrade_2;
    public GameObject nome_upgrade_3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attiva_schermata_upgrade(){
        Dictionary<string, int> scelta_abilita = new Dictionary<string, int>();
        foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_abilita_nome){
            if (!hero_rule.lista_abilita_personaggio.ContainsKey(attachStat.Key)){
                scelta_abilita.Add(attachStat.Key,1);
            } else if (hero_rule.lista_abilita_personaggio[attachStat.Key]<9){
                scelta_abilita.Add(attachStat.Key,hero_rule.lista_abilita_personaggio[attachStat.Key]+1);
            }
        }

        List<string> randomKeys = GetRandomKeys(scelta_abilita, scelta_abilita.Count);
        print ("numero di scelte: "+scelta_abilita.Count);

        schermata_upgrade.SetActive(true);
    }

    private static List<string> GetRandomKeys(Dictionary<string, int> dictionary, int count)
    {
        List<string> keys = new List<string>(count);

        if (dictionary.Count <= count)
        {
            keys.AddRange(dictionary.Keys);
        }
        else
        {
            Random random = new Random();

            List<int> indices = new List<int>(dictionary.Count);
            for (int i = 0; i < dictionary.Count; i++)
            {
                indices.Add(i);
            }

            for (int i = 0; i < count; i++)
            {
                int randomIndex = random.Next(indices.Count);
                int dictionaryIndex = indices[randomIndex];
                indices.RemoveAt(randomIndex);

                string key = new List<string>(dictionary.Keys)[dictionaryIndex];
                keys.Add(key);
            }
        }

        return keys;
    }
}
