using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //player의 움직임에 대한 클래스

    Rigidbody2D rigid2D;          //물리엔진을 통해 움직임 제어 → Rigidbody2D객체생성(힘 계산)
    float jumpForce = 780.0f;     //점프할 때 가해지는 힘
    float walkForce = 50.0f;      //걸을 때 가해지는 힘
    float maxWalkSpeed = 3.0f;    //걸을때의 최고속도
    int life = 3;                 //player의 생명(3개)
    int status = 0;
    GameObject director;          //GameDirector의 함수를 사용할것이므로 객체 생성
    GameObject manager;           //GameManager의 함수를 사용할 것이므로 객체 생성
    Vector3 defaultPos = new Vector3(-8, -3, 0);       //player의 기본위치 

    void Start () {
        this.rigid2D = GetComponent<Rigidbody2D>();           //rigid2D변수에 Rigidbody2D컴포넌트 연결
        this.director = GameObject.Find("GameDirector");      //director변수에 GameDirector연결
        this.manager = GameObject.Find("GameManager");        //manager변수에 GameManager연결
    }
	
	void Update () {
        lifeFunc();     //생명과 관련된 함수
        status = director.GetComponent<GameDirector>().getStatus();

        if (Input.GetKeyDown(KeyCode.UpArrow)&&Mathf.Abs(this.rigid2D.velocity.y)<0.0001)       //위쪽 방향키가 눌리고 현재 y축속도의 절대값이 0.0001보다 작은 경우
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);                               //위쪽으로 가하는 힘을 증가
        }

        if (Input.GetKeyDown(KeyCode.Space))  manager.GetComponent<GameManager>().goGameOverScene();     //Space가 눌리는 경우 GameOverScene으로 이동

        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;       //오른쪽 방향키가 눌리면 key값을 1로 설정
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;       //왼쪽 방향키가 눌리면 key값을 -1로 설정

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);   //현재 x축 속도의 절대값을 speedx에 저장

        if(speedx < this.maxWalkSpeed)  this.rigid2D.AddForce(transform.right * key * this.walkForce);  //speedx가 최대속도보다 작은 경우 오른쪽으로 가하는 힘을 증가(key가 -1인경우 왼쪽으로)

        if(key != 0)  transform.localScale = new Vector3(key, 1, 1);  //key값(눌린 방향키가 어떤 방향키인지)에 따라 player의 모양을 바꿈
    }

    //player의 생명과 관련된 함수
    void lifeFunc()
    {
        if (transform.position.y < -6)                     //player가 낙하해서 화면 아래쪽으로 나가는 경우를 처리
        {
            if(status == 1) {
                transform.localPosition = new Vector3(transform.position.x, 5, 0);
                director.GetComponent<GameDirector>().setStatus(0);
            }
            transform.localPosition = defaultPos;          //player를 기본위치로 이동
            director.GetComponent<GameDirector>().invisibleHeart(life - 1);          //director의 invisibleHeart함수를 이용해 UI에 보이는 하트를 하나 지움
            life--;                                        //player의 생명을 하나 줄임

            if (life == 0) manager.GetComponent<GameManager>().goGameOverScene();  //player의 생명이 0이 되면 manager의 goGameOverScene함수를 이용해 GameOverScene으로 이동
        }
    }
}
