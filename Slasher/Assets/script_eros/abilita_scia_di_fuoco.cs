using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_scia_di_fuoco : MonoBehaviour
{
    public float dmg=0.1f;
    public float durata=3;
    public bool bool_attiva=false;
    public GameObject GO_scia_singola;
    public scia_fuoco_singola_rule scia_fuoco_singola_rule;
    public Transform hero;
    public hero_rule hero_rule;
    private float dimenione_float;
    private Vector3 dimensione_fuoco;
    private int livello_scale=1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bool_attiva){
            if (hero_rule.bool_movimento){
                if (Random.Range(0,3)!=2){
                    GameObject go_temp;
                    go_temp=Instantiate(GO_scia_singola);
                    go_temp.name="scia_di_fuoco";
                    go_temp.transform.localPosition = new Vector3(hero.position.x, hero.position.y, hero.position.z);
                    dimenione_float=1+(0.15f*livello_scale)+Random.Range(0.0f, 0.5f);
                    go_temp.transform.localScale=new Vector3 (dimenione_float,dimenione_float,dimenione_float);
                    go_temp.SetActive(true);
                }
            }
        }
    }

    public void setta_livello(int livello){
        livello_scale=livello;
        durata=0.8f+(0.2f*livello);
        dmg=0.1f*livello;
        scia_fuoco_singola_rule.durata=durata;
    }
}
