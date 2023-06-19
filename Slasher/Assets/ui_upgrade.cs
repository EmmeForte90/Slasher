using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;
using TMPro;

public class ui_upgrade : MonoBehaviour
{
    public hero_rule hero_rule;
    public info_comuni info_comuni;

    public GameObject schermata_upgrade;
    public Image img_upgrade_1;
    public Image img_upgrade_2;
    public Image img_upgrade_3;
    public TextMeshProUGUI liv_upgrade_1;
    public TextMeshProUGUI liv_upgrade_2;
    public TextMeshProUGUI liv_upgrade_3;
    public TextMeshProUGUI desc_upgrade_1;
    public TextMeshProUGUI desc_upgrade_2;
    public TextMeshProUGUI desc_upgrade_3;
    public TextMeshProUGUI nome_upgrade_1;
    public TextMeshProUGUI nome_upgrade_2;
    public TextMeshProUGUI nome_upgrade_3;
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

        List<string> randomKeys = GetRandomKeys(scelta_abilita, 3);
        print ("numero di scelte: "+scelta_abilita.Count);

        schermata_upgrade.SetActive(true);

        img_upgrade_1.sprite = Resources.Load<Sprite>("icone_abilita/"+randomKeys[0]);
        img_upgrade_2.sprite = Resources.Load<Sprite>("icone_abilita/"+randomKeys[1]);
        img_upgrade_3.sprite = Resources.Load<Sprite>("icone_abilita/"+randomKeys[2]);
        liv_upgrade_1.SetText(scelta_abilita[randomKeys[0]].ToString());
        liv_upgrade_2.SetText(scelta_abilita[randomKeys[1]].ToString());
        liv_upgrade_3.SetText(scelta_abilita[randomKeys[2]].ToString());
        desc_upgrade_1.SetText(info_comuni.lista_abilita_descrizione_generica[randomKeys[0]]);
        desc_upgrade_2.SetText(info_comuni.lista_abilita_descrizione_generica[randomKeys[1]]);
        desc_upgrade_3.SetText(info_comuni.lista_abilita_descrizione_generica[randomKeys[2]]);
        nome_upgrade_1.SetText(info_comuni.lista_abilita_nome[randomKeys[0]]);
        nome_upgrade_2.SetText(info_comuni.lista_abilita_nome[randomKeys[1]]);
        nome_upgrade_3.SetText(info_comuni.lista_abilita_nome[randomKeys[2]]);
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
