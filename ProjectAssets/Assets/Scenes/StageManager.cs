using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //엔딩 요건들
    int life = 3;               //플레이어의 생명은 3개
    int momLife = 3;            //부모님의 생명도 3개(차후 논의를 통해 수정 가능)
    int dadLife = 3;
    bool st04 = false;          //Stage04에 진입했는지 확인
    bool killWitch = false;     //마녀를 잡았는지 확인

    int ending = 0;             //엔딩 요건들을 확인해봤을때 이번에 획득한 엔딩

    //플레이어 정보들
    bool isHensel = true;             //캐릭터가 헨젤인지 = 현재 플레이중인 캐릭터가 누구인지(true:헨젤, false:그레텔)
    bool HSkill01 = false;            //헨젤의 1번 스킬이 사용되었는지
    bool HSkill02 = false;            //헨젤의 2번 스킬이 사용되었는지
    bool GSkill01 = false;            //그레텔의 1번 스킬이 사용되었는지
    bool GSkill02 = false;            //그레텔의 2번 스킬이 사용되었는지

    GameObject GameManager;     //Scene간 이동을 위해 GameManager객체 생성

    void Start()
    {
        this.GameManager = GameObject.FindWithTag("GameManager");     //GameManger객체를 Tag를 통해 찾아옴
    }

    void Update()
    {
        
    }

    //엔딩 요건들을 확인하여 획득한 엔딩이 있는지 확인
    void checkEnding()
    {
        if(momLife == 0 && dadLife == 0) { ending = 1; }     //부모님의 생명이 둘 다 0이 되면: 엔딩1 "뿌리를 뽑다"
        else if (st04)                                       //부모님이 죽지 않고
        {
            if (killWitch) { ending = 3; }                   //Stage04에 진입하고, 마녀를 처치했으면: 엔딩3 "금의환향"
            else if (life == 0) { ending = 4; }              //Stage04에 진입했지만, 마녀를 처치하지 못하고 생명이 0이 되면: 엔딩4 "좋은 단백질 공급원"
        }
        else if(life == 0) { ending = 2; }                   //부모님이 죽지않고 Stage04에 진입하지 못했지만 생명이 0이 되면: 엔딩2 "우린 영원히 함께"
    }

    //엔딩을 획득하면 엔딩화면으로 이동
    void goEnding()
    {
        if(ending != 0) { GameManager.GetComponent<GameManager>().goEndingScene(); }
    }

    //각 멤버변수들의 설정자, 접근자
    public int getLife() { return life; }

    public void setLife(int life) { this.life = life; }

    public int getMomLife() { return momLife; }

    public void setMomLife(int momLife) { this.momLife = momLife; }

    public int getDadLife() { return dadLife; }

    public void setDadLife(int dadLife) { this.dadLife = dadLife; }

    public bool getKillWitch() { return killWitch; }

    public void setKillWirch(bool killWitch) { this.killWitch = killWitch; }

    public int getEnding() { return ending; }

    public bool getIsHensel() { return isHensel; }

    public void setIsHensel(bool isHensel) { this.isHensel = isHensel; }
}
