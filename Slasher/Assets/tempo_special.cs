using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Spine;
using Spine.Unity;

public class tempo_special : MonoBehaviour
{
    public GameObject GO_schermata_animazione_special;
    public SkeletonGraphic SkeletonAnimation_anim_eroe;

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
        GO_schermata_animazione_special.SetActive(false);
        resetta_animazione();
        GO_tempo_special.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bool_attivo){
            if (tempo_scadenza>0){
                tempo_scadenza-=(1*Time.deltaTime);
                tempo_secondi=Mathf.RoundToInt(tempo_scadenza);
                txt_time.SetText(tempo_secondi.ToString());
                if (tempo_secondi<=0){
                    /*
                    txt_time.SetText("");
                    txt_ultra.SetText(parola_finale);
                    */
                    GO_tempo_special.SetActive(false);


                    bool_attivo=false;

                    
                    SkeletonAnimation_anim_eroe.timeScale=1;
                    GO_schermata_animazione_special.SetActive(true);

                    StartCoroutine(attiva_potere_hero());
                    //StartCoroutine(disattiva_tempo_special());
                }
            }
        }
    }

    public void attiva_special(float tempo_partenza_f, string parola_inizio, string parola_fine){
        txt_time.SetText("");
        txt_ultra.SetText(parola_inizio);
        parola_finale=parola_fine;
        tempo_scadenza=tempo_partenza_f+0.5f;
        bool_attivo=true;
        GO_tempo_special.SetActive(true);
    }

    private IEnumerator disattiva_tempo_special(){
        yield return new WaitForSeconds(1f);
        GO_tempo_special.SetActive(false);
    }

    private IEnumerator attiva_potere_hero(){
        yield return new WaitForSeconds(2f);
        hero_rule.attiva_potere_tempo_speciale();
        resetta_animazione();
    }

    private void resetta_animazione(){
        SkeletonAnimation_anim_eroe.AnimationState.ClearTrack(0);
        SkeletonAnimation_anim_eroe.AnimationState.SetAnimation(0,"animation",false);
        SkeletonAnimation_anim_eroe.timeScale=0;
        GO_schermata_animazione_special.SetActive(false);
    }
}
