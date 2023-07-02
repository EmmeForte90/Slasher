using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mappa : MonoBehaviour
{
    public GameObject GO_mappa;
    public GameObject lista_blocchi_mappa_totali;
    public GameObject lista_blocchi_mappa_fighi;
    private Dictionary<string, GameObject> mappa_reale = new Dictionary<string, GameObject>();
    private Dictionary<int, GameObject> lista_blocchi_creazione_mappa = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> lista_blocchi_creazione_mappa_fighi = new Dictionary<int, GameObject>();
    private int num_blocchi_creazione_mappa=0;
    private int num_blocchi_creazione_mappa_fighi=0;
    private int grandezza_blocchi=230;
    private Dictionary<string, int> blocchi_sempre_visibili = new Dictionary<string, int>();
    public GameObject obj_vit_50;

    private float num_blocchi_generati=0;

    // Start is called before the first frame update
    void Start()
    {
        lista_blocchi_mappa_fighi.SetActive(false);

        foreach(Transform child in lista_blocchi_mappa_totali.transform) {
            num_blocchi_creazione_mappa++;
            lista_blocchi_creazione_mappa.Add(num_blocchi_creazione_mappa,child.gameObject);
            child.gameObject.SetActive(false);
        }

        foreach(Transform child in lista_blocchi_mappa_fighi.transform) {
            num_blocchi_creazione_mappa_fighi++;
            lista_blocchi_creazione_mappa_fighi.Add(num_blocchi_creazione_mappa_fighi,child.gameObject);
            child.gameObject.SetActive(false);
        }

        genera_blocchi_mappa(0,0);
    }

    public void hero_esce_blocco(string string_blocco){
        string[] splitArray =  string_blocco.Split(char.Parse("_"));
        name = splitArray[0];
        int x=int.Parse(splitArray[0]);
        int y=int.Parse(splitArray[1]);

            string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        y--;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        x++;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        y++;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        y++;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        x--;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        x--;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        y--;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
        y--;string_blocco=from_coordinate_to_string(x,y);if (!blocchi_sempre_visibili.ContainsKey(string_blocco)){mappa_reale[string_blocco].SetActive(false);}
    }

    public void hero_entra_mappa_stringa(string string_blocco){
        string[] splitArray =  string_blocco.Split(char.Parse("_"));
        name = splitArray[0];
        int x=int.Parse(splitArray[0]);
        int y=int.Parse(splitArray[1]);
        genera_blocchi_mappa(x,y);
    }

    public void genera_blocchi_mappa_stringa(string string_blocco){
        //print (string_blocco);
        string[] splitArray =  string_blocco.Split(char.Parse("_"));
        name = splitArray[0];
        int x=int.Parse(splitArray[0]);
        int y=int.Parse(splitArray[1]);
        genera_blocchi_mappa(x,y);
    }

    private void genera_blocchi_mappa(int x, int y){
        string string_blocco;
        blocchi_sempre_visibili.Clear();
            string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        y--;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        x++;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        y++;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        y++;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        x--;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        x--;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        y--;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
        y--;string_blocco=from_coordinate_to_string(x,y);precheck_blocco_mappa(x,y);
    }

    private void precheck_blocco_mappa(int x, int y){
        string string_blocco=from_coordinate_to_string(x,y);
        blocchi_sempre_visibili.Add(string_blocco,1);
        if (!mappa_reale.ContainsKey(string_blocco)){genera_blocco_random(x,y);}
        else {
            mappa_reale[string_blocco].SetActive(true);
        }
    }

    private void genera_blocco_random(int x, int y){
        num_blocchi_generati++;
        float pos_x=x*grandezza_blocchi;
        float pos_z=y*grandezza_blocchi;
        float pos_y=0;
        int numero_casuale=0;
        string string_blocco=from_coordinate_to_string(x,y);
        if (num_blocchi_generati%2!=0){
            numero_casuale=Random.Range(1,(num_blocchi_creazione_mappa+1));
            GameObject go_temp=Instantiate(lista_blocchi_creazione_mappa[numero_casuale]);
            if ((x+y)%2==0){pos_y=-0.1f;}
            go_temp.transform.SetParent(GO_mappa.transform);
            go_temp.transform.position=new Vector3(pos_x,pos_y,pos_z);

            var children = go_temp.GetComponentsInChildren<Transform>();
            foreach (var child in children){
                if (child.name == "pavimento"){
                    child.name=string_blocco;
                    break;
                }
            }

            go_temp.SetActive(true);
            mappa_reale.Add(string_blocco,go_temp);

            if (num_blocchi_generati>1){
                GameObject go_temp_2=Instantiate(obj_vit_50);
                go_temp_2.transform.SetParent(go_temp.transform);
                go_temp_2.transform.localPosition=new Vector3(0,1,0);
            }
        } else {//devo generare un blocco figo
            numero_casuale=Random.Range(1,(num_blocchi_creazione_mappa_fighi+1));
            GameObject go_temp=Instantiate(lista_blocchi_creazione_mappa_fighi[numero_casuale]);
            if ((x+y)%2==0){pos_y=-0.1f;}
            go_temp.transform.SetParent(GO_mappa.transform);
            go_temp.transform.position=new Vector3(pos_x,pos_y,pos_z);

            var children = go_temp.GetComponentsInChildren<Transform>();
            foreach (var child in children){
                if (child.name == "pavimento"){
                    child.name=string_blocco;
                    break;
                }
            }
            go_temp.SetActive(true);
            mappa_reale.Add(string_blocco,go_temp);
        }
    }

    private string from_coordinate_to_string(int x, int y){return x.ToString()+"_"+y.ToString();}
}
