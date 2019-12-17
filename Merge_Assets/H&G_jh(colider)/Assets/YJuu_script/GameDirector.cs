using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Camera StartCamera;
    public Camera MainCamera;
    public GameObject Hansel;
    private float SCPosX;

    // Start is called before the first frame update
    void Start()
    {
        startFunc();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void startFunc()
    {
        if (GameManager.instance.firstStart)
        {
            setStartCamera();
            Hansel.GetComponent<PlayerController>().canMove = false;
            StartCoroutine("firstCameraMove");

        }
        else
        {
            setMainCamera();
        }
    }

    private IEnumerator firstCameraMove()
    {
        SCPosX = StartCamera.transform.position.x;
        while (SCPosX > -19)
        {
            SCPosX = StartCamera.transform.position.x - 0.1f;
            StartCamera.transform.position = new Vector3(SCPosX, 0, -10);
            yield return new WaitForSeconds(.02f);
        }
        setMainCamera();
        Hansel.GetComponent<PlayerController>().canMove = true;
    }

    private void setStartCamera()
    {
        MainCamera.enabled = false;
        StartCamera.enabled = true;
    }

    private void setMainCamera()
    {
        MainCamera.enabled = true;
        StartCamera.enabled = false;
    }
}
