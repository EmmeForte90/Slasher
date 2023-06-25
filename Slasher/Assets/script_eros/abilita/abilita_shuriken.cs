using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_shuriken : MonoBehaviour
{
    public float dmg=0.1f;
    public int quantita=3;
    public GameObject GO_shuriken;
    public Transform hero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void lancia_shuriken(){
        for (int i=1;i<=quantita;i++){
            StartCoroutine(lancia_shuriken_coroutine(i));
        }
    }

    private IEnumerator lancia_shuriken_coroutine(int num){
        yield return new WaitForSeconds(0.1f*num);
        GameObject go_temp;
        go_temp=Instantiate(GO_shuriken);
        go_temp.name="shuriken";
        go_temp.transform.localPosition = new Vector3(hero.position.x, hero.position.y, hero.position.z);
        go_temp.SetActive(true);
    }

    public void setta_livello(int livello){
        switch (livello){
            case 1:{quantita=3;break;}
            case 2:{quantita=5;break;}
            case 3:{quantita=7;break;}
            case 4:{quantita=9;break;}
            case 5:{quantita=10;break;}
            case 6:{quantita=11;break;}
            case 7:{quantita=12;break;}
            case 8:{quantita=13;break;}
            case 9:{quantita=14;break;}
            case 10:{quantita=15;break;}
        }
        dmg=0.3f+(0.05f*livello);
    }
}
