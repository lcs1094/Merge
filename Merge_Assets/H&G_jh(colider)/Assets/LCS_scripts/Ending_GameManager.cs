using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_GameManager : MonoBehaviour
{
    bool[] ending_array = {false,false,false,false,false};
    public static Ending_GameManager instance;
    void Awake(){
        if(Ending_GameManager.instance == null){
            Ending_GameManager.instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public bool ending_return(int idx){
        return ending_array[idx];
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(true)    // ending1 해금 조건
            ending_array[1] = true;
        if(true)    // ending2 해금 조건
            ending_array[2] = true;
        if(true)    // ending3 해금 조건
            ending_array[3] = true;
        if(true)    // ending4 해금 조건
            ending_array[4] = true;
    }
}
