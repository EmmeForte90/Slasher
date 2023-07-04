using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pozza_acido_rule : MonoBehaviour
{
    public SpriteRenderer sprite_pozza;
    private string file_sprite;

    private float tempo_rimpicciolimento=2;
    private Vector3 start_scale;
    private Vector3 new_scale;
    // Start is called before the first frame update
    void Start()
    {
        tempo_rimpicciolimento=2;

        file_sprite=Random.Range(1,15).ToString();
        sprite_pozza.sprite = Resources.Load<Sprite>("pozze_acido/"+file_sprite);

        StartCoroutine(distruggi_pozza_acido());

        start_scale=transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        tempo_rimpicciolimento-=(1f*Time.deltaTime);
        if (tempo_rimpicciolimento<=1){
            if (tempo_rimpicciolimento>0){
                new_scale=start_scale*tempo_rimpicciolimento;
                transform.localScale=new_scale;
            }
        }
    }

    private IEnumerator distruggi_pozza_acido(){
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
