using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boccetta_di_acido_rule : MonoBehaviour
{
    private float speed_min=8;
    private float speed_max=20;
    private Rigidbody rb;
    public GameObject GO_pozza_acido;
    private bool bool_distrutta=false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(Random.Range(10,50)*-1, Random.Range(0, 360), 0);
        //rb.AddRelativeForce(new Vector3(0, Random.Range(speed_min,speed_max)));
        StartCoroutine(distruggi_boccetta());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Random.Range(speed_min,speed_max));
    }

    void OnTriggerEnter(Collider collision){
        //print ("boccetta: entro in collissione con "+collision.gameObject.name+" ("+collision.gameObject.tag+")");
        if (collision.gameObject.tag=="pavimento"){
            bool_distrutta=true;
            Destroy(gameObject);
            GameObject go_temp;
            go_temp=Instantiate(GO_pozza_acido);
            go_temp.name="pozza_acido";
            go_temp.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            go_temp.SetActive(true);
        }
    }

    private IEnumerator distruggi_boccetta(){
        yield return new WaitForSeconds(1.5f);
        if (!bool_distrutta){
            Destroy(gameObject);
        }
    }
}
