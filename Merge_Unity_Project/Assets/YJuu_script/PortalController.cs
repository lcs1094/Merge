using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private bool allBread = false;
    private bool allMonster = false;
    private bool go = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            allBread = StageManager.instance.getAllBread();
            allMonster = StageManager.instance.getAllMonster();
            go = StageManager.instance.getGo();

            if (allBread && allMonster && go) { StageManager.instance.goNextStage(); }
        }
    }
}
