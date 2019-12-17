using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeperoFence : MonoBehaviour
{
    public GameObject fence;

    private float fPosX;
    private float fPosY;
    private float speed = 0.08f;
    private IEnumerator movement= null;

    // Start is called before the first frame update
    void Start()
    {
        fPosX = fence.transform.position.x;
        fPosY = fence.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (StageManager.instance.getAllMonster())
        {
            if (movement == null)
            {
                movement = moveFence();
                StartCoroutine(movement);
            }
        }
    }

    private IEnumerator moveFence()
    {
        while (fPosY > -4.5f)
        {
            fPosY -= speed;
            fence.transform.position = new Vector3(fPosX, fPosY, -5);
            yield return new WaitForSeconds(0.04f);
        }
    }

}
