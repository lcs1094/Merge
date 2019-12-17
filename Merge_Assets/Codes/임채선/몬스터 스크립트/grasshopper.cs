using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grasshopper : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb; // 오브젝트 물리속성
    public float speed = 1.0f; // 오브젝트 움직임 속도
    Vector3 movement; // 오브젝트 움직임 좌표
    //GameObject Throwing; // 주인공 오브젝트의 공격 스킬 오브젝트(복사해서 사용)
    public int maxHealth = 60;
    public float width = 2.0f;
    public float height = 2.0f;
    public bool isDead;
    bool isHit = false;
    int Health;
    private float Tick_Timer = 2.0f;
    private float Hit_Timer = 3.0f;
    private float Tick_Time = 0.0f;
    private float Hit_Time = 0.0f;
    bool isdetect=false;
    GameObject detectedTarget;
    public GameObject portion;
    public float skill_Active = 3.0f;
    float skill_timer = 0.0f;
    Vector3 moveVelocity = Vector3.zero;
    Vector3 M_Zone;
    GameObject My_SpwanZone;

    public void damage(int dmg){    // 피격 시 실행할 데미지 함수
        this.Health -= dmg;
    }
    public void Set_Zone(Vector3 Zone){
        this.M_Zone = Zone;
    }
    public void Set_SpwanZone(GameObject Zone){
        this.My_SpwanZone = Zone;
        Set_Zone(My_SpwanZone.transform.position);
    }
    public void Zone_Out(){
        if(isdetect){
            moveVelocity = Vector3.zero;
        }
        else{
            moveVelocity = (M_Zone-this.transform.position).normalized;
        }
    }
    void move(){
        transform.position += moveVelocity * speed * Time.deltaTime;
        if(moveVelocity.x > 0){
            transform.localScale = new Vector3(-width, height, 1);
        }
        else{
            transform.localScale = new Vector3(width, height, 1);
        }
    }
    void Die(){
        isDead = true;
        My_SpwanZone.GetComponent<Grasshopper_Spwanzone>().Monster_State(isDead);
        int drop = Random.Range(0,10);
        if(drop >= 8){
            GameObject item = Instantiate(portion) as GameObject;
            item.transform.position = this.transform.position - moveVelocity;
        }
        Destroy(this.gameObject); // 몬스터 오브젝트 삭제
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.tag == "Player"){
            this.Health -= 10;
            isHit = true;
            Debug.Log(this.Health);
        }
        if(col.transform.tag == "HSkill01"){
            this.Health -= 50;
            isHit = true;
        }
        if(col.transform.tag == "HSkill02"){
            this.Health -= 100;
            isHit = true;
        }
        if(col.transform.tag == "GSkill01"){
            this.Health -= 35;
            isHit = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col){  // Player 태그를 가진 콜라이더와 충돌시 추적 타겟을 설정
        if(col.gameObject.tag == "Player"){
            detectedTarget = col.gameObject;
        }
    }
    void OnTriggerStay2D(Collider2D col){   // 추적중 함수
        if(col.gameObject.tag == "Player"){
            isdetect = true;
        }
    }
    void OnTriggerExit2D (Collider2D col){  // 추적 종료 함수, 난수이동 Coroutine 시작
        if(col.gameObject.tag == "Player"){
            isdetect = false;
            moveVelocity = (M_Zone-this.transform.position).normalized;
        }
    }
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.Health = maxHealth;
        isDead = false;
        moveVelocity = (M_Zone-this.transform.position).normalized;
        if(moveVelocity == Vector3.zero)
            moveVelocity = new Vector3(1,0,0);
        My_SpwanZone.GetComponent<Grasshopper_Spwanzone>().Monster_State(isDead);
        animator = this.GetComponent<Animator>();
    }
    void FixedUpdate(){
        move();
        if(Health <= 0){
            return;
        }
        skill_timer += Time.deltaTime;  // 스킬 발동을 위한 시간 측정
        animator.SetFloat("Skill", skill_timer);
        if(skill_timer > skill_Active){ // 스킬 발동을 위한 시간이 쿨타임만큼 지나면
            this.rb.AddForce(transform.up * 200f);
            GetComponent<AudioSource>().Play();
            skill_timer = 0.0f;         // 시간 변수 초기화
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Health > maxHealth){ 
            Health = maxHealth;
        }
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
            Tick_Time = 0.0f;
            if(Hit_Time > Hit_Timer){
                isHit = false;
                Hit_Time = 0.0f;
            }
        }
        
        if(isdetect){
            Vector3 playerPos = detectedTarget.transform.position;
            moveVelocity = (playerPos - this.transform.position).normalized;
        }
    }
}