using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private Rigidbody2D rigid2D;          //물리엔진을 통해 움직임 제어 → Rigidbody2D객체생성(힘 계산)
    private float jumpForce = 450.0f;     //점프할 때 가해지는 힘
    private float walkForce = 60.0f;      //걸을 때 가해지는 힘
    private float maxWalkSpeed = 3.0f;    //걸을때의 최고속도
    private Vector3 defaultPos = new Vector3(0, 0, 0);       //player의 기본위치
    private string name = "";
    private bool isGrounded = false;
    private int jumpCount = 2;
    public bool isLadder = false;
    private Animator animator;
    public GameObject Skill01;
    public GameObject Skill02;
    public bool canMove = true;
    private int key = 1;
    private float HealDelta = 0;
    private float Delta = 0;
    private PlayerSound sound;
    private bool isDead = false;
    private IEnumerator healING;

    void Awake()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        this.sound = gameObject.GetComponent<PlayerSound>();
        this.name = gameObject.name;
        jumpCount = 0;
    }

    void Update()
    {
        pauseInput();
        if (!StageManager.instance.getIsPause())
        {
            if (canMove)
            {
                changeKeyInput();
                moveInput();
                goKeyInput();
                skillKeyInput();
                jumpKeyInput();
                ladderKeyInput();
            }

        }
        if (StageManager.instance.getLife() == 0) { deadMotion(); }
    }

    private void pauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { StageManager.instance.setIsPause(); }
    }

    private void goKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StageManager.instance.setGo(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            StageManager.instance.setGo(false);
        }
    }

    private void changeKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (jumpCount == 2)
            {
                if (StageManager.instance.getChangeCoolOver())
                {
                    sound.changeSound();
                    StageManager.instance.setIsHansel();
                }
            }
        }
    }

    private void skillKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (StageManager.instance.getSkill01())
            {
                animator.SetBool("Skill01", true);
                StageManager.instance.setSkill01(false);
                StartCoroutine(Timer("Skill01", 0.08f));
                sound.skillSound(1);
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (StageManager.instance.getSkill02())
            {
                animator.SetBool("Skill02", true);
                StageManager.instance.setSkill02(false);
                if (name == "Gretel")
                {
                    if (isGrounded)
                    {
                        rigid2D.velocity = Vector3.zero;
                        canMove = false;
                        StartCoroutine("HealTimer");
                        sound.skillSound(2);
                    }
                }
                else { StartCoroutine(Timer("Skill02", 0.08f)); sound.skillSound(2); }
            }
        }
    }

    private IEnumerator Timer(string skill, float skillTime)
    {
        Delta = 0;
        while (Delta < skillTime * 2)
        {
            Delta += 0.01f;
            if (Delta > skillTime)
            {
                animator.SetBool(skill, false);
            }
            yield return new WaitForSeconds(.01f);
        }
        if (skill == "Skill01")
        {
            GameObject skill01 = Instantiate(Skill01) as GameObject;
            skill01.GetComponent<SkillController>().Skill01(name, transform.position, key);
        }
        else if (skill == "Skill02")
        {
            GameObject skill02 = Instantiate(Skill02) as GameObject;
            skill02.GetComponent<SkillController>().Skill02(name, transform.position, key);
        }
    }

    private IEnumerator HealTimer()
    {
        HealDelta = 0;
        GameObject skill02 = Instantiate(Skill02) as GameObject;
        skill02.GetComponent<SkillController>().Skill02(name, transform.position, key);
        while (HealDelta < 1.4f)
        {
            HealDelta += 0.01f;
            if (isDead)
            {
                StopCoroutine("HealTimer");
                Destroy(skill02);
            }
            if (HealDelta > 0.7f && HealDelta < 0.71f) { StageManager.instance.plusLife(); }
            yield return new WaitForSeconds(.01f);
        }
        if (!isDead)
        {
            StageManager.instance.plusLife();
            canMove = true;
        }
        animator.SetBool("Skill02", false);
        Destroy(skill02);
    }

    private void jumpKeyInput()
    {
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    sound.audioStop();
                    this.rigid2D.AddForce(transform.up * this.jumpForce);
                    jumpCount--;
                    animator.SetInteger("jumpCount", jumpCount);
                }
            }
        }
    }

    private void ladderKeyInput()
    {
        if (isLadder)
        {
            rigid2D.velocity = Vector3.zero;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                sound.ladderSound();
                animator.SetBool("climbing", true);
                rigid2D.bodyType = RigidbodyType2D.Kinematic;
                transform.Translate(new Vector3(0, 0.09f, 0));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                sound.ladderSound();
                animator.SetBool("climbing", true);
                rigid2D.bodyType = RigidbodyType2D.Kinematic;
                transform.Translate(new Vector3(0, -0.09f, 0));
            }
        }

        else
        {
            rigid2D.bodyType = RigidbodyType2D.Dynamic;
            animator.SetBool("climbing", false);
        }
    }

    private void moveInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;       //오른쪽 방향키가 눌리면 key값을 1로 설정
            playerMove(key);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;       //왼쪽 방향키가 눌리면 key값을 -1로 설정
            playerMove(key);
        }
    }

    private void playerMove(int key)
    {
        sound.walkSound();
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);   //현재 x축 속도의 절대값을 speedx에 저장

        if (speedx < this.maxWalkSpeed) this.rigid2D.AddForce(transform.right * key * this.walkForce);  //speedx가 최대속도보다 작은 경우 오른쪽으로 가하는 힘을 증가(key가 -1인경우 왼쪽으로)

        if (key != 0) transform.localScale = new Vector3(key, 1, 1);  //key값(눌린 방향키가 어떤 방향키인지)에 따라 player의 모양을 바꿈
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 2;
            sound.walkSound();
            animator.SetInteger("jumpCount", jumpCount);
        }

        else if (col.gameObject.tag == "Monster" || col.gameObject.tag == "Monster_Skill" || col.gameObject.tag == "Witch" || col.gameObject.tag == "Witch1_Skill" || col.gameObject.tag == "Witch2_Skill")
        {
            StageManager.instance.minusLife();
            if (StageManager.instance.getLife() == 0)
            {
                deadMotion();
            }
        }
        else if (col.gameObject.tag == "Witch_Ulti")
        {
            StageManager.instance.minusLife();
            StageManager.instance.minusLife();
            StageManager.instance.minusLife();
            if (StageManager.instance.getLife() == 0)
            {
                deadMotion();
            }
        }
    }

    private void deadMotion()
    {
        animator.SetBool("Dead", true);
        rigid2D.bodyType = RigidbodyType2D.Static;
        canMove = false;
        isDead = true;
    }

    public int getKey() { return key; }

    public void setKey(int key) { this.key = key; }
}