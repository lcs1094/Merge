using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Witch2Controller : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 3.0f; // 오브젝트 움직임 속도
    GameObject traceTarget; // 추적할 대상 오브젝트
    bool isTracing;
    public Vector3 PlayerScale;
    public Vector3 playerPos;
    float skill_Active = 10.0f;  // 스킬 쿨타임
    float skill_timer = 0.0f;   // 시간 측정용 변수
    int toss;
    float skill2_ttime = 0.0f;
    public GameObject Skill1;
    public GameObject Skill2;
    public GameObject Skill3;
    float monster_Active = 7.5f;
    float monster_timer = 0.0f;
    private int maxHealth = 1000;
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
    bool isout = false;
    private int shotpos;
    private int skillcount = 0;
    bool half_health = true;
    bool quarter_health = true;
    public GameObject DeathAnime;
    void diemotion(){
        GameObject DeathAnim = Instantiate(DeathAnime) as GameObject;
        DeathAnim.transform.localScale = new Vector3(20,20,0);
        DeathAnim.transform.position = this.transform.position;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.name =="player"){ // 공격 스킬 피격판정시
        // col.transform.tag로 사용 가능, 단, tag 사용시 모든 스킬의 tag 변경할 것
            Health -= 10; // 피격물체에 따른 체력 감소
            isHit = true;
        }
        if(col.transform.tag == "HSkill01"){
            this.Health -= 60;
            isHit = true;
        }
        if(col.transform.tag == "HSkill02"){
            this.Health -= 100;
            isHit = true;
        }
        if(col.transform.tag == "GSkill01"){
            this.Health -= 35;
            isHit = true;
            Debug.Log(this.Health);
        }
    }
    void OnTriggerEnter2D(Collider2D col){  // Player 태그를 가진 콜라이더와 충돌시 추적 타겟을 설정
        if(col.gameObject.tag == "Player"){
            traceTarget = col.gameObject;
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
        }
    }
    void Die(){
        isDead = true;
        StopCoroutine(SkillShot());
        StageManager.instance.setKillWitch(true);
        Destroy(this.gameObject); // 몬스터 오브젝트 삭제
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = maxHealth;
        isDead = false;
        this.transform.position = new Vector3(12f, -6f,0);
        half_health = true;
        quarter_health = true;
    }

    //코루틴 부분
    IEnumerator SkillShot(){
        float wix = Random.Range(-5.0f,13f);
        GameObject skill = Instantiate(Skill1) as GameObject;   // 스킬 오브젝트 프리팹으로 생성
        skill.transform.position = new Vector3(wix,0.5f,0);
        skillcount += 1;
        Debug.Log(skillcount);
        if(skillcount > 5){
            yield break;
        }
        yield return new WaitForSecondsRealtime(1.0f);
        StartCoroutine(SkillShot());
    }
    void SkillShot2(){
        GameObject skill = Instantiate(Skill2) as GameObject;   // 스킬 오브젝트 프리팹으로 생성
        skill.transform.position = new Vector3(7f,-6.4f,0);
        skill.transform.localScale = new Vector3(-2f,0.5f,0);
    }
    void SkillShot3(){
        for(int i=0;i<8;i++){
            GameObject skillalarm = Instantiate(Skill3) as GameObject;
            skillalarm.transform.position = new Vector3(Random.Range(-5.0f,13f),-7f,0);
        }
    }
    // void Skill(){
    //     StartCoroutine(SkillShot());
    // }
    // Update is called once per frame
    void FixedUpdate()
    {
        skill_timer += Time.deltaTime;
        if(skill_timer > skill_Active){
            GetComponent<AudioSource>().Play();
            toss = Random.Range(0,2);
            Debug.Log(toss);
            if(toss  == 0){
                StartCoroutine(SkillShot());
            }
            else{
                SkillShot2();
            }
            skill_timer = 0;
        }
        if((Health < 500) && quarter_health){
            Debug.Log("Skill3");
            SkillShot3();
            quarter_health = false;
        }
        if((Health < 250) && half_health){
            Debug.Log("Skill3");
            SkillShot3();
            half_health = false;
        }
        // if(Health/maxHealth < 0.7){
        //     director.GetComponent<DialogueDirector>().setCondition();                
        //     director.GetComponent<DialogueDirector>().chatFunc(4);
        // }
        if(skillcount > 5){
            StopCoroutine(SkillShot());
            skillcount = 0;
        }
        if(Health > maxHealth){ 
            Health = maxHealth;
        }
        if(Health <= 0){
            if(!isDead){
                diemotion();
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
    }
}
