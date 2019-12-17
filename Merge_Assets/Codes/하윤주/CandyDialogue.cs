using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDialogue : MonoBehaviour
{
    public GameObject director;
    private bool first = true;
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

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            allBread = StageManager.instance.getAllBread();
            allMonster = StageManager.instance.getAllMonster();
            go = StageManager.instance.getGo();
            if (allBread && allMonster && go)
            {
                director.GetComponent<DialogueDirector>().setCondition();
                director.GetComponent<DialogueDirector>().chatFunc(2);
                StartCoroutine(waitForDialogue());
            }
        }
    }

    private IEnumerator waitForDialogue()
    {
        while (true)
        {
            if (director.GetComponent<DialogueDirector>().getDialogueEnd())
            {
                StageManager.instance.goNextStage();
                yield break;
            }
            yield return null;
        }
    }
}
