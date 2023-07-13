using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effetti : MonoBehaviour
{

    public GameObject ps_hit_red;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void effetto_hit_rosso(float x, float y, float z){
        GameObject go_temp=Instantiate(ps_hit_red);
        go_temp.transform.localPosition=new Vector3(x,y,z);
        go_temp.SetActive(true);
    }
}
