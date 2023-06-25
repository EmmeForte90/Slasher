using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mappa : MonoBehaviour
{
    public GameObject GO_mappa;
    public GameObject lista_blocchi_mappa_totali;
    private Dictionary<string, GameObject> mappa_reale = new Dictionary<string, GameObject>();
    private Dictionary<int, GameObject> lista_blocchi_creazione_mappa = new Dictionary<int, GameObject>();
    private int num_blocchi_creazione_mappa=0;
    private int grandezza_blocchi=230;
    private Dictionary<string, int> blocchi_sempre_visibili = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in lista_blocchi_mappa_totali.transform) {
            num_blocchi_creazione_mappa++;
            lista_blocchi_creazione_mappa.Add(num_blocchi_creazione_mappa,child.gameObject);
            child.gameObject.SetActive(false);
        }
        genera_blocchi_mappa(0,0);
    }

    public void hero_esce_blocco(string string_blocco){
        string[] splitArray =  string_blocco.Split(char.Parse("-"));
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
        string[] splitArray =  string_blocco.Split(char.Parse("-"));
        name = splitArray[0];
        int x=int.Parse(splitArray[0]);
        int y=int.Parse(splitArray[1]);
        genera_blocchi_mappa(x,y);
    }

    public void genera_blocchi_mappa_stringa(string string_blocco){
        string[] splitArray =  string_blocco.Split(char.Parse("-"));
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
        string string_blocco=from_coordinate_to_string(x,y);
        int numero_casuale=Random.Range(1,num_blocchi_creazione_mappa);
        GameObject go_temp=Instantiate(lista_blocchi_creazione_mappa[numero_casuale]);
        float pos_x=x*grandezza_blocchi;
        float pos_z=y*grandezza_blocchi;
        float pos_y=0;
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

    private string from_coordinate_to_string(int x, int y){return x.ToString()+"-"+y.ToString();}
}
