using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilita_meteore : MonoBehaviour
{
    public float dmg=1f;
    private int quantita=0;
    public GameObject GO_meteora_singola;
    public Transform hero;
    private int min_range_x=10;
    private int max_range_x=30;
    private int min_range_y=10;
    private int max_range_y=30;
    private int range_x, range_y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lancia_meteora(){
        for (int i=1;i<=quantita;i++){
            StartCoroutine(lancia_meteora_coroutine(i));
        }
    }

    private IEnumerator lancia_meteora_coroutine(int num){
        yield return new WaitForSeconds(0.1f*num);
        GameObject go_temp;
        go_temp=Instantiate(GO_meteora_singola);
        go_temp.name="meteoria_singola";
        range_x=Random.Range(min_range_x,max_range_x);  if (Random.Range(0,2)==1){range_x*=-1;}
        range_y=Random.Range(min_range_y,max_range_y);  if (Random.Range(0,2)==1){range_y*=-1;}
        go_temp.transform.localPosition = new Vector3(hero.position.x+range_x, hero.position.y+40, hero.position.z+range_y);
        go_temp.SetActive(true);
    }

    public void setta_livello(int livello){
        dmg=1f+(0.25f*livello);
        quantita=30+(livello*3);
    }
}
