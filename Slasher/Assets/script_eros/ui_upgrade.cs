using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ui_upgrade : MonoBehaviour
{
    public hero_rule hero_rule;
    public game game;
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

    private Dictionary<string, int> scelta_abilita = new Dictionary<string, int>();
    private List<string> abilita_random_su_tre;
    // Start is called before the first frame update
    void Start()
    {
        schermata_upgrade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            attiva_schermata_upgrade();
        }
    }

    public void attiva_schermata_upgrade(){
        Time.timeScale = 0f;
        scelta_abilita.Clear();
        foreach(KeyValuePair<string,string> attachStat in info_comuni.lista_abilita_nome){
            if (!hero_rule.lista_abilita_personaggio.ContainsKey(attachStat.Key)){
                scelta_abilita.Add(attachStat.Key,1);
            } else if (hero_rule.lista_abilita_personaggio[attachStat.Key]<10){
                scelta_abilita.Add(attachStat.Key,hero_rule.lista_abilita_personaggio[attachStat.Key]+1);
            }
        }

        abilita_random_su_tre = Getabilita_random_su_tre(scelta_abilita, 3);
        //print ("numero di scelte: "+scelta_abilita.Count);

        schermata_upgrade.SetActive(true);

        img_upgrade_1.sprite = Resources.Load<Sprite>("icone_abilita/"+abilita_random_su_tre[0]);
        img_upgrade_2.sprite = Resources.Load<Sprite>("icone_abilita/"+abilita_random_su_tre[1]);
        img_upgrade_3.sprite = Resources.Load<Sprite>("icone_abilita/"+abilita_random_su_tre[2]);
        liv_upgrade_1.SetText(scelta_abilita[abilita_random_su_tre[0]].ToString());
        liv_upgrade_2.SetText(scelta_abilita[abilita_random_su_tre[1]].ToString());
        liv_upgrade_3.SetText(scelta_abilita[abilita_random_su_tre[2]].ToString());
        desc_upgrade_1.SetText(info_comuni.lista_abilita_descrizione_generica[abilita_random_su_tre[0]]);
        desc_upgrade_2.SetText(info_comuni.lista_abilita_descrizione_generica[abilita_random_su_tre[1]]);
        desc_upgrade_3.SetText(info_comuni.lista_abilita_descrizione_generica[abilita_random_su_tre[2]]);
        nome_upgrade_1.SetText(info_comuni.lista_abilita_nome[abilita_random_su_tre[0]]);
        nome_upgrade_2.SetText(info_comuni.lista_abilita_nome[abilita_random_su_tre[1]]);
        nome_upgrade_3.SetText(info_comuni.lista_abilita_nome[abilita_random_su_tre[2]]);
    }

    public void click_scelta_upgrade(int num_scelta){
        EventSystem.current.SetSelectedGameObject(null);
        schermata_upgrade.SetActive(false);
        string abilita_temp=abilita_random_su_tre[num_scelta-1];
        if (scelta_abilita[abilita_temp]>1){
            hero_rule.lista_abilita_personaggio[abilita_temp]=scelta_abilita[abilita_temp];
        } else {
            hero_rule.lista_abilita_personaggio.Add(abilita_temp,1);
        }
        hero_rule.aggiorna_abilita_livello(abilita_temp,scelta_abilita[abilita_temp]);
        Time.timeScale = 1f;
        game.check_esp_eccesso();
    }

    private static List<string> Getabilita_random_su_tre(Dictionary<string, int> dictionary, int count)
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
