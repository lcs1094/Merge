using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    string Stage;
    //int Stage;    // 씬 번호로 저장할 시 사용
    //int CheckPoint;
    public GameObject PausePanel;
    private bool paused = false; // 일시정지 확인

    public void Puase(){       // 일시정지 메소드
        Time.timeScale = 0; // 게임시간 정지
        PausePanel.gameObject.SetActive(true); // 일시정지 패널 활성화
    }
    public void Resume(){ // 일시정지 해제 메소드
        Time.timeScale = 1; // 게임시간 정상화 <- 일시정지시 게임시간을 정지했으므로
        PausePanel.gameObject.SetActive(false); // 일시정지 패널 비활성화
    }
    public void Quit(){ // 게임 종료 메소드
        Application.Quit(); // 프로그램 종료
    }
    public void ToMain(){ // 씬 전환 메소드, LoadGame 메소드는 Title 씬에서 사용하였음
        Time.timeScale = 1; // 게임시간 정상화 <- 일시정지시 게임시간을 정지했으므로
        SceneManager.LoadScene("Title"); // Title 메소드 호출
    }

    public void ChangeWindow(){
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Save(){
        Stage = SceneManager.GetActiveScene().name;
        //Stage = SceneManager.GetActiveScene().buildIndex; // 씬 번호로 저장할 시 사용
        PlayerPrefs.SetString("Stage", Stage);
        //PlayerPrefs.SetInt("Stage", Stage)   // 씬 번호로 저장된 스테이지를 가져올 경우 사용
        //PlayerPrefs.SetInt("CheckPoint", CheckPoint);
    }

    void start(){}
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)) // ESC 입력시
        {
            if (paused) // 일시정지 상태이면
            {
                Resume();
                paused = false; // 일시정지 상태 false
            }
            else // 일시정지 상태가 아니면
            {
                Puase();
                paused = true; // 일시정지 상태 true
            }

        }
        
    }
}
