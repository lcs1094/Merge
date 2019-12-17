using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    private GameObject Director;

    // Start is called before the first frame update
    void Start()
    {
        Director = GameObject.FindWithTag("GameDirector");
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            Director.GetComponent<GameDirector>().addBread();
            col.gameObject.GetComponent<PlayerSound>().breadSound();
            Destroy(gameObject);
        }
    }
}
