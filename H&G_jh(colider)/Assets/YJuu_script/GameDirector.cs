using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private Vector3 defaultPosSt01 = new Vector3(0, 0, 0);
    private Vector3 defaultPosSt02 = new Vector3(-50, 0, 0);
    private GameObject stageManager;
    private GameObject player;
    private bool isHansel;
    private int stage;
    private bool sceneChanged;
    public GameObject Hansel;
    public GameObject Gretel;

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.FindWithTag("StageManager");
    }

    // Update is called once per frame
    void Update()
    {
        sceneStart();
    }

    void sceneStart()
    {
        sceneChanged = stageManager.GetComponent<StageManager>().getSceneChanged();
        if (sceneChanged)
        {
            nowPlayer();
            movePlayer();
            stageManager.GetComponent<StageManager>().setSceneChanged(false);
        }
    }

    void movePlayer()
    {
        stage = stageManager.GetComponent<StageManager>().getStage();
        Debug.Log(stage);
        if (stage == 0) { player.transform.position = defaultPosSt01; Debug.Log("st01"); }
        else if (stage == 1) { player.transform.position = defaultPosSt02; Debug.Log("st02"); }
    }

    void nowPlayer()
    {
        isHansel = stageManager.GetComponent<StageManager>().getIsHansel();
        if (isHansel) { player = Hansel; Debug.Log("Hansel"); }
        else { player = Gretel; Debug.Log("Gretel"); }
    }
}
