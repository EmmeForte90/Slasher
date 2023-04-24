using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_boccetta_di_acido : MonoBehaviour
{
    public float dmg=0.1f;
    public int quantita=3;
    public GameObject GO_boccetta_acido;
    public Transform hero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void lancia_boccetta(){
        for (int i=1;i<=quantita;i++){
            StartCoroutine(lancia_boccetta_coroutine(i));
        }
    }

    private IEnumerator lancia_boccetta_coroutine(int num){
        yield return new WaitForSeconds(1f*num);
        GameObject go_temp;
        go_temp=Instantiate(GO_boccetta_acido);
        go_temp.name="boccetta_acido";
        go_temp.transform.localPosition = new Vector3(hero.position.x, hero.position.y+2, hero.position.z);
        go_temp.SetActive(true);
    }

    public void setta_livello(int livello){
        quantita=2+(1*livello);
        dmg=0.1f+(0.025f*livello);
        //rotationSpeed=120+(10*livello);
    }
}
