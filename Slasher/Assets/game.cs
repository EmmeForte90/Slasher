using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject GO_cont_spawn_enemy_vicino;
    public Dictionary<int, GameObject> lista_GO_spawn_enemy_vicino = new Dictionary<int, GameObject>();

    public GameObject GO_cont_spawn_enemy_bordo;
    public Dictionary<int, GameObject> lista_GO_spawn_enemy_bordo = new Dictionary<int, GameObject>();

    public GameObject GO_sep_default;

    private int num_sep_vicino=0;

    private float tempo_spawn_vicino=1f;
    private float tempo_spawn_vicino_attuale=1f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in GO_cont_spawn_enemy_vicino.transform) {
            if (child.gameObject.active){
                num_sep_vicino++;
                lista_GO_spawn_enemy_vicino.Add(num_sep_vicino,child.gameObject);
            }
        }
        //genera_sep_medi();    //funzione usata per generare automaticamente gli spawn...
    }

    // Update is called once per frame
    void Update()
    {
        if (tempo_spawn_vicino_attuale<=0){
            spawn_enemy("vicino");
            tempo_spawn_vicino_attuale+=tempo_spawn_vicino;
        } else {
            tempo_spawn_vicino_attuale-=(1f*Time.deltaTime);
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

                print("spawn "+num_sep);
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
                break;
            }
        }
    }

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
}
