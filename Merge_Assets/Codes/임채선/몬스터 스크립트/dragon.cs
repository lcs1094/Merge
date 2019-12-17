using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dragon : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb; // 오브젝트 물리속성
    public float speed = 1.0f; // 오브젝트 움직임 속도
    Vector3 movement; // 오브젝트 움직임 좌표
    //GameObject Throwing; // 주인공 오브젝트의 공격 스킬 오브젝트(복사해서 사용)
    public int maxHealth = 100; // 몬스터 최대 체력
    public float width = 2.0f;  // 몬스터 가로 크기
    public float height = 2.0f; // 몬스터 세로 크기
    public bool isDead;         // 몬스터 사망 판정 bool
    bool isHit = false;         // 몬스터 피격 판정 bool
    int Health;                 // 몬스터 현재 체력
    public float Tick_Timer = 2.0f;    // 체력 회복 타임
    private float Hit_Timer = 3.0f;     // 피격 해제 타임
    private float Tick_Time = 0.0f;     // 체력 회복 타이머
    private float Hit_Time = 0.0f;      // 피격 해제 타이머
    bool isdetect=false;                // Player 발견 bool
    GameObject detectedTarget;          // Target 게임오브젝트
    public GameObject portion;          // Drop 아이템
    public float skill_Active = 5.0f;   // 스킬 발동 타임
    float skill_timer = 0.0f;           // 스킬 발동 타이머
    public GameObject skill2;           // 스킬 오브젝트
    Vector3 moveVelocity = Vector3.zero;    // 움직임 방향 벡터
    Vector3 M_Zone;                     // 몬스터 이동 범위 벡터
    GameObject My_SpwanZone;            // 몬스터 이동 범위 오브젝트

    public void damage(int dmg){    // 피격 시 실행할 데미지 함수
        this.Health -= dmg;         // 데미지만큼 체력 
    }
    public void Set_Zone(Vector3 Zone){ // 몬스터 이동 범위 설정 함수
        this.M_Zone = Zone;
    }
    public void Set_SpwanZone(GameObject Zone){ // 몬스터 이동 범위 오브젝트 설정 함수
        this.My_SpwanZone = Zone;
        Set_Zone(My_SpwanZone.transform.position);
    }
    public void Zone_Out(){     // 이동 범위를 벗어나면 호출할 함수
        if(isdetect){
            moveVelocity = Vector3.zero;    // detect 상태이면 움직임 멈춤
        }
        else{
            moveVelocity = (M_Zone-this.transform.position).normalized;
            // detect가 아니면 오브젝트의 중심 위치로의 단위 방향 벡터로 움직임 벡터 설정
        }
    }
    void move(){    // 움직임 함수
        transform.position += moveVelocity * speed * Time.deltaTime;
        // 매 프레임단위 시간마다 speed * 이동 방향 단위벡터만큼 이동
        if(moveVelocity.x > 0){
            transform.localScale = new Vector3(-width, height, 1);
            // 우로 이동할 시 우측을 보도록 설정
        }
        else{
            transform.localScale = new Vector3(width, height, 1);
            // 좌로 이동할 시 좌측을 보도록 설정
        }
    }
    void Die(){ // 몬스터 사망 처리 함수
        isDead = true;  // 사망 bool을 true로 설정
        GameDirector.instance.killMonster();
        My_SpwanZone.GetComponent<Dragon_Spwanzone>().Monster_State(isDead);    // 스폰존의 스크립트의 Monster_State를 isDead를 인자로 실행
        int drop = Random.Range(0,10);  // drop은 0~9까지 랜덤 정수
        if(drop >= 8){                  // drop이 8 이상이면
            GameObject item = Instantiate(portion) as GameObject;   // portion으로 지정한 오브젝트를 item 오브젝트로 생성
            item.transform.position = this.transform.position - moveVelocity;   // item의 위치는 현재 몬스터의 위치에서 이동 방향만큼을 빼 준 Vector
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
        if(col.gameObject.tag == "Player"){ // 트리거에 들어온 gameObject의 tag가 Player 이면
            detectedTarget = col.gameObject;    // detectedTarget을 gameObject로 설정
        }
    }
    void OnTriggerStay2D(Collider2D col){   // 추적중 함수
        if(col.gameObject.tag == "Player"){ // 위와 동일
            isdetect = true;                // isdetect를 true로 설정
        }
    }
    void OnTriggerExit2D (Collider2D col){  // 추적 종료 함수, 난수이동 Coroutine 시작
        if(col.gameObject.tag == "Player"){ // 위와 동일
            isdetect = false;               // isdetect를 false로 설정
            moveVelocity = (M_Zone-this.transform.position).normalized; // 몬스터의 이동 벡터를 이동 범위 오브젝트의 중심좌표로 설정하고 단위벡터로 설정
        }
    }
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();  // 오브젝트의 리지드바디를 설정한 리지드바디2d로 설정
        this.Health = maxHealth;                // 몬스터의 체력을 최대 체력으로 설정
        isDead = false;                         // isDead를 false로 설정
        moveVelocity = (M_Zone-this.transform.position).normalized; // 몬스터의 이동 벡터를 이동 범위 오브젝트의 중심좌표로 설정하고 단위벡터로 설정
        if(moveVelocity == Vector3.zero)        // 이동 벡터가 zero이면
            moveVelocity = new Vector3(1,0,0);  // 이동 벡터를 (1,0,0)으로 설정
        My_SpwanZone.GetComponent<Dragon_Spwanzone>().Monster_State(isDead);    // 위와 동일
        animator = this.GetComponent<Animator>();                               // 오브젝트의 애니메이터를 부착한 애니메이터로 설정
    }
    void FixedUpdate(){
        move();             // move() 함수 실행
        if(Health <= 0){    // 체력이 0이하이면 종료
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Health > maxHealth){ // 체력이 최대 체력보다 크면
            Health = maxHealth; // 체력을 최대 체력으로 설정
        }
        if(Health <= 0){        // 체력이 0 이하이면
            if(!isDead)         // isDead가 false이면
                Die();          // Die 함수 실행
                return;         // Update 종료
        }
        if(!isHit){             // isHit가 false이면
            Tick_Time += Time.deltaTime;    // 체력 회복 타이머 시작
            if(Tick_Time > Tick_Timer){     // 체력 회복 타이머가 회복 시간보다 커지면
                Health += 2;                // 체력을 2씩 회복
                Tick_Time = 0.0f;           // 체력 회복 타이머를 0으로 초기화
            }
        }
        else{
            Hit_Time += Time.deltaTime;     // isHit가 true이면
            Tick_Time = 0.0f;               // 회복 타이머를 0으로 설정
            if(Hit_Time > Hit_Timer){       // 피격 타이머가 피격 시간보다 커지면
                isHit = false;              // isHit를 false로 설정
                Hit_Time = 0.0f;            // 피격 타이머를 0으로 설정
            }
        }
        animator.SetFloat("Skill", skill_timer);    // 매 프레임마다 animator의 Float를 skill_timer로 설정
        if(isdetect){                               // isdetect가 true이면
            Vector3 playerPos = detectedTarget.transform.position;  // PlayerPos를 detectedTarget의 position으로 설정
            moveVelocity = (playerPos - this.transform.position).normalized;    // 이동 벡터를 PlayerPos로의 단위 벡터로 설정
            skill_timer += Time.deltaTime;  // 스킬 발동을 위한 시간 측정
            if(skill_timer > skill_Active){ // 스킬 발동을 위한 시간이 쿨타임만큼 지나면
                skill_timer = 0.0f;         // 시간 변수 초기화
                GameObject skill = Instantiate(skill2) as GameObject;   // 스킬 오브젝트 프리팹으로 생성
                skill.transform.position = this.transform.position;    // 스킬의 위치는 몬스터 오브젝트의 위치로 생성
            }
        }
    }
}