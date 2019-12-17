using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo_Controller : MonoBehaviour
{
    public float speed = 10.0f;
    float starttimer = 1.0f;
    float timer = 0;
    Vector3 tomove;
    void OnCollisionEnter2D(Collision2D col){
        if(col.transform.tag =="Player" || col.transform.tag == "Ground"){ // 공격 스킬 피격판정시
        // col.transform.tag로 사용 가능, 단, tag 사용시 모든 스킬의 tag 변경할 것
            //col.gameObject.GetComponent<PlayerController>().hit(10);  // Player의 Hit 함수 실행
            //Stagemanager.instance.hit(20);    // 스테이지매니저.인스턴스이름.함수
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 moving = new Vector3(this.transform.position.x - 6.0f,-10,0);
        tomove = (moving - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > starttimer){
            transform.position += tomove * speed * Time.deltaTime;
        }  
        if(this.transform.position.y < -20){
            Destroy(this.gameObject);
        }
    }
}
