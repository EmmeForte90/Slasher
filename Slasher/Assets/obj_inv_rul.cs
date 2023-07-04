using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_inv_rul : MonoBehaviour
{
    public hero_rule hero_rule;
    public float tempo_invincibilita=20;
    private bool bool_attivo=false;
    public GameObject ps_attiva;
    private float tempo_al_secondo=0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bool_attivo){
            tempo_al_secondo-=(1*Time.deltaTime);
            if (tempo_al_secondo>=0){
                transform.localScale = new Vector3(tempo_al_secondo*2, tempo_al_secondo*2, tempo_al_secondo*2);
            } else {
                transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }

    void OnTriggerEnter(Collider collision){
        if (bool_attivo){return;}
        if (collision.gameObject.name=="go_raggio_exp"){
            bool_attivo=true;
            StartCoroutine(eroe_guadagna_invincibilita());
        }
    }

    private IEnumerator eroe_guadagna_invincibilita(){
        ps_attiva.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        hero_rule.eroe_invincibilita(tempo_invincibilita);
        Destroy(gameObject);
    }
}
