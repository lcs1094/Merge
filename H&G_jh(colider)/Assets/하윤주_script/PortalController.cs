using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private GameObject stageManager;
    private bool allBread = false;
    private bool go = false;

    // Start is called before the first frame update
    void Start()
    {
        stageManager = GameObject.FindWithTag("StageManager");

    }

    // Update is called once per frame
    void Update()
    {
        allBread = stageManager.GetComponent<StageManager>().getAllBread();
        go = stageManager.GetComponent<StageManager>().getGo();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collision");
            if (allBread && go) { stageManager.GetComponent<StageManager>().goNextStage(); }
        }
    }
}
