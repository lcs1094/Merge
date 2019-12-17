using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour
{
    public GameObject title;
    public GameObject black;
    public GameObject Canvas;
    private float tPosX;
    private float bPosX;
    private float tPosY;
    private float bPosY;

    public float time = 0.04f;
    public float speed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        bPosX = black.transform.position.x;
        bPosY = black.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void firstStartFunc()
    {
        Canvas.SetActive(false);
        title.SetActive(false);
        GameManager.instance.firstStart = true;
        GameManager.instance.lastHeart = 0;
      
        StartCoroutine("move");
    }

    public IEnumerator move()
    {
        while (bPosX > -14)
        {
            bPosX -= speed;
            black.transform.position = new Vector3(bPosX, bPosY, 0);
            yield return new WaitForSecondsRealtime(time);
        }
        
        GameManager.instance.goTutorialScene();
    }
}
