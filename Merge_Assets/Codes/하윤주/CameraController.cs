using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라의 움직임을 담당하는 스크립트
public class CameraController : MonoBehaviour
{
    private Transform player;           //플레이어의 transform을 저장
    private Transform cameraPos;        //카메라의 transform을 저장
    private Vector3 newPos;             //카메라가 새로 이동할 위치
    private float posX;                 //새로 이동할 X좌표
    private float posY;                 //새로 이동할 Y좌표
    private bool isHansel;              //플레이어가 헨젤인지 여부
    public GameObject Hansel;           //실제 플레이어 GameObject 헨젤
    public GameObject Gretel;           //실제 플레이어 GameObject 그레텔
    public float leftX;                 //맵에서 카메라가 갈 수 있는 왼쪽 끝 X좌표
    public float rightX;                //맵에서 카메라가 갈 수 있는 오른쪽 끝 X좌표
    public float upY;                   //맵에서 카메라가 갈 수 있는 위쪽 끝 Y좌표
    public float downY;                 //맵에서 카메라가 갈 수 있는 아래쪽 끝 Y좌표


    // Start is called before the first frame update
    void Start()
    {
        cameraPos = GetComponent<Transform>();  //카메라의 Transform을 cameraPos에 저장
        getnewPos();                            //새로운 카메라 좌표를 얻어오는 함수
        cameraPos.position = newPos;            //카메라를 새로운 좌표로 이동
    }

    // Update is called once per frame
    void Update()
    {
        getnewPos();     //플레이어가 이동할때마다 따라 움직여야 하므로 update시마다 새로운 좌표를 얻어옴
    }

    void FixedUpdate()
    {
        cameraPos.position = Vector3.Lerp(cameraPos.position, newPos, Time.deltaTime * 2f);   //Vector3.Lerp를 이용해 부드럽게 카메라를 움직임
    }

    //새로운 카메라 좌표를 얻어오는 함수
    private void getnewPos()
    {
        nowPlayer();                                                 //현재 플레이 중인 플레이어를 찾음
        if (player.position.x < leftX) { posX = leftX; }             //플레이어의 X좌표가 카메라의 왼쪽 끝 X좌표보다 작으면 왼쪽 끝 X좌표를 카메라의 X좌표로 설정
        else if (player.position.x > rightX) { posX = rightX; }      //같은 방식으로 오른쪽 끝 X를 카메라의 X좌표로 설정(카메라가 맵 밖을 비추지 않도록)
        else { posX = player.position.x; }                           //왼쪽 끝과 오른쪽 끝 안쪽에 있으면 플레이어의 X좌표를 카메라의 X좌표로 설정
        if (player.position.y > upY) { posY = upY; }                 //X좌표와 같은 방식으로 위쪽 끝과 아래쪽 끝 내에서 카메라의 Y좌표 설정
        else if (player.position.y < downY) { posY = downY; }
        else { posY = player.position.y; }

        newPos = new Vector3(posX, posY, -10);                       //방금 구한 X좌표와 Y좌표로 카메라의 새로운 위치 설정
    }

    //현재 플레이어를 찾는 함수
    void nowPlayer()
    {
        isHansel = StageManager.instance.getIsHansel();     //StageManager에서 헨젤인지 여부를 받아옴
        if (isHansel) { player = Hansel.transform; }        //헨젤이면 헨젤의 transform을 player에 저장
        else { player = Gretel.transform; }                 //그레텔이면 그레텔의 transform을 player에 저장
    }
}
