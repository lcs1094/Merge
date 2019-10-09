using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    float speed = 10.0f;
    Vector3 Skill_Move = Vector3.zero; // Skill 속도 벡터를 0으로 설정
    GameObject Witch;   // Witch 오브젝트
    float Skill1_timer = 5.0f; // 스킬 지속 시간
    float skill_Ontime = 0.0f; // 스킬이 생성되고 나서부터의 시간
    // Start is called before the first frame update
    void Start()
    {
        this.Witch = GameObject.Find("witch");
        this.transform.position = Witch.transform.position;
        WitchController player_direction = GameObject.Find("witch").GetComponent<WitchController>(); // witch에 부착된 WitchController 스크립트에서 witch의 방향을 호출한다.
        if(player_direction.dist == "Left"){ // Witch가 왼쪽으로 이동 중이면
            Skill_Move = Vector3.left;      // 벡터 방향 왼쪽
            transform.localScale = new Vector3(1, 1, 1);    // 이미지 원본
        }
        else if(player_direction.dist == "Right"){  // Witch가 오른쪽으로 이동 중이면
            Skill_Move = Vector3.right;             // 벡터 방향 오른쪽
            transform.localScale = new Vector3(-1, 1, 1);   // 이미지 좌우 반전
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Skill_Move * speed * Time.deltaTime;  // 스킬 오브젝트의 위치를 스피드와 방향벡터, 시간으로 계산
        
        skill_Ontime += Time.deltaTime;     // 스킬 생성 이후 Time Ticks
        if(skill_Ontime > Skill1_timer)     // 스킬이 생성된 이후 스킬 지속시간만큼 흘렀으면 (스킬 지속시간이 끝났으면)
            Destroy(gameObject);            // 스킬 오브젝트 파괴
    }
}
