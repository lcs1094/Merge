using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private int direction = 0;
    private Vector3 playerPos = new Vector3(0, 0, 0);
    private float delta = 0;
    private int skillTimer = 6;
    private string tag = "";
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.tag = gameObject.tag;
        Player = GameObject.FindWithTag("Player");
        Debug.Log(Player.name);
        this.transform.position = Player.transform.position;
        setPosition();
    }

    // Update is called once per frame
    void Update()
    {
        skillType();
    }

    private void skillType()
    {
        if(tag == "GSkill02")
        {
            Timer();
        }
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

    private void Timer()
    {
        delta += Time.deltaTime;     // 스킬 생성 이후 Time Ticks
        if (delta > skillTimer)      // 스킬이 생성된 이후 스킬 지속시간만큼 흘렀으면 (스킬 지속시간이 끝났으면)
            Destroy(gameObject);     // 스킬 오브젝트 파괴
    }

    private void skillMove()
    {
        transform.localPosition += new Vector3(direction,0,0);
        if (direction != 0) transform.localScale = new Vector3(direction, 1, 1);  //key값(눌린 방향키가 어떤 방향키인지)에 따라 player의 모양을 바꿈
    }

    public void setDirection(int direction)
    {
        this.direction = direction;
    }

    public void setPlayerPos()
    {
        this.playerPos = Player.transform.position;
    }

    private void setPosition()
    {
        this.transform.position = Player.transform.position;
        this.transform.Translate(new Vector3(0, 1, 0));
    }
}
    