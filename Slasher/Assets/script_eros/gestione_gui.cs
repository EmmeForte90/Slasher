using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class gestione_gui : MonoBehaviour
{
    public Dictionary<string, int> lista_posizioni_abilita = new Dictionary<string, int>();    //abilità, posizione
    private Dictionary<int,TextMeshProUGUI> lista_lvl_abilita = new Dictionary<int, TextMeshProUGUI>();  //posizione, abilita
    private Dictionary<int,Image> lista_img_abilita = new Dictionary<int, Image>();  //posizione, abilita
    public GameObject GO_abilita_attive;
    // Start is called before the first frame update
    void Start()
    {
        ricerca_go_ricorsiva(GO_abilita_attive.transform);
    }

    private void ricerca_go_ricorsiva(Transform parent){
        int posizione_attiva=0;
        int posizione_attiva_img=0;
        foreach(Transform child in parent) {
            //print (child.name);

            if (child.name.Contains("lvl_ab_attiva_")){
                posizione_attiva=int.Parse(child.name.Replace("lvl_ab_attiva_",""));
                lista_lvl_abilita.Add(posizione_attiva,child.GetComponent<TextMeshProUGUI>());
                lista_lvl_abilita[posizione_attiva].SetText("-");
            } else if (child.name.Contains("img_ab_attiva_")){
                posizione_attiva_img=int.Parse(child.name.Replace("img_ab_attiva_",""));
                lista_img_abilita.Add(posizione_attiva_img,child.GetComponent<Image>());
                lista_img_abilita[posizione_attiva_img].enabled=false;
            }
            ricerca_go_ricorsiva(child);
        }
    }

    public void abilita_attiva_gui(string abilita, int livello, int posizione){
        lista_lvl_abilita[posizione].SetText(livello.ToString());
        if (livello==1){
            lista_img_abilita[posizione].sprite=Resources.Load<Sprite>("icone_abilita/abilita");
            lista_img_abilita[posizione].enabled=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
