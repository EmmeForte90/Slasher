using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_exp_rule : MonoBehaviour
{
    public game game;
    public float xp=1;
    private bool bool_attivo=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision){
        if (bool_attivo){return;}
        if (collision.gameObject.name=="go_raggio_exp"){
            bool_attivo=true;
            StartCoroutine(eroe_guadagna_xp());
        }
    }

    private IEnumerator eroe_guadagna_xp(){
        yield return new WaitForSeconds(0.5f);
        game.eroe_guadagna_exp(xp);
        Destroy(gameObject);
    }
}
