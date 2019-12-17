using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{

    private Vector3 defaultPos = new Vector3(0, 0, 0);
    private GameObject StageManager;
    public GameObject Hansel;

    // Start is called before the first frame update
    void Start()
    {
        StageManager = GameObject.FindWithTag("StageManager");
        Hansel.transform.position = defaultPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
