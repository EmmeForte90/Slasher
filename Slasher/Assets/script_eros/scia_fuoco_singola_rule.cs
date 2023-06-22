using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scia_fuoco_singola_rule : MonoBehaviour
{
    public float durata=3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(distruggi_scia());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator distruggi_scia(){
        yield return new WaitForSeconds(durata);
        Destroy(gameObject);
    }
}
