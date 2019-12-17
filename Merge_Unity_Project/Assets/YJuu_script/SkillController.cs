using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    private int direction = 0;
    private Vector3 playerPos = new Vector3(0, 0, 0);
    private float delta = 0;
    private string thisName = "";
    private int damage = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (thisName != "GSkill02")
        {
            if (col.gameObject.tag == "Monster" || col.gameObject.tag == "Witch")
            {
                Debug.Log("Collision");
                Destroy(gameObject);
            }
        }
    }

    public void Skill01(string name, Vector3 playerPos, int key)
    {
        this.direction = key;

        if (name == "Hansel")
        {
            transform.localScale = new Vector3(direction, 1, 1);
            HSkill01(playerPos, .8f, direction);
            thisName = "HSkill01";
        }
        else if (name == "Gretel")
        {
            transform.localScale = new Vector3(direction, 1, 1);
            GSkill01(playerPos, 2f, direction);
            thisName = "GSkill01";
        }
    }

    public void Skill02(string name, Vector3 playerPos, int key)
    {
        this.direction = key;

        if (name == "Hansel")
        {
            transform.localScale = new Vector3(direction * -1, 1, 1);
            HSkill02(playerPos, 0.6f, direction);
            thisName = "HSkill02";
        }

        else if (name == "Gretel")
        {
            GSkill02(playerPos, 2, direction);
            thisName = "GSkill02";
        }
    }


    private void HSkill01(Vector3 playerPos, float skillTime, int key)
    {
        damage = 50;
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition(0.5f * direction, -0.2f);
    }

    private void HSkill02(Vector3 playerPos, float skillTime, int direction)
    {
        damage = 100;
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition(0.55f * direction, -0.15f);
    }

    private void GSkill01(Vector3 playerPos, float skillTime, int direction)
    {
        damage = 35;
        StartCoroutine(Timer(skillTime));
        setPlayerPos(playerPos);
        setPosition(1.5f * direction, -0.1f);
        StartCoroutine(skillMove());
    }

    private void GSkill02(Vector3 playerPos, float skillTime, int direction)
    {
        damage = 0;
        setPlayerPos(playerPos);
        setPosition(0, 1);
    }

    private IEnumerator Timer(float skillTime)
    {
        while (delta < skillTime)       // 스킬이 생성된 이후 스킬 지속시간만큼 흘렀으면 (스킬 지속시간이 끝났으면)
        {
            delta += 0.1f;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(gameObject);     // 스킬 오브젝트 파괴
    }

    private IEnumerator skillMove()
    {
        while (true)
        {
            this.transform.Translate(new Vector3(0.2f * direction, 0, 0));

            yield return new WaitForSeconds(.01f);
        }
    }

    private void setPlayerPos(Vector3 playerPos)
    {
        this.playerPos = playerPos;
    }

    private void setPosition(float x, float y)
    {
        this.transform.position = playerPos;
        this.transform.Translate(new Vector3(x, y, 0));
    }
}
