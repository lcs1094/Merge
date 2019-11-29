using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;           // "Player" 오브젝트를 Transform으로 가지고 옴
    private Transform cameraPos;        // 카메라의 Transform p   <- 좌표값을 가지기 위해 gameObject가 아닌 Transform으로 생성
    private Vector3 newPos;
    private float posX;
    private float posY;
    private GameObject stageManager;
    private bool isHansel;
    public GameObject Hansel;
    public GameObject Gretel;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GetComponent<Transform>();  // 카메라에 Transform 컴포넌트 적용
        this.stageManager = GameObject.FindWithTag("StageManager");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        getnewPos();
        cameraPos.position = newPos;
    }

    void getnewPos()
    {
        nowPlayer();
        if (player.position.x < -43.5f) { posX = -43.5f; }
        else if (player.position.x > 56.5f) { posX = 56.5f; }
        else { posX = player.position.x; }
        if (player.position.y > 1) { posY = 1; }
        else if (player.position.y < -1) { posY = -1; }
        else { posY = player.position.y; }

        newPos = new Vector3(posX, posY, -10);

    }

    void nowPlayer()
    {
        isHansel = stageManager.GetComponent<StageManager>().getIsHansel();
        if (isHansel) { player = Hansel.transform; }
        else { player = Gretel.transform; }
    }
}
