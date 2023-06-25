using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteora_singola_rule : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject GO_esplosione_meteora;
    private bool bool_distrutta=false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(distruggi_meteora());
        rb.AddForce(-transform.up * 50f,ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision){
        print ("meteora: entro in collissione con "+collision.gameObject.name+" ("+collision.gameObject.tag+")");
        if (collision.gameObject.tag=="pavimento"){
            bool_distrutta=true;
            Destroy(gameObject);
            GameObject go_temp;
            go_temp=Instantiate(GO_esplosione_meteora);
            go_temp.name="esplosione_meteora";
            go_temp.transform.localPosition = new Vector3(transform.position.x, 1, transform.position.z);
            go_temp.SetActive(true);
        }
    }

    private IEnumerator distruggi_meteora(){
        yield return new WaitForSeconds(2f);
        if (!bool_distrutta){
            Destroy(gameObject);
        }
    }
}
