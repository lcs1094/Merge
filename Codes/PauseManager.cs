using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    private bool paused = false; // 일시정지 확인
    void start(){}
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)) // ESC 입력시
        {
            if (paused) // 일시정지 상태이면
            {
                Time.timeScale = 1; // 게임시간 정상화
                PausePanel.gameObject.SetActive(false); // 일시정지 패널 비활성화
                paused = false; // 일시정지 상태 false
            }
            else // 일시정지 상태가 아니면
            {
                Time.timeScale = 0; // 게임시간 정지
                PausePanel.gameObject.SetActive(true); // 일시정지 패널 활성화
                paused = true; // 일시정지 상태 true
            }

        }
    }
    
    public void Resume(){ // 일시정지 해제 메소드
        paused = false; // 일시정지 상태 false
        Time.timeScale = 1; // 게임시간 정상화 <- 일시정지시 게임시간을 정지했으므로
        PausePanel.gameObject.SetActive(false); // 일시정지 패널 비활성화
    }
    public void Quit(){ // 게임 종료 메소드
        Application.Quit(); // 프로그램 종료
    }
    public void LoadGame2(){ // 씬 전환 메소드, LoadGame 메소드는 Title 씬에서 사용하였음
        Time.timeScale = 1; // 게임시간 정상화 <- 일시정지시 게임시간을 정지했으므로
        SceneManager.LoadScene("Title"); // Title 메소드 호출
    }
}
