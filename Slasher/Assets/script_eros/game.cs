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

    public GameObject GO_cont_spawn_enemy_bordo;
    public Dictionary<int, GameObject> lista_GO_spawn_enemy_bordo = new Dictionary<int, GameObject>();

    public GameObject GO_cont_spawn_enemy_cerchio;
    public Dictionary<int, GameObject> lista_GO_spawn_enemy_cerchio = new Dictionary<int, GameObject>();

    public GameObject GO_sep_default;

    private int num_sep_bordo=0;
    private int num_sep_cerchio=0;

    private float tempo_spawn_bordo_attuale=1f;
    private float tempo_spawn_cerchio_attuale=1f;

    private int eroe_livello=0;
    private float xp_attuale=0;
    private float xp_next=0;
    private float xp_eccesso=0;

    private float tempo_attuale_totale=0;
    private int tempo_attuale_secondi=0;
    private int secondi_totali=0;
    public TMPro.TextMeshProUGUI txt_tempo;

    // Start is called before the first frame update
    void Start()
    {
        tempo_spawn_cerchio_attuale=301;

        foreach(Transform child in lista_nemici_tipo.transform) {
            lista_GO_nemici_tipo.Add(child.gameObject.name,child.gameObject);
        }
        foreach(Transform child in GO_cont_spawn_enemy_bordo.transform) {
            if (child.gameObject.active){
                num_sep_bordo++;
                lista_GO_spawn_enemy_bordo.Add(num_sep_bordo,child.gameObject);
            }
        }
        foreach(Transform child in GO_cont_spawn_enemy_cerchio.transform) {
            if (child.gameObject.active){
                num_sep_cerchio++;
                lista_GO_spawn_enemy_cerchio.Add(num_sep_cerchio,child.gameObject);
            }
        }
        //genera_sep_medi();    //funzione usata per generare automaticamente gli spawn...presto potrai cancellare

        xp_next=get_next_level_xp(1);
    }

    // Update is called once per frame
    void Update()
    {
        setta_txt_tempo();

        if (tempo_spawn_bordo_attuale>0){
            tempo_spawn_bordo_attuale-=(1f*Time.deltaTime);
        } else {
            switch (secondi_totali/60){
                case 0:{spawn_enemy("bordo","imp_semplice");tempo_spawn_bordo_attuale+=1f;break;}
                case 1:{spawn_enemy("bordo","imp_semplice");tempo_spawn_bordo_attuale+=0.5f;break;}
                case 2:{spawn_enemy("bordo","cane");tempo_spawn_bordo_attuale+=0.5f;break;}
                case 3:{
                    spawn_enemy("bordo","imp_semplice");
                    spawn_enemy("bordo","cane");
                    spawn_enemy("bordo","cane");
                    tempo_spawn_bordo_attuale+=2f;
                    break;
                }
                case 4:{spawn_enemy("bordo","demone");tempo_spawn_bordo_attuale+=70;break;}
                case 5:{
                    spawn_enemy("bordo","imp_semplice");
                    spawn_enemy("bordo","imp_semplice");
                    spawn_enemy("bordo","volante");
                    tempo_spawn_bordo_attuale+=1f;
                    break;
                }
                case 6:{
                    spawn_enemy("bordo","imp_semplice");
                    spawn_enemy("bordo","cane");
                    spawn_enemy("bordo","volante");
                    tempo_spawn_bordo_attuale+=1f;
                    break;
                }
                case 7:{
                    spawn_enemy("bordo","cane");
                    spawn_enemy("bordo","cane");
                    spawn_enemy("bordo","cane_2");
                    tempo_spawn_bordo_attuale+=1f;
                    break;
                }
                case 8:{spawn_enemy("bordo","demone");tempo_spawn_bordo_attuale+=70;break;}
                case 9:{
                    spawn_enemy("bordo","cane");
                    spawn_enemy("bordo","cane_2");
                    spawn_enemy("bordo","cane_2");
                    tempo_spawn_bordo_attuale+=1f;
                    break;
                }
                case 10:{
                    spawn_enemy("bordo","cane_2");
                    spawn_enemy("bordo","volante");
                    tempo_spawn_bordo_attuale+=0.5f;
                    break;
                }
                case 11:{
                    spawn_enemy("bordo","volante");
                    tempo_spawn_bordo_attuale+=0.2f;
                    break;
                }
                case 12:{
                    spawn_enemy("bordo","demone");
                    spawn_enemy("bordo","demone");
                    tempo_spawn_bordo_attuale+=70;
                    break;
                }
                case 13:{
                    spawn_enemy("bordo","minotauro");
                    tempo_spawn_bordo_attuale+=1f;
                    break;
                }
                case 14:{
                    spawn_enemy("bordo","piovra");
                    tempo_spawn_bordo_attuale+=0.5f;
                    break;
                }
                case 15:{
                    spawn_enemy("bordo","imp_semplice");
                    tempo_spawn_bordo_attuale+=0.1f;
                    break;
                }
                case 16:{
                    spawn_enemy("bordo","demone_collo");
                    tempo_spawn_bordo_attuale+=70;
                    break;
                }
                case 17:{
                    spawn_enemy("bordo","cane");
                    tempo_spawn_bordo_attuale+=0.1f;
                    break;
                }
                case 18:{
                    spawn_enemy("bordo","cane_2");
                    tempo_spawn_bordo_attuale+=0.1f;
                    break;
                }
                case 19:{
                    spawn_enemy("bordo","volante");
                    tempo_spawn_bordo_attuale+=0.1f;
                    break;
                }
                case 20:{
                    spawn_enemy("bordo","demone_collo");
                    spawn_enemy("bordo","demone_collo");
                    tempo_spawn_bordo_attuale+=70;
                    break;
                }
                default:{
                    tempo_spawn_bordo_attuale+=1000;
                    break;
                }
            }
            
        }

        if (tempo_spawn_cerchio_attuale>0){
            tempo_spawn_cerchio_attuale-=(1f*Time.deltaTime);
        } else {
            switch (secondi_totali/60){
                case 5:{spawn_enemy("cerchio_totale","piovra");tempo_spawn_cerchio_attuale+=120;break;}
                case 7:{spawn_enemy("cerchio_totale","piovra");tempo_spawn_cerchio_attuale+=120;break;}
                case 9:{spawn_enemy("cerchio_totale","piovra");tempo_spawn_cerchio_attuale+=120;break;}
                case 11:{spawn_enemy("cerchio_totale","piovra");tempo_spawn_cerchio_attuale+=120;break;}
                case 13:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=120;break;}
                case 15:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=120;break;}
                case 17:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=120;break;}
                case 19:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 20:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 21:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 22:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 23:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 24:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 25:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 26:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 27:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 28:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=60;break;}
                case 29:{spawn_enemy("cerchio_totale","piovra_2");tempo_spawn_cerchio_attuale+=1000;break;}
                default:{
                    tempo_spawn_cerchio_attuale+=2000;
                    break;
                }
            }
        }
    }

    public void eroe_distrugge_area(float distanza_distruzione){
        float distanza_temp=0;
        foreach(Transform child in nemici.transform) {
            distanza_temp=Vector3.Distance(hero_transform.position, child.transform.position);
            if (distanza_temp<=distanza_distruzione){
                child.GetComponent<enemy_rule>().danneggia_nemico("super_potere",5);
            }
            //print (distanza_temp);
        }
    }

    private void setta_txt_tempo(){
        tempo_attuale_totale+=(1*Time.deltaTime);
        tempo_attuale_secondi=(int)tempo_attuale_totale;
        secondi_totali=tempo_attuale_secondi;

        if (tempo_attuale_secondi>0){
            if (tempo_attuale_secondi%600==0){
                tempo_special.attiva_special(10f, "ULTRA!", "BOOM!");
            }
        }

        int num_minuti=0;
        if (tempo_attuale_secondi>=60){
            num_minuti=tempo_attuale_secondi/60;
            tempo_attuale_secondi-=(num_minuti*60);
        }
        string testo="";
        if (num_minuti<10){testo+="0";}
        testo+=num_minuti.ToString();
        testo+=" : ";
        if (tempo_attuale_secondi<10){testo+="0";}
        testo+=tempo_attuale_secondi.ToString();
        txt_tempo.SetText(testo);
    }

    private void spawn_enemy(string raggio, string id_nemico){
        int num_sep=0;
        switch (raggio){
            case "bordo":{
                for (int i=1;i<=num_sep_bordo;i++){
                    lista_GO_spawn_enemy_bordo[i].GetComponent<SpriteRenderer>().color=Color.white;
                }

                num_sep=Random.Range(1,(num_sep_bordo+1));
                spawn_enemy_point(raggio,num_sep,id_nemico);
                break;
            }
            case "cerchio_totale":{
                for (int i=1;i<=num_sep_cerchio;i++){
                    spawn_enemy_point(raggio,i,id_nemico);
                }
                break;
            }
        }
    }

    private void spawn_enemy_point(string raggio, int num_sep, string id_nemico){
        switch (raggio){
            case "bordo":{
                if (id_nemico!="piovra"){
                    Collider[] hitColliders = Physics.OverlapSphere(lista_GO_spawn_enemy_bordo[num_sep].transform.position, 1);
                    foreach (var hitCollider in hitColliders){
                        if (hitCollider.tag=="roccia"){
                            spawn_enemy(raggio,id_nemico);
                            lista_GO_spawn_enemy_bordo[num_sep].GetComponent<SpriteRenderer>().color=Color.red;
                            return;
                        }
                    }
                }
                lista_GO_spawn_enemy_bordo[num_sep].GetComponent<SpriteRenderer>().color=Color.green;
                spawn_nemico(lista_GO_spawn_enemy_bordo[num_sep].transform.position.x,lista_GO_spawn_enemy_bordo[num_sep].transform.position.y,lista_GO_spawn_enemy_bordo[num_sep].transform.position.z,id_nemico);
                break;
            }
            case "cerchio_totale":{
                if (id_nemico!="piovra"){
                    Collider[] hitColliders = Physics.OverlapSphere(lista_GO_spawn_enemy_cerchio[num_sep].transform.position, 1);
                    foreach (var hitCollider in hitColliders){
                        if (hitCollider.tag=="roccia"){
                            spawn_enemy(raggio,id_nemico);
                            lista_GO_spawn_enemy_cerchio[num_sep].GetComponent<SpriteRenderer>().color=Color.red;
                            return;
                        }
                    }
                }
                lista_GO_spawn_enemy_cerchio[num_sep].GetComponent<SpriteRenderer>().color=Color.green;
                spawn_nemico(lista_GO_spawn_enemy_cerchio[num_sep].transform.position.x,lista_GO_spawn_enemy_cerchio[num_sep].transform.position.y,lista_GO_spawn_enemy_cerchio[num_sep].transform.position.z,id_nemico);
               
                break;
            }
        }
    }

    private void spawn_nemico(float x, float y, float z, string id_nemico){
        GameObject go_temp;
        go_temp=Instantiate(lista_GO_nemici_tipo[id_nemico],nemici.transform);
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
        int radius=50;      //100
        int num_spaw=30;    //50

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
        go_temp=Instantiate(GO_sep_default,GO_cont_spawn_enemy_cerchio.transform);
        go_temp.transform.position=new Vector3(x,1,z);
    }
    //fine blocco
}
