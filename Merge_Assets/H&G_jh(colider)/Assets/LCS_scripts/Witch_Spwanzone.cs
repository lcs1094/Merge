using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch_Spwanzone : MonoBehaviour
{
    public bool isDead;
    bool isout = false;
    public GameObject Witch;
    GameObject Spwan_Witch;
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Witch"){
            Spwan_Witch = col.gameObject;
            col.gameObject.GetComponent<WitchController>().Set_Zone(this.transform.position);
            isout = false;
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Witch"){
            //Debug.Log("Zone Out!");
            isout = true;
            col.gameObject.GetComponent<WitchController>().Zone_Out();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = this.transform.position;
        GameObject Spwan_Witch = Instantiate(Witch) as GameObject;
        Spwan_Witch.transform.position = pos;
        Spwan_Witch.GetComponent<WitchController>().Set_SpwanZone(this.gameObject);
        Spwan_Witch.GetComponent<WitchController>().Set_Zone(this.transform.position);
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isout){
            Spwan_Witch.GetComponent<WitchController>().Zone_Out();
        }
    }
}
