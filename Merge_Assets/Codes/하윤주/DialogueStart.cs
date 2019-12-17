using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public GameObject director;
    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (first)
            {
                col.GetComponent<PlayerController1>().canMove = false;
                director.GetComponent<DialogueDirector>().setCondition();
                director.GetComponent<DialogueDirector>().chatFunc(1);
                first = false;
                StartCoroutine(check(col));
            }
        }
    }

    private IEnumerator check(Collider2D col)
    {
        while (true)
        {
            if (director.GetComponent<DialogueDirector>().getDialogueEnd())
            {
                col.GetComponent<PlayerController1>().canMove = true;
                yield break;
            }
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
}
