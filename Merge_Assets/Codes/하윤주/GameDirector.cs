using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private bool isContinue;
    private int bread;
    private int monster;
    private bool allBread = false;
    private bool allMonster = false;
    public int targetBread;
    public int targetMonster;
    public static GameDirector instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        isContinue = StageManager.instance.setStage();
        if (isContinue)
        {
            bread = StageManager.instance.getBread();
            monster = StageManager.instance.getMonster();
        }
        else { bread = 0; monster = 0; }
        StageManager.instance.setBread(bread);
        StageManager.instance.setMonster(monster);
        if (targetBread == bread)
        {
            allBread = true;
            StageManager.instance.setAllBread(allBread);
        }
        if (targetMonster == monster)
        {
            allMonster = true;
            StageManager.instance.setAllMonster(allMonster);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addBread()
    {
        if (!allBread)
        {
            this.bread += 1;
            StageManager.instance.setBread(bread);
        }
        if (bread >= targetBread)
        {
            allBread = true;
            StageManager.instance.setAllBread(allBread);
        }
    }

    public void killMonster()
    {
        if (!allMonster)
        {
            this.monster += 1;
            StageManager.instance.setBread(bread);
        }
        if (monster >= targetMonster)
        {
            allMonster = true;
            StageManager.instance.setAllMonster(allMonster);
        }
    }

    public int getBread() { return bread; }

    public int getMonster() { return monster; }

    public bool getAllBread() { return allBread; }
}
