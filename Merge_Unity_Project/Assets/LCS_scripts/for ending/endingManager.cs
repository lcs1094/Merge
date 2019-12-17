using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endingManager : MonoBehaviour
{
    public static endingManager Emanager;
    public bool[] endingdown = {false,false,false,false,false};
    public bool blackdown = false;
    public GameObject blackbox;
    public GameObject SceneOne;
    public GameObject SceneTwo;
    public Button One;
    public Button Two;
    Vector3 begin = new Vector3(0,0,0);
    Vector3 done = new Vector3(0,-12.1f,0);
    public void First(){
        if(blackbox.transform.position.y > -12){
            blackbox.transform.position = done;
        }
        else{
            One.gameObject.SetActive(false);
            SceneOne.SetActive(false);
            SceneTwo.SetActive(true);
            Two.gameObject.SetActive(true);
            blackbox.transform.position = begin;
        }
    }
    public void Second(){
        if(blackbox.transform.position.y > -12){
            blackbox.transform.position = done;
        }
        else{
            SceneManager.LoadScene("Gallery");
        }
    }
    void Start()
    {
        if(endingManager.Emanager == null){
            endingManager.Emanager = this;
        }
        else{
            Destroy(gameObject);
        }
        One.gameObject.SetActive(true);
        SceneOne.SetActive(true);
        SceneTwo.SetActive(false);
        Two.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
