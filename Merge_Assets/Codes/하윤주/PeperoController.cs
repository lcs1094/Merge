using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeperoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);   
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.1f, 0);                     //1프레임마다 아래로 낙하
        if (transform.position.y < -2.5f) { Destroy(gameObject); }   //화면 밖으로 벗어나면 삭제
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StageManager.instance.minusLife();
            Destroy(gameObject);
        }
    }

        /*
        private IEnumerator peperoMove()
        {
            while(transform.position.y > 2.5f)
            {
                transform.Translate(0, -0.1f, 0);                     //1프레임마다 아래로 낙하
                yield return new WaitForSeconds(.1f);
            }
            Destroy(gameObject);
        }
        */
    }
