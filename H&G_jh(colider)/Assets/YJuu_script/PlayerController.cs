using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;          //물리엔진을 통해 움직임 제어 → Rigidbody2D객체생성(힘 계산)
    private float jumpForce = 450.0f;     //점프할 때 가해지는 힘
    private float walkForce = 50.0f;      //걸을 때 가해지는 힘
    private float maxWalkSpeed = 3.0f;    //걸을때의 최고속도
    private Vector3 defaultPos = new Vector3(0, 0, 0);       //player의 기본위치
    private GameObject StageManager;          //GameDirector의 함수를 사용할것이므로 객체 생성
    private string name = "";
    private bool isGrounded = false;
    private int jumpCount = 2;
    private bool isRope = false;
    private Animator animator;
    public GameObject GSkill02;
    private bool canMove = true;
    private float delta = 0;
    private float SkillTime = 6;

    void Awake()
    {
        this.animator = gameObject.GetComponent<Animator>();
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        this.StageManager = GameObject.FindWithTag("StageManager");
        this.name = gameObject.name;
        jumpCount = 0;
    }

    void Update()
    {
        checkFall();
        if (canMove)
        {
            changeKeyInput();
            moveInput();
            goKeyInput();
            skillKeyInput();
            jumpKeyInput();
        }
        else Timer();
        Debug.Log(delta);
    }

    private void checkFall()
    {
        if (transform.position.y < -6)
        {
            transform.localPosition = defaultPos;          //player를 기본위치로 이동
        }
    }

    private void goKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StageManager.GetComponent<StageManager>().setGo(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StageManager.GetComponent<StageManager>().setGo(false);
        }
    }

    private void changeKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            StageManager.GetComponent<StageManager>().setIsHansel();
        }
    }

    private void skillKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Skill01", true);
            StageManager.GetComponent<StageManager>().usedSkill01(name);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Skill02", true);
            StageManager.GetComponent<StageManager>().usedSkill02(name);
            if (name == "Gretel")
            {
                Instantiate(GSkill02);
                canMove = false;
            }
        }
    }

    private void Timer()
    {
        delta += Time.deltaTime;    // 스킬 생성 이후 Time Ticks
        if (delta > SkillTime)      // 스킬이 생성된 이후 스킬 지속시간만큼 흘렀으면 (스킬 지속시간이 끝났으면)
        {
            canMove = true;
            delta = 0;
            animator.SetBool("Skill02", false);
        }
    }

    private void jumpKeyInput()
    {
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    this.rigid2D.AddForce(transform.up * this.jumpForce);
                    jumpCount--;
                    animator.SetInteger("jumpCount", jumpCount);
                }
            }
        }
    }

    private void ropeKeyInput()
    {
        if (isRope)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)){this.rigid2D.AddForce(transform.up * this.walkForce);}
            if (Input.GetKeyDown(KeyCode.DownArrow)){this.rigid2D.AddForce(transform.up* -1 * this.walkForce);}
        }
    }

    private void moveInput()
    {
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;       //오른쪽 방향키가 눌리면 key값을 1로 설정
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;       //왼쪽 방향키가 눌리면 key값을 -1로 설정

        float speedx = Mathf.Abs(this.rigid2D.velocity.x);   //현재 x축 속도의 절대값을 speedx에 저장

        if (speedx < this.maxWalkSpeed) this.rigid2D.AddForce(transform.right * key * this.walkForce);  //speedx가 최대속도보다 작은 경우 오른쪽으로 가하는 힘을 증가(key가 -1인경우 왼쪽으로)

        if (key != 0) transform.localScale = new Vector3(key, 1, 1);  //key값(눌린 방향키가 어떤 방향키인지)에 따라 player의 모양을 바꿈
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 2;
            animator.SetInteger("jumpCount", jumpCount);
        }

        if(col.gameObject.tag == "Rope")
        {
            Debug.Log("rope");
            isRope = true;
        }

        /*else if(col.gameObject.tag == "")  태그가 몬스터 스킬이면 아픈 애니메이션 실행
        {

        }*/

    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Rope")
        {
            animator.SetBool("climbing", true);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Rope")
        {
            animator.SetBool("climbing", false);
        }
    }
}
