using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_Controller : MonoBehaviour
{
    private float existence_time = 10.0f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.tag =="Player"){ // 공격 스킬 피격판정시
        Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= existence_time)
            Destroy(gameObject);
    }
}
