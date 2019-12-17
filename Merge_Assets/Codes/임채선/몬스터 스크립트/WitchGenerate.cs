using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchGenerate : MonoBehaviour
{
    public GameObject Witch;
    public GameObject Witch_2;
    public bool witch_dead = false;
    public void Witch1_Dead(){
        GameObject Witch2 = Instantiate(Witch_2) as GameObject;
    }
    void Start()
    {
        //Witch = GameObject.FindWithTag ("Witch");
    }

    // Update is called once per frame
    void Update()
    {
        if(Witch.GetComponent<WitchController> ().isDead){
            Witch1_Dead();
        }
    }
}
