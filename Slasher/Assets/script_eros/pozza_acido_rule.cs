using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pozza_acido_rule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(distruggi_pozza_acido());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator distruggi_pozza_acido(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
