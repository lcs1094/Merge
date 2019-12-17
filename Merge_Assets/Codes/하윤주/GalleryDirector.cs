using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryDirector : MonoBehaviour
{
    public GameObject ending1_button;
    public GameObject ending2_button;
    public GameObject ending3_button;
    public GameObject ending4_button;
    public GameObject ending1_Text;
    public GameObject ending2_Text;
    public GameObject ending3_Text;
    public GameObject ending4_Text;
    void Start()
    {
        if(!GameManager.instance.endings[1]){
            ending1_button.GetComponent<Button>().interactable = false;
            //ending1_Text.GetComponent<Text>().text = "잠금";
        }
        if(!GameManager.instance.endings[2]){
            ending2_button.GetComponent<Button>().interactable = false;
            //ending2_Text.GetComponent<Text>().text = "잠금";
        }
        if(!GameManager.instance.endings[3]){
            ending3_button.GetComponent<Button>().interactable = false;
            //ending3_Text.GetComponent<Text>().text = "잠금";
        }
        if(!GameManager.instance.endings[4]){
            ending4_button.GetComponent<Button>().interactable = false;
            //ending4_Text.GetComponent<Text>().text = "잠금";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goMain() { GameManager.instance.goMainScene(); }
    public void goEnding01(){
        GameManager.instance.goEndingScene01(); 
    }
    public void goEnding02(){
        GameManager.instance.goEndingScene02();
    }
    public void goEnding03(){
        GameManager.instance.goEndingScene03();
    }
    public void goEnding04(){
        GameManager.instance.goEndingScene04();
    }
}
