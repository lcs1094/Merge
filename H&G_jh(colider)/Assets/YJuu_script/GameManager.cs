using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool[] endings = new bool[5];     //엔딩을 획득했는지 확인(true면 획득한 엔딩)
    public bool firstStart = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        endings = new bool[] { false, false, false, false, false };
        //엔딩은 4개이므로 0번index는 사용하지 않음
        //맨 처음에는 획득한 엔딩이 없음 (DB에서 데이터를 받아와서 획득한 엔딩을 저장하는 함수 작성 필요)
    }

    void Update()
    {
        
    }
   

    //획득한 엔딩번호를 매개변수로 받아 획득여부를 true로 바꿈
    public void findEnding(int endingNum){this.endings[endingNum] = true; }

    //화면전환 함수
    public void goMainScene() {SceneManager.LoadScene("MainScene");}

    public void goEndingScene01(){SceneManager.LoadScene("EndingScene01");}

    public void goEndingScene02() { SceneManager.LoadScene("EndingScene02"); }

    public void goEndingScene03() { SceneManager.LoadScene("EndingScene03"); }

    public void goEndingScene04() { SceneManager.LoadScene("EndingScene04"); }

    public void goTutorialScene(){SceneManager.LoadScene("TutorialScene");}

    public void goForestScene(){SceneManager.LoadScene("ForestScene");}

    public void goCandyScene(){SceneManager.LoadScene("CandyScene");}

    public void goLavaScene(){SceneManager.LoadScene("LavaScene");}

    public void goGallery(){SceneManager.LoadScene("Gallery");}

    public void Quit(){Application.Quit();}

    public int getSceneNum()
    {
        if (SceneManager.GetActiveScene().name == "TutorialScene") { return 0; }
        else if (SceneManager.GetActiveScene().name == "ForestScene") { return 1; }
        else if (SceneManager.GetActiveScene().name == "CandyScene") { return 2; }
        else if (SceneManager.GetActiveScene().name == "LavaScene") { return 3; }
        else { return -1; }
    }

}