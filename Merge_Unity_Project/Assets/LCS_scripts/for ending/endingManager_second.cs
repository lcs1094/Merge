using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endingManager_second : MonoBehaviour
{
    public static endingManager_second Emanager;
    public bool[] endingdown = {false,false,false,false,false};
    public bool blackdown = false;
    public GameObject blackbox;
    public GameObject Scene;
    public Button One;
    Vector3 begin = new Vector3(0,0,0);
    Vector3 done = new Vector3(0,-12.1f,0);
    public void First(){
        if(blackbox.transform.position.y > -12){
            blackbox.transform.position = done;
        }
        else{
            SceneManager.LoadScene("Gallery");
        }
    }
    void Start()
    {
        if(endingManager_second.Emanager == null){
            endingManager_second.Emanager = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
