using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDirector : MonoBehaviour
{
    private float delta = 0.0f;       //흐른시간
    private float coolTime = 3.0f;
    private bool coolOver = true;
    private bool IsHansel = true;
    private bool nowHansel = true;
    private int key = 1;
    private GameObject nowPlayer;
    private GameObject newPlayer;
    public GameObject Hansel;
    public GameObject Gretel;
    Vector3 nowPos = new Vector3(0, 0, 0);
    Vector3 defaultPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        nowPlayer = Hansel;
        Hansel.SetActive(true);
        Gretel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        this.IsHansel = StageManager.instance.getIsHansel();
        changeCharacter();
    }

    void changeCharacter()
    {
        if ((IsHansel && !nowHansel) || (!IsHansel && nowHansel))
        {
            changeFunc();
            StageManager.instance.setChangeCoolOver(false);
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
        key = nowPlayer.GetComponent<PlayerController1>().getKey();
        nowPlayer.SetActive(false);
    }

    void setActivePlayer()
    {
        if (IsHansel)
        {
            newPlayer = Hansel;
            nowHansel = true;
        }
        else
        {
            newPlayer = Gretel;
            nowHansel = false;
        }
        newPlayer.transform.position = nowPos;
        newPlayer.transform.localScale = new Vector3(key, 1, 1);
        newPlayer.GetComponent<PlayerController1>().setKey(key);
        nowPlayer = newPlayer;
        nowPlayer.SetActive(true);
    }
}
