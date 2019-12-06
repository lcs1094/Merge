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
    private bool isHansel;
    public GameObject Hansel;
    public GameObject Gretel;
    public float leftX;
    public float rightX;
    public float upY;
    public float downY;


    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GetComponent<Transform>();  // 카메라에 Transform 컴포넌트 적용
    }

    // Update is called once per frame
    void Update()
    {
        getnewPos();
        cameraPos.position = newPos;
    }

    void getnewPos()
    {
        nowPlayer();
        if (player.position.x < leftX) { posX = leftX; }
        else if (player.position.x > rightX) { posX = rightX; }
        else { posX = player.position.x; }
        if (player.position.y > upY) { posY = upY; }
        else if (player.position.y < downY) { posY = downY; }
        else { posY = player.position.y; }

        newPos = new Vector3(posX, posY, -10);
        
    }

    void nowPlayer()
    {
        isHansel = StageManager.instance.getIsHansel();
        if (isHansel) { player = Hansel.transform; }
        else { player = Gretel.transform; }
    }
}
