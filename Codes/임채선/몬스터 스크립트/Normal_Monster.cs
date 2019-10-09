using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Normal_Monster : MonoBehaviour
{
    Rigidbody2D rb; // 오브젝트 물리속성
    float speed = 3.0f; // 오브젝트 움직임 속도
    float jumpforce = 0.0f;
    Vector3 movement; // 오브젝트 움직임 좌표
    //GameObject Throwing; // 주인공 오브젝트의 공격 스킬 오브젝트(복사해서 사용)
    int movementFlag =0; // 0: idle, 1: left, 2: right, 3: Jump
    //추후 9방향으로 추가
    public int maxHealth = 100;
    bool isDead = false;
    bool isHit = false;
    int Health = 3;
    private float Tick_Timer = 2.0f;
    private float Hit_Timer = 3.0f;
    private float Tick_Time = 0.0f;
    private float Hit_Time = 0.0f;
    GameObject player;

    [SerializeField]
    private Text HPText;

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if (movementFlag == 1){
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementFlag == 2){
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        transform.position += moveVelocity * speed * Time.deltaTime;
    }
    void Die(){
        isDead = true;
        //rb.velocity = Vector2.zero;
        //Collider2D[] colls = gameObject.GetComponents<Collider2D>();
        //for(int i=0;i<colls.Length;i++)
            //colls[i].enabled = false; // 몬스터의 모든 콜라이더 끄기
        Destroy(this.gameObject); // 몬스터 오브젝트 삭제
    }
    /*void jump(){
        //rb.velocity = Vector2.zero;
        //Vector2 jumpVelocity = new Vector2 (0,jumpforce);
        //rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        if(movementFlag == 3){
            this.rb.AddForce(transform.up * this.jumpforce);
        }
    }*/
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.name =="player"){ // 공격 스킬 피격판정시
        // col.transform.tag로 사용 가능, 단, tag 사용시 모든 스킬의 tag 변경할 것
            Health -= 10; // 피격물체에 따른 체력 감소
            isHit = true;
        }
    }
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        StartCoroutine("ChangeMove");
        Health = maxHealth;
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
        if(Health <= 0){
            return;
        }
        //jump();
    }
    void Update(){
        if(Health > maxHealth){ 
            Health = maxHealth;
        }
        HPText.text = Health + "/" + maxHealth;
        if(Health <= 0){
            if(!isDead)
                Die();
                return;
        }
        if(!isHit){
            Tick_Time += Time.deltaTime;
            if(Tick_Time > Tick_Timer){
                Health += 2;
                Tick_Time = 0.0f;
            }
        }
        else{
            Hit_Time += Time.deltaTime;
            if(Hit_Time > Hit_Timer){
                isHit = false;
                Hit_Time = 0.0f;
            }
        }
    }
}