using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder_controller : MonoBehaviour
{
    public GameObject Skill3;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 3.0f){
            GameObject skill = Instantiate(Skill3) as GameObject;   // 스킬 오브젝트 프리팹으로 생성
            skill.transform.position = new Vector3(this.transform.position.x,-3.0f,0);
            skill.transform.localScale = new Vector3(0.4f,2,0);
            timer = 0;
            Destroy(this.gameObject);
        }
    }
}
