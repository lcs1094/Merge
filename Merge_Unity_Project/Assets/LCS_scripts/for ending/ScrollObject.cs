using UnityEngine;
using System.Collections;

public class ScrollObject : MonoBehaviour
{
	public float speed = 1.0f;
	void Update ()
	{
		// 매 프레임 x 포지션을 조금씩 이동시킨다
		if(transform.position.y > -12){
			transform.Translate(0, -1 * speed * Time.deltaTime, 0);
		}
		else{
			endingManager.Emanager.blackdown = true;
		}
	}
}