using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Gallery_Changer : MonoBehaviour
{
    private bool ending1 = false;
    public GameObject ending1_button;
    public Sprite locked;
    public Sprite unlocked;
    public void Gallery_Back(){
        SceneManager.LoadScene("MainScene");     // 게임시작 씬을 호출
    }
    public void Play_Ending(){
        SceneManager.LoadScene("Ending1");
    }
    IEnumerator sleep2seconds(){
        yield return new WaitForSeconds(2.0f);
    }
    void Start(){
        ending1_button = GameObject.Find("ending1_button");
        ending1 = false;
    }
    void Update(){
        if(ending1){
            ending1_button.GetComponent<Image>().sprite = unlocked;
            ending1_button.GetComponent<Button>().interactable = true;
        }
        else{
            ending1_button.GetComponent<Image>().sprite = locked;
            ending1_button.GetComponent<Button>().interactable = false;
        }
    }
}
