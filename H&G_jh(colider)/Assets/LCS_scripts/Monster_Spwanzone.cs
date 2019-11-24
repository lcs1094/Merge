using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Spwanzone : MonoBehaviour
{
    bool isspwan;
    float spwan_timer = 0f;
    float spwan_time = 3.0f;
    bool istime = false;
    public GameObject Monster;
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Monster"){
            isspwan = Monster_Generator.MG.setspwan();
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Moster"){
            isspwan = Monster_Generator.MG.unsetspwan();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spwan_timer += Time.deltaTime;
        if(spwan_timer > spwan_time){
            istime = true;
            spwan_timer = 0f;
        }
        else{
            istime = false;
        }
        if(!isspwan && istime){
            float x = Random.Range(-2.5f,2.5f);
            Vector3 pos = this.transform.position;
            pos.x += x;
            GameObject Spwan_Monster = Instantiate(Monster) as GameObject;
            Spwan_Monster.transform.position = pos;
            isspwan = true;
        }
    }
}
