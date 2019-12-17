using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog_zone : MonoBehaviour
{
    public bool isDead;
    float spwan_timer = 0f;
    float spwan_time = 15.0f;
    bool istime = false;
    public GameObject Monster;
    GameObject Spwaned_Monster;
    public void Monster_State(bool sd){
        this.isDead = sd;
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Monster"){
            Spwaned_Monster = col.gameObject;
            col.gameObject.GetComponent<frog>().Set_Zone(this.transform.position);
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Monster"){
            //Debug.Log("Zone Out!");
            col.gameObject.GetComponent<frog>().Zone_Out();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-1.0f,1.0f);
        Vector3 pos = this.transform.position;
        pos.x += x;
        GameObject Spwan_Monster = Instantiate(Monster) as GameObject;
        Spwan_Monster.transform.position = pos;
        Spwan_Monster.GetComponent<frog>().Set_SpwanZone(this.gameObject);
        isDead = false;
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
        if(isDead && istime){
            GameObject Spwan_Monster = Instantiate(Monster) as GameObject;
            Spwan_Monster.transform.position = this.transform.position;
            isDead = false;
            Spwan_Monster.GetComponent<frog>().Set_SpwanZone(this.gameObject);
        }
    }
}
