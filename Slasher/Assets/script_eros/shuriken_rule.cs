using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken_rule : MonoBehaviour
{
    private float speed=50;
    private bool bool_attivo=true;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        StartCoroutine(distruggi_shuriken_coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag=="nemico"){
            distruggi_shuriken();
        }
    }

    private IEnumerator distruggi_shuriken_coroutine(){
        yield return new WaitForSeconds(1.5f);
        distruggi_shuriken();
    }

    private void distruggi_shuriken(){
        if (!bool_attivo){return;}
        bool_attivo=false;
        Destroy(gameObject);
    }
}
