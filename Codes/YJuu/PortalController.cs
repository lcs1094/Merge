using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private bool allBread = false;
    private bool go = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        allBread = StageManager.instance.getAllBread();
        go = StageManager.instance.getGo();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collision");
            if (allBread && go) { StageManager.instance.goNextStage(); }
        }
    }
}
