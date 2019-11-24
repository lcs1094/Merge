using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirector : MonoBehaviour
{
    private float delta = 0.0f;       //흐른시간
    private float coolTime = 2.0f;
    private bool coolOver = true;
    private bool IsHansel = true;
    private bool nowHansel = true;
    private GameObject nowPlayer;
    private GameObject newPlayer;
    public GameObject Hansel;
    public GameObject Gretel;
    Vector3 nowPos = new Vector3(0, 0, 0);
    Vector3 defaultPos = new Vector3(0, 0, 0);
    private GameObject StageManager;

    // Start is called before the first frame update
    void Start()
    {
        this.StageManager = GameObject.FindWithTag("StageManager");
        nowPlayer = Hansel;
        Hansel.SetActive(true);
        Gretel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        this.IsHansel = StageManager.GetComponent<StageManager>().getIsHansel();
        timer();
    }

    void timer()
    {
        if (!coolOver)
        {
            delta += Time.deltaTime;       //흐른 시간을 delta에 저장
            if ((IsHansel && !nowHansel) || (!IsHansel && nowHansel))
            {
                StageManager.GetComponent<StageManager>().setIsHansel();
            }
            if (coolTime - delta < 0)
                if (coolTime - delta < 0)
            {
                delta = 0.0f;
                coolOver = true;
            }
        }
        else { changeCharacter(); }
    }

    void changeCharacter()
    {
        if ((IsHansel && !nowHansel) || (!IsHansel && nowHansel))
        {
            changeFunc();
        }
    }

    void changeFunc()
    {
        setInactivePlayer();
        setActivePlayer();
        coolOver = false;
    }

    void setInactivePlayer()
    {
        nowPos = nowPlayer.transform.position;
        nowPlayer.SetActive(false);
    }

    void setActivePlayer()
    {
        if (IsHansel)
        {
            newPlayer = Hansel;
            newPlayer.SetActive(true);
            nowHansel = true;
        }
        else
        {
            newPlayer = Gretel;
            newPlayer.SetActive(true);
            nowHansel = false;
        }
        newPlayer.transform.position = nowPos;
        nowPlayer = newPlayer;
    }
}
