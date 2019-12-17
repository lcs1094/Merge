using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{
    public Transform player;    // "Player" 오브젝트를 Transform으로 가지고 옴
    Transform p;                // 카메라의 Transform p   <- 좌표값을 가지기 위해 gameObject가 아닌 Transform으로 생성
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponent<Transform>();  // 카메라에 Transform 컴포넌트 적용
    }

    // Update is called once per frame
    void LateUpdate()
    {
        p.position = new Vector3(player.position.x, player.position.y, -10); // 카메라의 위치를 플레이어의 위치와 동일선상에 놓고, Z축을 변영하여 멀리서 보도록 설정
    }
}
