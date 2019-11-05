using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool[] endings = new bool[5];     //엔딩을 획득했는지 확인(true면 획득한 엔딩)


    void Awake()
    {
        DontDestroyOnLoad(gameObject);     //전체를 총괄하는 오브젝트이므로 사라지지 않음
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

    public void goEndingScene(){SceneManager.LoadScene("EndingScene");}

    public void goStage01(){SceneManager.LoadScene("Stage01");}

    public void goStage02(){SceneManager.LoadScene("Stage02");}

    public void goStage03(){SceneManager.LoadScene("Stage03");}

    public void goStage04(){SceneManager.LoadScene("Stage04");}

    public void goGallery(){SceneManager.LoadScene("Gallery");}

    public void Quit(){Application.Quit();}

}

