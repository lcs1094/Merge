using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeperoGenerator : MonoBehaviour
{
    public GameObject peperoPrefab;

    private float span = 0.8f;     //몇 초에 한번씩 실행할것인지 → 1초에 한번씩
    private float delta = 0;       //흐른시간

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;     //흐른시간을 delta에 저장
        if (this.delta > this.span)       //일정 시간이 지나면
        {
            this.delta = 0;               //한번 실행했으므로 흐른시간 초기화
            instantiatePepero();
        }
    }

    private void instantiatePepero()
    {
        GameObject go = Instantiate(peperoPrefab) as GameObject;
        float px = Random.Range(-12.0f, -15.0f);
        go.transform.position = new Vector3(px, 5f, 0);
    }
}
