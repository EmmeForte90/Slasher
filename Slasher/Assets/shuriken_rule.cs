using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuriken_rule : MonoBehaviour
{
    private float speed=50;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        StartCoroutine(distruggi_shuriken());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private IEnumerator distruggi_shuriken(){
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
