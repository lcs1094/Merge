using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 780.0f;
    int jumpcount = 0;
    bool isground = true;
    bool ismoved = false;
    float speed = 7.0f;
    float move;

    GameObject bottomcloud;
    GameObject boxcloud;
    Vector3 movement;
    void move_f(){
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal") < 0){
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-1,1,1);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0){
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(1,1,1);
        }
        transform.position += moveVelocity * speed * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.name == "boxcloud" || col.transform.name == "bottomcloud"){
            isground = true;
            jumpcount = 0;
            ismoved = false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        this.rigid2D = GetComponent<Rigidbody2D>();
        jumpcount = 0;
        isground = true;
        ismoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(isground){
            //if(jumpcount == 0){
                if(Input.GetKeyDown(KeyCode.Space)){
                    this.rigid2D.AddForce(transform.up * this.jumpForce);
                    //jumpcount = 1;
                    //isground = false;
                }
            //}
        //}
        /*
        if(!ismoved){
            if(jumpcount == 1){
                if(Input.GetKeyDown(KeyCode.LeftArrow)){
                    //transform.Translate(-this.speed, 0, 0);
                    //transform.position = new Vector2(transform.position.x - 60f*Time.deltaTime, transform.position.y);
                    move = -speed * Time.deltaTime;
                    this.transform.Translate(new Vector2(move,0));
                    ismoved = true;
                }

                if(Input.GetKeyDown(KeyCode.RightArrow)){
                    //transform.Translate(this.speed, 0, 0);
                    //transform.position = new Vector2(transform.position.x + 60f*Time.deltaTime, transform.position.y);
                    move = speed * Time.deltaTime;
                    this.transform.Translate(new Vector2(move,0));
                    ismoved = true;
                }
            }
        }
        */
        move_f();
    }
}
