using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 780.0f;
    float speed = 7.0f;
    float move;

    Vector3 movement;
    void move_f(){
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal") < 0){
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-2,2,1);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0){
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(2,2,1);
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
