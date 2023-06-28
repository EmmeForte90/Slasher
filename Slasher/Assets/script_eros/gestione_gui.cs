using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gestione_gui : MonoBehaviour
{
    public Image fill_ampolla_sx;
    public Image fill_ampolla_dx;
    public Image fill_barra_bottom;

    public Dictionary<string, int> lista_posizioni_abilita = new Dictionary<string, int>();    //abilità, posizione
    private Dictionary<int,TextMeshProUGUI> lista_lvl_abilita = new Dictionary<int, TextMeshProUGUI>();  //posizione, abilita
    private Dictionary<int,Image> lista_img_abilita = new Dictionary<int, Image>();  //posizione, abilita
    public GameObject GO_abilita_attive;

    public Dictionary<string, int> lista_posizioni_abilita_passive = new Dictionary<string, int>();    //abilità, posizione
    private Dictionary<int,TextMeshProUGUI> lista_lvl_abilita_passive = new Dictionary<int, TextMeshProUGUI>();  //posizione, abilita
    private Dictionary<int,Image> lista_img_abilita_passive = new Dictionary<int, Image>();  //posizione, abilita
    public GameObject GO_abilita_passive;

    // Start is called before the first frame update
    void Awake()
    {
        ricerca_ricorsiva_ab_attive(GO_abilita_attive.transform);
        ricerca_ricorsiva_ab_passive(GO_abilita_passive.transform);

        /*
        setta_fill(fill_ampolla_sx,-0.2f);
        setta_fill(fill_ampolla_dx,2f);
        setta_fill(fill_barra_bottom,0.8f);
        */
    }

    public void setta_img_xp(float xp, float xp_totale){
        fill_barra_bottom.fillAmount=xp/xp_totale;
    }

    private void setta_fill(Image img, float settaggio){
        img.fillAmount=settaggio;
    }

    private void ricerca_ricorsiva_ab_attive(Transform parent){
        int posizione_attiva=0;
        int posizione_attiva_img=0;
        foreach(Transform child in parent) {
            if (child.name.Contains("lvl_ab_attiva_")){
                posizione_attiva=int.Parse(child.name.Replace("lvl_ab_attiva_",""));
                lista_lvl_abilita.Add(posizione_attiva,child.GetComponent<TextMeshProUGUI>());
                lista_lvl_abilita[posizione_attiva].SetText("-");
            } else if (child.name.Contains("img_ab_attiva_")){
                posizione_attiva_img=int.Parse(child.name.Replace("img_ab_attiva_",""));
                lista_img_abilita.Add(posizione_attiva_img,child.GetComponent<Image>());
                lista_img_abilita[posizione_attiva_img].enabled=false;
            }
            ricerca_ricorsiva_ab_attive(child);
        }
    }

    private void ricerca_ricorsiva_ab_passive(Transform parent){
        int posizione_passiva=0;
        int posizione_passiva_img=0;
        foreach(Transform child in parent) {
            if (child.name.Contains("lvl_ab_passiva_")){
                posizione_passiva=int.Parse(child.name.Replace("lvl_ab_passiva_",""));
                lista_lvl_abilita_passive.Add(posizione_passiva,child.GetComponent<TextMeshProUGUI>());
                lista_lvl_abilita_passive[posizione_passiva].SetText("-");
            } else if (child.name.Contains("img_ab_passiva_")){
                posizione_passiva_img=int.Parse(child.name.Replace("img_ab_passiva_",""));
                lista_img_abilita_passive.Add(posizione_passiva_img,child.GetComponent<Image>());
                lista_img_abilita_passive[posizione_passiva_img].enabled=false;
            }
            ricerca_ricorsiva_ab_passive(child);
        }
    }

    public void abilita_attiva_gui(string abilita, int livello, int posizione){
        if (livello==1){
            lista_img_abilita[posizione].sprite=Resources.Load<Sprite>("icone_abilita/abilita");
            lista_img_abilita[posizione].enabled=true;
        } else {
            posizione=lista_posizioni_abilita[abilita];
        }
        lista_lvl_abilita[posizione].SetText(livello.ToString());
    }

    public void abilita_passiva_gui(string abilita, int livello, int posizione){
        if (livello==1){
            lista_img_abilita_passive[posizione].sprite=Resources.Load<Sprite>("icone_abilita/abilita");
            lista_img_abilita_passive[posizione].enabled=true;
        } else {
            posizione=lista_posizioni_abilita_passive[abilita];
        }
        lista_lvl_abilita_passive[posizione].SetText(livello.ToString());
    }
}