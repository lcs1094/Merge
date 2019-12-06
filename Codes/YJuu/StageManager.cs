using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance = null;
    
    //엔딩 요건들
    private int life = 3;               //플레이어의 생명은 3개
    private int momLife = 3;            //부모님의 생명도 3개(차후 논의를 통해 수정 가능)
    private int dadLife = 3;
    private bool lava = false;          //Stage04에 진입했는지 확인
    private bool killWitch = false;     //마녀를 잡았는지 확인
    private int ending = 0;             //엔딩 요건들을 확인해봤을때 이번에 획득한 엔딩

    //플레이어 정보들
    private bool isHansel = true;             //캐릭터가 헨젤인지 = 현재 플레이중인 캐릭터가 누구인지(true:헨젤, false:그레텔)
    private bool HSkill01 = false;            //헨젤의 1번 스킬이 사용되었는지
    private bool HSkill02 = false;            //헨젤의 2번 스킬이 사용되었는지
    private bool GSkill01 = false;            //그레텔의 1번 스킬이 사용되었는지
    private bool GSkill02 = false;            //그레텔의 2번 스킬이 사용되었는지
    private bool go = false;
    private int stage = 0;
    private bool changeCoolOver = true;      //캐릭터 변환 쿨타임이 끝났는지(T:쿨타임 끝=변경 가능)

    //빵가루와 관련된 변수들
    private int bread = 0;                //모은 빵가루의 개수
    private bool allBread = false;        //빵가루를 다 모았는지 여부를 확인

    private bool sceneChanged = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        
    }

    void Update()
    {
        stage = GameManager.instance.getSceneNum();
        if (stage != 0) { allBread = true; }
    }

    //엔딩 요건들을 확인하여 획득한 엔딩이 있는지 확인
    private void checkEnding()
    {
        if(momLife == 0 && dadLife == 0)
        {
            ending = 1;
            GameManager.instance.goEndingScene01();
        }     //부모님의 생명이 둘 다 0이 되면: 엔딩1 "뿌리를 뽑다"
        else if (lava)                                       //부모님이 죽지 않고
        {
            if (killWitch)
            {
                ending = 3;
                GameManager.instance.goEndingScene03();
            }                   //Stage04에 진입하고, 마녀를 처치했으면: 엔딩3 "금의환향"
            else if (life == 0)
            {
                ending = 4;
                GameManager.instance.goEndingScene04();
            }              //Stage04에 진입했지만, 마녀를 처치하지 못하고 생명이 0이 되면: 엔딩4 "좋은 단백질 공급원"
        }
        else if(life == 0)
        {
            ending = 2;
            GameManager.instance.goEndingScene02();
        }                   //부모님이 죽지않고 Stage04에 진입하지 못했지만 생명이 0이 되면: 엔딩2 "우린 영원히 함께"
        GameManager.instance.findEnding(ending);     //엔딩을 획득했으면 StageManager의 endings의 해당엔딩을 true로 바꿈
    }
  
    public void goNextStage()
    {
        if (stage == 0) { GameManager.instance.goForestScene(); }
        else if (stage == 1) { GameManager.instance.goCandyScene(); }
        else if (stage == 2)
        {
            GameManager.instance.goLavaScene();
            lava = true;
        }
    }

    //각 멤버변수들의 설정자, 접근자
    public int getLife() { return life; }

    public void minusLife(int life) { this.life -= life; }

    public void plusLife(int life) { this.life += life; }

    public int getMomLife() { return momLife; }

    public void setMomLife(int momLife) { this.momLife = momLife; }

    public int getDadLife() { return dadLife; }

    public void setDadLife(int dadLife) { this.dadLife = dadLife; }

    public bool getKillWitch() { return killWitch; }

    public void setKillWitch(bool killWitch) { this.killWitch = killWitch; }

    public int getEnding() { return ending; }

    public bool getIsHansel() { return isHansel; }

    public void setIsHansel()
    {
        if (changeCoolOver)
        {
            if (isHansel) { isHansel = false; }
            else { isHansel = true; }
        }
    }

    public void setChangeCoolOver(bool coolOver)
    {
        this.changeCoolOver = coolOver;
    }

    public bool getChangeCoolOver() { return changeCoolOver; }

    public void addBread()
    {
        this.bread += 1;
        Debug.Log("bread:"+bread);
        if (bread == 1)
        {
            allBread = true;
            bread = 0;
        }
    }

    public bool getAllBread() { return this.allBread; }

    public void setGo(bool go) { this.go = go; }

    public bool getGo() { return this.go; }

    public void usedSkill01(string name)
    {
        if (name == "Hansel") { this.HSkill01 = true; Debug.Log("HSkill01"); }
        else if (name == "Gretel") { this.GSkill01 = true; Debug.Log("GSkill01"); }
    }

    public void usedSkill02(string name)
    {
        if (name == "Hansel") { this.HSkill02 = true; Debug.Log("HSkill02"); }
        else if (name == "Gretel") { this.GSkill02 = true; Debug.Log("GSkill02"); }
    }

    public bool getHSkill01() { return HSkill01; }

    public bool getHSkill02() { return HSkill02; }

    public bool getGSkill01() { return GSkill01; }

    public bool getGSkill02() { return GSkill02; }

    public int getStage() { return stage; }

    public bool getSceneChanged() { return sceneChanged; }

    public void setSceneChanged(bool changed) { this.sceneChanged = changed; }
}
