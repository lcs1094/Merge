using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill2Controller : MonoBehaviour
{
    float skill_Ontime = 0.0f;
    float Skill2_timer = 2.0f;
    GameObject PlayerPos;
    float speed = 10.0f;
    Vector3 toplayer;
    // Start is called before the first frame update
    void Start()
    {
        this.PlayerPos = GameObject.FindGameObjectWithTag("Player");
        toplayer = (this.PlayerPos.transform.position-this.transform.position).normalized;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.tag =="Player"){ // 공격 스킬 피격판정시
        // col.transform.tag로 사용 가능, 단, tag 사용시 모든 스킬의 tag 변경할 것
            col.gameObject.GetComponent<PlayerController>().hit();
            //Stagemanager.instance.hit(20);    // 스테이지매니저.인스턴스이름.함수
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += toplayer * speed * Time.deltaTime; // <- skill_controller 스크립트로 이동할 코드부분!!!!
        skill_Ontime += Time.deltaTime;     // 스킬 생성 이후 Time Ticks
        if(skill_Ontime > Skill2_timer)     // 스킬이 생성된 이후 스킬 지속시간만큼 흘렀으면 (스킬 지속시간이 끝났으면)
            Destroy(gameObject);            // 스킬 오브젝트 파괴
    }
}
