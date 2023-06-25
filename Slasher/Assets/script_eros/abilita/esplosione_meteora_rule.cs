using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esplosione_meteora_rule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(distruggi_esplosione_meteora());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator distruggi_esplosione_meteora(){
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
