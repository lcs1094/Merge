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
        //if (coolOver) { changeCharacter(); }
        //else { timer(); }
        changeCharacter();
    }
    
    void timer()
    {
        delta += Time.deltaTime;       //흐른 시간을 delta에 저장
        if (coolTime - delta < 0)
        {
            delta = 0.0f;
            coolOver = true;
        }
    }


    void changeCharacter()
    {
        if (IsHansel && !nowHansel)
        {
            setInactivePlayer();
            setActivePlayer();
            Debug.Log("Hansel");
        }

        else if (!IsHansel && nowHansel)
        {
            setInactivePlayer();
            setActivePlayer();
            Debug.Log("Gretel");
        }
        coolOver = false;
        StageManager.GetComponent<StageManager>().setChangedFalse();
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
