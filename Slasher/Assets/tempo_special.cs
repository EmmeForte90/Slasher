using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class tempo_special : MonoBehaviour
{
    public hero_rule hero_rule;
    public GameObject GO_tempo_special;
    public TextMeshProUGUI txt_time;
    public TextMeshProUGUI txt_ultra;
    private float tempo_scadenza=0;
    private string parola_finale="";
    private int tempo_secondi;

    private bool bool_attivo=false;
    // Start is called before the first frame update
    void Start()
    {
        GO_tempo_special.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)){
            print ("ci");
            attiva_special(12f, "ULTRA!", "BOOM!");
        }
        if (bool_attivo){
            if (tempo_scadenza>0){
                tempo_scadenza-=(1*Time.deltaTime);
                tempo_secondi=Mathf.RoundToInt(tempo_scadenza);
                txt_time.SetText(tempo_secondi.ToString());
                if (tempo_secondi<=0){
                    txt_time.SetText("0");
                    txt_ultra.SetText(parola_finale);
                    bool_attivo=false;

                    hero_rule.attiva_potere_tempo_speciale();

                    StartCoroutine(disattiva_tempo_special());
                }
            }
        }
    }

    public void attiva_special(float tempo_partenza, string parola_inizio, string parola_fine){
        txt_ultra.SetText(parola_inizio);
        parola_finale=parola_fine;
        tempo_scadenza=tempo_partenza+0.5f;
        bool_attivo=true;
        GO_tempo_special.SetActive(true);
    }

    private IEnumerator disattiva_tempo_special(){
        yield return new WaitForSeconds(2f);
        GO_tempo_special.SetActive(false);
    }
}
