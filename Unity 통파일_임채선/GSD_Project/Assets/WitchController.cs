using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WitchController : MonoBehaviour
{
    Rigidbody2D rb; // 오브젝트 물리속성
    float speed = 3.0f; // 오브젝트 움직임 속도
    float jumpforce = 0.0f;
    Vector3 movement; // 오브젝트 움직임 좌표
    //GameObject Throwing; // 주인공 오브젝트의 공격 스킬 오브젝트(복사해서 사용)
    int movementFlag =0; // 0: idle, 1: left, 2: right, 3: Jump
    //추후 9방향으로 추가
    GameObject traceTarget; // 추적할 대상 오브젝트
    bool isTracing;
    public string dist = "";

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (isTracing)
        {
            Vector3 playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
                dist = "Left";
            else if (playerPos.x > transform.position.x)
                dist = "Right";
        }
        else
        {
            if (movementFlag == 1)
                dist = "Left";
            else if (movementFlag == 2)
                dist = "Right";
        }

        if (dist=="Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dist=="Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        };

        transform.position += moveVelocity * speed * Time.deltaTime;
    }
    /*void jump(){
        //rb.velocity = Vector2.zero;
        //Vector2 jumpVelocity = new Vector2 (0,jumpforce);
        //rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        if(movementFlag == 3){
            this.rb.AddForce(transform.up * this.jumpforce);
        }
    }*/
    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            traceTarget = col.gameObject;
            StopCoroutine("ChangeMove");
        }
    }
    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            isTracing = true;
        }
    }
    void OnTriggerExit2D (Collider2D col){
        if(col.gameObject.tag == "Player"){
            isTracing = false;
            StartCoroutine("ChangeMove");
        }
    }
    /*
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.name =="Throwing"){ // 공격 스킬 피격판정시
            //피격시 실행할 코드
        }
    }
    */
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        StartCoroutine("ChangeMove");
    }

    //코루틴 부분
    IEnumerator ChangeMove(){
        movementFlag = Random.Range(0,3);
        yield return new WaitForSeconds(3.0f);
        StartCoroutine("ChangeMove");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        Move();
        //jump();
    }
}
