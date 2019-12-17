using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDirector : MonoBehaviour
{
    public GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        director.GetComponent<DialogueDirector>().setCondition();
        director.GetComponent<DialogueDirector>().chatFunc(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
