using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    private string Stage;
    //int CheckPoint;
    //int Stage;    // 씬 번호로 저장된 스테이지를 가져올 경우 사용

    public void StartGame(){
        SceneManager.LoadScene("gameScene");     // 게임시작 씬을 호출
    }

    public void LoadGame(){

        SceneManager.LoadScene(Stage);        // 저장된 게임을 불러오는 씬 호출
    }

    public void Gallery(){
        SceneManager.LoadScene("Ending_Gallery");         // 갤러리 씬 호출
    }

    public void Quit(){
        Application.Quit();                             // 프로그램 종료(게임 종료)
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Stage")){
            Stage = "gameScene";
        }
        else{
            Stage = PlayerPrefs.GetString("Stage");
            //Stage = PlayerPrefs.GetInt("Stage", 0);   // 씬 번호로 저장된 스테이지를 가져올 경우 사용
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
