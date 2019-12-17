using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 780.0f;
    float speed = 7.0f;
    float move;

    public void hit(int dmg){      //피격시 실행
        transform.position += new Vector3(-dmg,0,0);
    }
    Vector3 movement;
    void move_f(){
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal") < 0){
            moveVelocity = Vector3.left;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0){
            moveVelocity = Vector3.right;
        }
        transform.position += moveVelocity * speed * Time.deltaTime;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }
        move_f();
    }
}
