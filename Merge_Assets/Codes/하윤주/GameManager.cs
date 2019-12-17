using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager Instance;
    public static GameManager instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<GameManager>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newGameManager = new GameObject("GameManager").AddComponent<GameManager>();
                    Instance = newGameManager;
                }
            }
            return Instance;
        }
        private set
        {
            Instance = value;
        }
    }
    public bool[] endings = new bool[] { false, false, false, false, false };     //엔딩을 획득했는지 확인(true면 획득한 엔딩)
    public bool firstStart = false;
    public int lastPlayScene = -1;
    public int lastHeart = 0;
    public int lastBread = 0;
    public int lastMonster = 0;

    void Awake()
    {
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length != 1) { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
        DataController.instance.LoadGameData();
        DataController.instance.Load(0);
    }


    //획득한 엔딩번호를 매개변수로 받아 획득여부를 true로 바꿈
    public void findEnding(int endingNum) { this.endings[endingNum] = true; FindObjectOfType<StageManager>().write_End(); }

    //화면전환 함수
    public void goMainScene() { SceneManager.LoadScene("MainScene"); DataController.instance.Save(0); }

    public void goEndingScene01() { SceneManager.LoadScene("EndingScene01"); }

    public void goEndingScene02() { SceneManager.LoadScene("EndingScene02"); }

    public void goEndingScene03() { SceneManager.LoadScene("EndingScene03"); }

    public void goEndingScene04() { SceneManager.LoadScene("EndingScene04"); }

    public void goTutorialScene() { SceneManager.LoadScene("TutorialScene"); }

    public void goForestScene() { SceneManager.LoadScene("ForestScene"); }

    public void goCandyScene() { SceneManager.LoadScene("CandyScene"); }

    public void goLavaScene() { SceneManager.LoadScene("LavaScene"); }

    public void goGalleryScene() { SceneManager.LoadScene("Gallery"); }

    public void goSaveScene() { SceneManager.LoadScene("SaveScene"); }

    public void goLoadScene() { SceneManager.LoadScene("LoadScene"); }

    public void Quit() { Application.Quit(); }

    public int getSceneNum()
    {
        if (SceneManager.GetActiveScene().name == "TutorialScene") { return 0; }
        else if (SceneManager.GetActiveScene().name == "ForestScene") { return 1; }
        else if (SceneManager.GetActiveScene().name == "CandyScene") { return 2; }
        else if (SceneManager.GetActiveScene().name == "LavaScene") { return 3; }
        else { return -1; }
    }

}