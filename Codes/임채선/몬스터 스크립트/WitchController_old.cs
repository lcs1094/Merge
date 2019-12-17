using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WitchController_old : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 3.0f; // 오브젝트 움직임 속도
    Vector3 movement; // 오브젝트 움직임 좌표
    //GameObject Throwing; // 주인공 오브젝트의 공격 스킬 오브젝트(복사해서 사용)
    int movementFlag =0; // 0: idle, 1: left, 2: right, 3: down, 4: up
    GameObject traceTarget; // 추적할 대상 오브젝트
    bool isTracing;
    public string dist = "";
    public Vector3 PlayerScale;
    public Vector3 playerPos;
    float skill_Active = 3.0f;  // 스킬 쿨타임
    float skill_timer = 0.0f;   // 시간 측정용 변수
    public GameObject Skill1;
    public GameObject Monster;
    float monster_Active = 15.0f;
    float monster_timer = 0.0f;
    private int maxHealth = 20;
    private int Health;
    public bool isDead;
    public float width = -2.0f;
    public float height = 2.0f;
    bool isHit = false;
    private float Tick_Timer = 2.0f;    // 체력 회복 간격
    private float Hit_Timer = 3.0f;     // 피격 판정 시간
    private float Tick_Time = 0.0f;
    private float Hit_Time = 0.0f;
    private float Death_Timer = 2.0f;   // 2페이즈 돌입 시 대기 시간
    private float Death_Time = 0.0f;
    public GameObject Witch_2;
    Vector3 M_Zone;
    GameObject My_SpwanZone;
    Vector3 moveVelocity = Vector3.zero;
    private bool onskill = false;
    private void Witch1_Dead(){
        GameObject Witch2 = Instantiate(Witch_2) as GameObject;
        Witch2.transform.position = this.transform.position;
    }
    public void Set_Zone(Vector3 Zone){
        this.M_Zone = Zone;
    }
    public void Set_SpwanZone(GameObject Zone){
        this.My_SpwanZone = Zone;
        Set_Zone(My_SpwanZone.transform.position);
    }
    public void Zone_Out(){
        if(isTracing){
            moveVelocity = Vector3.zero;
        }
        else{
            moveVelocity = (M_Zone-this.transform.position).normalized;
        }
    }
    public void damage(int dmg){    // 피격 시 실행할 데미지 함수
        this.Health -= dmg;
    }
    
    void Move()     // Witch의 움직임 함수
    {
        if (isTracing)
        {
            playerPos = traceTarget.transform.position;

            if (playerPos.x < transform.position.x)
            {
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(width,height,1);
            }
            else if (playerPos.x > transform.position.x)
            {
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(-width,height,1);
            }
            if (playerPos.y < transform.position.y)
                moveVelocity = Vector3.down;
            else if (playerPos.y > transform.position.y)
                moveVelocity = Vector3.up;
        }
        else
        {
            if (movementFlag == 1)
            {
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(width, height, 1);
            }
            else if (movementFlag == 2)
            {
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(-width, height, 1);
            }
            else if (movementFlag == 3)
                moveVelocity = Vector3.down;
            else if (movementFlag == 4)
                moveVelocity = Vector3.up;
        }

        transform.position += moveVelocity * speed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.name =="player"){ // 공격 스킬 피격판정시
        // col.transform.tag로 사용 가능, 단, tag 사용시 모든 스킬의 tag 변경할 것
            Health -= 10; // 피격물체에 따른 체력 감소
            isHit = true;
        }
    }
    void OnTriggerEnter2D(Collider2D col){  // Player 태그를 가진 콜라이더와 충돌시 추적 타겟을 설정
        if(col.gameObject.tag == "Player"){
            traceTarget = col.gameObject;
            StopCoroutine("ChangeMove");
        }
    }
    void OnTriggerStay2D(Collider2D col){   // 추적중 함수
        if(col.gameObject.tag == "Player"){
            isTracing = true;
        }
    }
    void OnTriggerExit2D (Collider2D col){  // 추적 종료 함수, 난수이동 Coroutine 시작
        if(col.gameObject.tag == "Player"){
            isTracing = false;
            StartCoroutine("ChangeMove");
        }
    }
    void Die(){
        isDead = true;
        Witch1_Dead();
        Destroy(this.gameObject); // 몬스터 오브젝트 삭제
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("ChangeMove");
        Health = maxHealth;
        isDead = false;
    }

    //코루틴 부분
    IEnumerator ChangeMove(){
        movementFlag = Random.Range(0,5);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("ChangeMove");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Health > maxHealth){ 
            Health = maxHealth;
        }
        if(Health <= 0){
            if(!isDead){
                GetComponent<ParticleSystem>().Play();
                Death_Time += Time.deltaTime;
                if(Death_Time >= Death_Timer)
                    Die();
            }
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
        if(!onskill){
            Move();
        }
        if(isTracing){
            skill_timer += Time.deltaTime;  // 스킬 발동을 위한 시간 측정
            if(!isTracing)
                skill_timer = 0.0f;
            if(skill_timer > skill_Active){ // 스킬 발동을 위한 시간이 쿨타임만큼 지나면
                skill_timer = 0.0f;         // 시간 변수 초기화
                GameObject skill = Instantiate(Skill1) as GameObject;   // 스킬 오브젝트 프리팹으로 생성
                skill.transform.position = this.transform.position;    // 스킬의 위치는 Witch 오브젝트의 위치로 생성
        }
        }
        else{
            monster_timer += Time.deltaTime;
            if(isTracing)
                monster_timer = 0.0f;
            if(monster_timer > monster_Active){
                monster_timer = 0.0f;
                GameObject Monsters = Instantiate(Monster) as GameObject;
                int rnadomPosX = Random.Range(-3,3);
                int rnadomPosY = Random.Range(-3,3);
                Monsters.transform.position = new Vector3(this.transform.position.x+rnadomPosX,this.transform.position.y+rnadomPosY,this.transform.position.z);
            }
        }
    }
}
