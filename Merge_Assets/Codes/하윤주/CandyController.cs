using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CandyScene에서 사탕의 움직임을 담당하는 스크립트
public class CandyController : MonoBehaviour
{
    public float headY = 0;       //사탕 머리 쪽의 끝 Y좌표
    public float tailY = 0;       //사탕 막대 쪽의 끝 Y좌표
    public bool UP = true;        //사탕이 위를 보고있는지
    
    private int fast = 0;         //다음 상태를 판별하는 변수   -1:느림, 0:정지, 1:빠름
    private int now = -1;         //현재 상태를 저장하는 변수   -1:느림, 0:정지, 1:빠름
    private IEnumerator moveCoroutine = null;  //Coroutine을 저장할 moveCoroutine변수

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moveCoroutine == null)           //실행중인 코루틴이 없다면(한번만 실행하기 위해서)
        {
            moveCoroutine = candyMove();     //candyMove코루틴을 moveCoroutine에 저장
            StartCoroutine(moveCoroutine);   //moveCoroutine실행
        }
    }

    //사탕의 움직임을 담당하는 코루틴
    private IEnumerator candyMove()
    {
        while (true)                                        //무한루프 실행
        {
            if (fast == 1)                                  //다음에 빠른 움직임이 필요한 상태라면
            {
                candyFast();                                //빠르게 움직이는 함수 호출
                yield return new WaitForSeconds(0.01f);     //0.01f에 한번씩 while문 확인
            }
            else if (fast == -1)                            //다음에 느린 움직임이 필요한 상태라면
            {
                candySlow();                                //느리게 움직이는 함수 호출
                yield return new WaitForSeconds(0.01f);     //0.01f에 한번씩 while문 확인
            }
            else                                            //fast가 -1또는 1이 아니면(다음에 정지해야한다면)
            {
                if (now == 1) { fast = -1; }                //현재 상태가 빠르게 움직이는 상태였다면 다음 상태는 느린 움직임
                else if (now == -1) { fast = 1; }           //현재 상태가 느리게 움직이는 상태였다면 다음 상태는 빠른 움직임
                yield return new WaitForSeconds(0.3f);      //정지상태로 0.3f대기
            }
        }
    }

    //빠르게 움직이는 함수
    private void candyFast()
    {
        now = 1;                                                //현재 상태에 1저장
        if (UP)                                                 //위를 보고 있는 사탕이면
        {
            transform.Translate(0, 0.1f, 0);                    //Y좌표를 0.1f증가
            if (transform.position.y >= headY) { fast = 0; }    //Y좌표가 머리 쪽의 끝 Y좌표에 도달하였으면 다음 상태는 정지
        }
        else                                                    //아래를 보고 있는 사탕이면
        {
            transform.Translate(0, -0.1f, 0);                   //Y좌표를 0.1f감소
            if (transform.position.y <= headY) { fast = 0; }    //Y좌표가 머리 쪽의 끝 Y좌표에 도달하였으면 다음 상태는 정지
        }
    }

    //느리게 움직이는 함수
    private void candySlow()
    {
        now = -1;                                              //현재 상태에 -1저장
        if (UP)                                                //위를 보고 있는 사탕이면

        {
            transform.Translate(0, -0.05f, 0);                 //Y좌표를 0.05f감소
            if (transform.position.y <= tailY) { fast = 0; }   //Y좌표가 막대 쪽의 끝 Y좌표에 도달하였으면 다음 상태는 정지
        }
        else                                                   //아래를 보고 있는 사탕이면
        {
            transform.Translate(0, 0.05f, 0);                 //Y좌표를 0.05f증가
            if (transform.position.y >= tailY) { fast = 0; }  //Y좌표가 막대 쪽의 끝 Y좌표에 도달하였으면 다음 상태는 정지
        }
    }
}
