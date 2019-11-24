using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Generator : MonoBehaviour
{   // 이 스크립트는 빈 오브젝트인 spawn과 스테이지 매니저에 적용할 스크립트입니다.
    public GameObject spwan1;
    public static Monster_Generator MG;
    //bool[] isspwan = {false,false,false,false,false,false,false};
    // Start is called before the first frame update
    public bool setspwan(){
        return true;
    }
    public bool unsetspwan(){
        return false;
    }
    void Start()
    {
        if(Monster_Generator.MG == null){
            Monster_Generator.MG = this;
        }
        else{
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
    }
}
