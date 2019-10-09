using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGenerator : MonoBehaviour
{
    public GameObject Skill1;   // 프리팹 사용을 위해 public으로 설정
    GameObject Witch;           // 스킬 시전자 (Witch) 오브젝트
    float skill_Active = 3.0f;  // 스킬 쿨타임
    float skill_timer = 0.0f;   // 시간 측정용 변수
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.Witch = GameObject.Find("witch");
        skill_timer += Time.deltaTime;  // 스킬 발동을 위한 시간 측정
        if(skill_timer > skill_Active){ // 스킬 발동을 위한 시간이 쿨타임만큼 지나면
            skill_timer = 0.0f;         // 시간 변수 초기화
            GameObject skill = Instantiate(Skill1) as GameObject;   // 스킬 오브젝트 프리팹으로 생성
            skill.transform.position = Witch.transform.position;    // 스킬의 위치는 Witch 오브젝트의 위치로 생성
        }
    }
}
