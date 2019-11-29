using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Gallery_Changer : MonoBehaviour
{
    private bool ending1 = false;
    private bool ending2 = false;
    private bool ending3 = false;
    private bool ending4 = false;
    public GameObject ending1_button;
    public GameObject ending2_button;
    public GameObject ending3_button;
    public GameObject ending4_button;
    public Sprite locked;
    public Sprite unlocked;
    public void Gallery_Back(){
        SceneManager.LoadScene("MainScene");     // 게임시작 씬을 호출
    }
    public void Play_Ending1(){
        SceneManager.LoadScene("Ending1");
    }
    public void Play_Ending2(){
        SceneManager.LoadScene("Ending2");
    }
    public void Play_Ending3(){
        SceneManager.LoadScene("Ending3");
    }
    public void Play_Ending4(){
        SceneManager.LoadScene("Ending4");
    }
    void Start(){
        ending1_button = GameObject.Find("ending1_button");
        ending2_button = GameObject.Find("ending2_button");
        ending3_button = GameObject.Find("ending3_button");
        ending4_button = GameObject.Find("ending4_button");
        ending1 = false;
    }
    void Update(){
        // ending1 = GameManager.instance.ending_return(1);
        // ending2 = GameManager.instance.ending_return(2);
        // ending3 = GameManager.instance.ending_return(3);
        // ending4 = GameManager.instance.ending_return(4);
        if(ending1){
            ending1_button.GetComponent<Image>().sprite = unlocked;
            ending1_button.GetComponent<Button>().interactable = true;
        }
        else{
            ending1_button.GetComponent<Image>().sprite = locked;
            ending1_button.GetComponent<Button>().interactable = false;
        }
        if(ending2){
            ending2_button.GetComponent<Image>().sprite = unlocked;
            ending2_button.GetComponent<Button>().interactable = true;
        }
        else{
            ending2_button.GetComponent<Image>().sprite = locked;
            ending2_button.GetComponent<Button>().interactable = false;
        }
        if(ending3){
            ending3_button.GetComponent<Image>().sprite = unlocked;
            ending3_button.GetComponent<Button>().interactable = true;
        }
        else{
            ending3_button.GetComponent<Image>().sprite = locked;
            ending3_button.GetComponent<Button>().interactable = false;
        }
        if(ending1){
            ending4_button.GetComponent<Image>().sprite = unlocked;
            ending4_button.GetComponent<Button>().interactable = true;
        }
        else{
            ending4_button.GetComponent<Image>().sprite = locked;
            ending4_button.GetComponent<Button>().interactable = false;
        }
    }
}
