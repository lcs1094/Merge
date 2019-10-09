using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("Start_Scene_Name");  // 게임시작 씬을 호출

    public void LoadGame() => SceneManager.LoadScene("LoadFile_Name");      // 저장된 게임을 불러오는 씬 호출

    public void Gallery() => SceneManager.LoadScene("Gallery_Name");        // 갤러리 씬 호출

    public void Quit() => Application.Quit();                               // 프로그램 종료(게임 종료)
}
