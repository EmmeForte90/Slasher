using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public string test_nemico="nemico_1";

    public tempo_special tempo_special;
    public Transform hero_transform; 

    public gestione_gui gestione_gui;
    public ui_upgrade ui_upgrade;

    public GameObject nemici;
    public GameObject lista_nemici_tipo;
    private Dictionary<string, GameObject> lista_GO_nemici_tipo = new Dictionary<string, GameObject>();

    public GameObject GO_cont_spawn_enemy_vicino;
    public Dictionary<int, GameObject> lista_GO_spawn_enemy_vicino = new Dictionary<int, GameObject>();

    public GameObject GO_cont_spawn_enemy_bordo;
    public Dictionary<int, GameObject> lista_GO_spawn_enemy_bordo = new Dictionary<int, GameObject>();

    public GameObject GO_sep_default;

    private int num_sep_vicino=0;
    private int num_sep_bordo=0;

    private float tempo_spawn_vicino=1f;
    private float tempo_spawn_vicino_attuale=1f;

    private float tempo_spawn_bordo=1f;
    private float tempo_spawn_bordo_attuale=1f;

    private int eroe_livello=0;
    private float xp_attuale=0;
    private float xp_next=0;
    private float xp_eccesso=0;

    private float tempo_attuale=0;
    public TMPro.TextMeshProUGUI txt_tempo;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in lista_nemici_tipo.transform) {
            lista_GO_nemici_tipo.Add(child.gameObject.name,child.gameObject);
        }

        foreach(Transform child in GO_cont_spawn_enemy_vicino.transform) {
            if (child.gameObject.active){
                num_sep_vicino++;
                lista_GO_spawn_enemy_vicino.Add(num_sep_vicino,child.gameObject);
            }
        }
        foreach(Transform child in GO_cont_spawn_enemy_bordo.transform) {
            if (child.gameObject.active){
                num_sep_bordo++;
                lista_GO_spawn_enemy_bordo.Add(num_sep_bordo,child.gameObject);
            }
        }
        //genera_sep_medi();    //funzione usata per generare automaticamente gli spawn...presto potrai cancellare

        xp_next=get_next_level_xp(1);
    }

    public void eroe_distrugge_area(float distanza_distruzione){
        float distanza_temp=0;
        foreach(Transform child in nemici.transform) {
            distanza_temp=Vector3.Distance(hero_transform.position, child.transform.position);
            if (distanza_temp<=distanza_distruzione){
                child.GetComponent<enemy_rule>().attiva_morte_nemico();
            }
            print (distanza_temp);
        }
    }

    private void setta_txt_tempo(){
        tempo_attuale+=(1*Time.deltaTime);
        int num_secondi=(int)tempo_attuale;

        if (num_secondi>0){
            if (num_secondi%20==0){
                tempo_special.attiva_special(9f, "ULTRA!", "BOOM!");
            }
        }

        int num_minuti=0;
        if (num_secondi>=60){
            num_minuti=num_secondi/60;
            num_secondi-=(num_minuti*60);
        }
        string testo="";
        if (num_minuti<10){testo+="0";}
        testo+=num_minuti.ToString();
        testo+=" : ";
        if (num_secondi<10){testo+="0";}
        testo+=num_secondi.ToString();
        txt_tempo.SetText(testo);

        
    }

    // Update is called once per frame
    void Update()
    {
        setta_txt_tempo();
        /*
        if (tempo_spawn_vicino_attuale<=0){
            spawn_enemy("vicino");
            tempo_spawn_vicino_attuale+=tempo_spawn_vicino;
        } else {
            tempo_spawn_vicino_attuale-=(1f*Time.deltaTime);
        }
        */

        if (tempo_spawn_bordo_attuale<=0){
            spawn_enemy("bordo");
            tempo_spawn_bordo_attuale+=tempo_spawn_bordo;
        } else {
            tempo_spawn_bordo_attuale-=(1f*Time.deltaTime);
        }
    }

    private void spawn_enemy(string raggio){
        int num_sep=0;
        switch (raggio){
            case "vicino":{
                for (int i=1;i<=num_sep_vicino;i++){
                    lista_GO_spawn_enemy_vicino[i].GetComponent<SpriteRenderer>().color=Color.white;
                }

                num_sep=Random.Range(1,(num_sep_vicino+1));
                spawn_enemy_point(raggio,num_sep);
                break;
            }
            case "bordo":{
                for (int i=1;i<=num_sep_bordo;i++){
                    lista_GO_spawn_enemy_bordo[i].GetComponent<SpriteRenderer>().color=Color.white;
                }

                num_sep=Random.Range(1,(num_sep_bordo+1));
                spawn_enemy_point(raggio,num_sep);
                break;
            }
        }
    }

    private void spawn_enemy_point(string raggio, int num_sep){
        switch (raggio){
            case "vicino":{
                Collider[] hitColliders = Physics.OverlapSphere(lista_GO_spawn_enemy_vicino[num_sep].transform.position, 1);
                foreach (var hitCollider in hitColliders){
                    if (hitCollider.tag=="roccia"){
                        spawn_enemy(raggio);
                        lista_GO_spawn_enemy_vicino[num_sep].GetComponent<SpriteRenderer>().color=Color.red;return;
                    }
                }
                lista_GO_spawn_enemy_vicino[num_sep].GetComponent<SpriteRenderer>().color=Color.green;
                spawn_nemico(lista_GO_spawn_enemy_vicino[num_sep].transform.position.x,lista_GO_spawn_enemy_vicino[num_sep].transform.position.y,lista_GO_spawn_enemy_vicino[num_sep].transform.position.z);
                break;
            }
            case "bordo":{
                Collider[] hitColliders = Physics.OverlapSphere(lista_GO_spawn_enemy_bordo[num_sep].transform.position, 1);
                foreach (var hitCollider in hitColliders){
                    if (hitCollider.tag=="roccia"){
                        spawn_enemy(raggio);
                        lista_GO_spawn_enemy_bordo[num_sep].GetComponent<SpriteRenderer>().color=Color.red;
                        return;
                    }
                }
                lista_GO_spawn_enemy_bordo[num_sep].GetComponent<SpriteRenderer>().color=Color.green;
                spawn_nemico(lista_GO_spawn_enemy_bordo[num_sep].transform.position.x,lista_GO_spawn_enemy_bordo[num_sep].transform.position.y,lista_GO_spawn_enemy_bordo[num_sep].transform.position.z);
                break;
            }
        }
    }

    private void spawn_nemico(float x, float y, float z){
        GameObject go_temp;
        go_temp=Instantiate(lista_GO_nemici_tipo[test_nemico],nemici.transform);
        go_temp.transform.position=new Vector3(x,1,z);
        go_temp.SetActive(true);
    }

    private int get_next_level_xp(int level){
        int xp=0;
        if (level<=10){xp=level*10;}
        else if (level<=20){xp=level*12;}
        else if (level<=30){xp=level*15;}
        else if (level<=40){xp=level*20;}
        else {xp=level*30;}
        return xp;
    }

    public void eroe_guadagna_exp(float xp){
        xp_attuale+=xp;
        print ("xp_attuale: "+xp_attuale);
        if (xp_attuale>=xp_next){
            eroe_livello++;
            xp_next=get_next_level_xp(eroe_livello);
            xp_attuale=0;
            xp_eccesso=xp_attuale-xp_next;
            ui_upgrade.attiva_schermata_upgrade();
            gestione_gui.txt_level.SetText(eroe_livello.ToString());
        }
        gestione_gui.setta_img_xp(xp_attuale,xp_next);
    }

    public void check_esp_eccesso(){
        if (xp_eccesso>0){
            eroe_guadagna_exp(xp_eccesso);
        }
    }

    //questo blocco è servita a generare temporaneamente i sep del bordo; Presto potrai cancellare
    private void genera_sep_medi(){
        float x,y,z;
        int theta;
        int radius=100;
        int num_spaw=50;

        for (int i=1;i<=num_spaw;i++){
            theta=360*i/num_spaw;
             x = Mathf.Cos(theta) * radius;
             y=0;
             z = Mathf.Sin(theta) * radius;
             print (x+" - "+y+" - "+z);
             inst_sep(x,y,z);
        }
    }

    private void inst_sep(float x, float y, float z){
        GameObject go_temp;
        go_temp=Instantiate(GO_sep_default,GO_cont_spawn_enemy_bordo.transform);
        go_temp.transform.position=new Vector3(x,1,z);
    }
    //fine blocco
}
