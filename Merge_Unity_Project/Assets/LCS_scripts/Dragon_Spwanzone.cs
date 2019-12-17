using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Spwanzone : MonoBehaviour
{
    public bool isDead;
    float spwan_timer = 0f;     // 스폰 타이머로 설정
    float spwan_time = 15.0f;   // 스폰 시간을 15초로 설정
    bool istime = false;
    public GameObject Monster;  // 게임오브젝트 Monster 선언
    GameObject Spwaned_Monster; // 게임오브젝트 Swpaned_Monster 선언
    public void Monster_State(bool sd){ // 몬스터의 사망 판정 bool을 가지고 오는 Monster_State 함수
        this.isDead = sd;
    }
    void OnTriggerEnter2D(Collider2D col){      // 트리거에 들어오면
        if(col.gameObject.tag == "Monster"){    // 들어온 게임오브젝트의 tag가 Monster이면
            Spwaned_Monster = col.gameObject;   // Spwaned_Monster를 게임오브젝트로 설정
            col.gameObject.GetComponent<dragon>().Set_Zone(this.transform.position);    // 들어온 게임오브젝트의 Zone을 이 스크립트가 부착된 게임오브젝트의 position으로 설정
        }
    }
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Monster"){    // 몬스터 태그의 오브젝트가 트리거에서 나가면
            //Debug.Log("Zone Out!");
            col.gameObject.GetComponent<dragon>().Zone_Out();   // 해당 오브젝트의 Zone_Out 함수 실행
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float x = Random.Range(-1.0f,1.0f);     // float x는 -1.0이상, 1.0 미만의 실수
        Vector3 pos = this.transform.position;  // pos는 이 오브젝트의 position
        pos.x += x;                             // pos의 x좌표에 x값을 더함
        GameObject Spwan_Monster = Instantiate(Monster) as GameObject;  // Monster 오브젝트를 Spwaned_Monster 오브젝트로 생성
        Spwan_Monster.transform.position = pos;                         // Spwaned_Monster의 위치를 pos좌표로 생성
        Spwan_Monster.GetComponent<dragon>().Set_SpwanZone(this.gameObject);    // Spwaned_Monster의 Set_SpwanZone 함수를 이 오브젝트를 인자로 실행
        isDead = false;                         
    }

    // Update is called once per frame
    void Update()
    {
        // spwan_timer += Time.deltaTime;          // 매 프레임마다 spwan_timer에 시간을 더함
        // if(spwan_timer > spwan_time){           // 소환 타이머가 소환 시간보다 커지면
        //     istime = true;                      // istime을 true로 설정
        //     spwan_timer = 0f;                   // spwan_timer를 0으로 초기화
        // }
        // else{
        //     istime = false;                     // 작으면 istime을 false로 설정
        // }
        // if(isDead && istime){                   // isDead이면서 istime이면
        //     GameObject Spwan_Monster = Instantiate(Monster) as GameObject;  // 새로운 몬스터 생성
        //     Spwan_Monster.transform.position = this.transform.position;
        //     isDead = false;
        //     Spwan_Monster.GetComponent<dragon>().Set_SpwanZone(this.gameObject);
        // }
    }
}
