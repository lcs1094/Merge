using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void goEndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

    public void goStage01()
    {
        SceneManager.LoadScene("Stage01");
    }

    public void goStage02()
    {
        SceneManager.LoadScene("Stage02");
    }

    public void goStage03()
    {
        SceneManager.LoadScene("Stage03");
    }

    public void goStage04()
    {
        SceneManager.LoadScene("Stage04");
    }

    public void goGallery()
    {
        SceneManager.LoadScene("Gallery");
    }

    public void Quit()
    {
        Application.Quit();
    }

}

