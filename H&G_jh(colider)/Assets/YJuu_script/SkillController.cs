using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private int direction = 0;
    private Vector3 playerPos = new Vector3(0, 0, 0);
    private float delta = 0;
    private string Tag = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            Debug.Log("Collision");
            //col.gameObject.GetComponent<MonsterController>().hit();     //Monster함수 이름 변경 필요
            Destroy(gameObject);
        }
    }

    public void Skill01(string name, Vector3 playerPos, int key)
    {
        if (name == "Hansel") { HSkill01(playerPos, .6f, key); }
        else if (name == "Gretel") { GSkill01(playerPos, 1.5f, key); }
    }

    public void Skill02(string name, Vector3 playerPos,int key)
    {
        if (name == "Hansel") { HSkill02(playerPos, 0.6f, key); }
        else if (name == "Gretel") { GSkill02(playerPos, 2, key); }
    }
    

    private void HSkill01(Vector3 playerPos,float skillTime,int key)
    {
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition();
    }

    private void HSkill02(Vector3 playerPos,float skillTime, int key)
    {
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition();
    }

    private void GSkill01(Vector3 playerPos,float skillTime, int key)
    {
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition();
    }

    private void GSkill02(Vector3 playerPos,float skillTime, int key)
    {
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition();
    }

    private IEnumerator Timer(float skillTime)
    {
        while (delta < skillTime)       // 스킬이 생성된 이후 스킬 지속시간만큼 흘렀으면 (스킬 지속시간이 끝났으면)
        {
            delta += 0.1f;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(gameObject);     // 스킬 오브젝트 파괴
        Debug.Log("end");
    }

    private void skillMove()
    {
        transform.localPosition += new Vector3(direction,0,0);
        if (direction != 0) transform.localScale = new Vector3(direction, 1, 1);  //key값(눌린 방향키가 어떤 방향키인지)에 따라 player의 모양을 바꿈
    }

    private void setDirection(int direction)
    {
        this.direction = direction;
    }

    private void setPlayerPos(Vector3 playerPos)
    {
        this.playerPos = playerPos;
    }

    private void setPosition()
    {
        this.transform.position = playerPos;
        this.transform.Translate(new Vector3(0, 1, 0));
    }
}
    