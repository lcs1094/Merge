using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;          //물리엔진을 통해 움직임 제어 → Rigidbody2D객체생성(힘 계산)
    public float jumpForce = 100.0f;     //점프할 때 가해지는 힘
    public float walkSpeed = 60f;      //걸을 때 가해지는 힘
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
    private float horizontalMove = 0;
    private Vector3 movement;
    
    private bool isDead = false;
    private IEnumerator healING;

    void Awake()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
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

    private void pauseInput ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){ StageManager.instance.setIsPause();}
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
            if (jumpCount==2)
            {
                StageManager.instance.setIsHansel();
            }
        }
    }

    private void skillKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Skill01", true);
            StageManager.instance.setSkill02(false);
            StartCoroutine(Timer("Skill01", 0.08f));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Skill02", true);
            StageManager.instance.setSkill02(false);
            if (name == "Gretel")
            {
                rigid2D.velocity = Vector3.zero;
                canMove = false;
                StartCoroutine("HealTimer");
            }
            else { StartCoroutine(Timer("Skill02", 0.08f)); }
        }
    }

    private IEnumerator Timer(string skill, float skillTime)
    {
        Delta = 0;
        while(Delta < skillTime*2)
        {
            Delta += 0.01f;
            if (Delta >skillTime)
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
            if (HealDelta > 0.7f&&HealDelta<0.71f) { StageManager.instance.plusLife(); }
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
                    this.rigid2D.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
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
                animator.SetBool("climbing", true);
                rigid2D.bodyType = RigidbodyType2D.Kinematic;
                transform.Translate(new Vector3(0, 0.08f, 0));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                animator.SetBool("climbing", true);
                rigid2D.bodyType = RigidbodyType2D.Kinematic;
                transform.Translate(new Vector3(0, -0.08f, 0));
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
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.RightArrow)){ key = 1;}       //오른쪽 방향키가 눌리면 key값을 1로 설정
        if (Input.GetKey(KeyCode.LeftArrow)){ key = -1; }       //왼쪽 방향키가 눌리면 key값을 -1로 설정
        playerMove();
    }

    private void playerMove()
    {
        transform.localScale = new Vector3 (key, 1, 1);
        movement.Set(horizontalMove, 0,0);
        movement = movement.normalized * walkSpeed * Time.deltaTime;
        rigid2D.MovePosition(transform.position + movement);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 2;
            animator.SetInteger("jumpCount", jumpCount);
        }
        
        else if(col.gameObject.tag == "Monster")  
        {
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

}