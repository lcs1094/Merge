using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentContorller : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = gameObject.GetComponent<Rigidbody2D>();
        this.collider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        string t = col.gameObject.tag;
        if (t == "HSkill01" || t == "HSkill02" || t == "GSkill01")
        {
            Destroy(col.gameObject);
            if (gameObject.name == "mother")
            {
                if (StageManager.instance.getMomLife() > 0) { StageManager.instance.minusMomLife(); }
                Debug.Log(StageManager.instance.getMomLife());
                if (StageManager.instance.getMomLife() == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    if (transform.position.y < -0.32f) { rigid2D.bodyType = RigidbodyType2D.Static; }
                }
            }
            if (gameObject.name == "father")
            {
                if (StageManager.instance.getDadLife() > 0) { StageManager.instance.minusDadLife(); }
                Debug.Log(StageManager.instance.getDadLife());
                if (StageManager.instance.getDadLife() == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    if (transform.position.y < -0.32f) { rigid2D.bodyType = RigidbodyType2D.Static; }
                }
            }
        }
    }
}
