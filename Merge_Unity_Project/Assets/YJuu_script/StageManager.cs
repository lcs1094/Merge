using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class StageManager : MonoBehaviour
{
    static StageManager Instance;
    public static StageManager instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<StageManager>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newStageManager = new GameObject("StageManager").AddComponent<StageManager>();
                    Instance = newStageManager;
                }
            }
            return Instance;
        }
        private set
        {
            instance = value;
        }
    }

    //엔딩 요건들
    private int life = 3;               //플레이어의 생명은 3개
    private int momLife = 3;            //부모님의 생명도 3개(차후 논의를 통해 수정 가능)
    private int dadLife = 3;
    private bool lava = false;          //Stage04에 진입했는지 확인
    private bool killWitch = false;     //마녀를 잡았는지 확인
    private int ending = 0;             //엔딩 요건들을 확인해봤을때 이번에 획득한 엔딩

    //플레이어 정보들
    private bool isHansel = true;             //캐릭터가 헨젤인지 = 현재 플레이중인 캐릭터가 누구인지(true:헨젤, false:그레텔)
    private bool Skill01 = true;              //1번 스킬이 사용가능한지
    private bool Skill02 = true;              //2번 스킬이 사용가능한지
    private bool go = false;
    private int stage = 0;
    private bool changeCoolOver = true;      //캐릭터 변환 쿨타임이 끝났는지(T:쿨타임 끝=변경 가능)

    //빵가루와 관련된 변수들
    private bool allBread = false;        //빵가루를 다 모았는지 여부를 확인
    private bool allMonster = false;
    private int bread = 0;
    private int monster = 0;

    private bool sceneChanged = false;

    private bool IsPause = false;
    private bool waitingEnd = false;

    void Awake()
    {
        var objs = FindObjectsOfType<StageManager>();
        if (objs.Length != 1) { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {

    }

    void Update()
    {
        checkEnding();
    }

    public bool setStage()
    {
        stage = GameManager.instance.getSceneNum();
        if (stage < 0) { Destroy(gameObject); }
        else { GameManager.instance.lastPlayScene = stage; }
        if (stage == 3) { lava = true; }

        if (GameManager.instance.lastHeart != 0)
        {
            this.life = GameManager.instance.lastHeart;
            this.bread = GameManager.instance.lastBread;
            this.monster = GameManager.instance.lastMonster;
            return true;
        }

        else { return false; }
    }

    //엔딩 요건들을 확인하여 획득한 엔딩이 있는지 확인
    private void checkEnding()
    {
        if (momLife == 0 && dadLife == 0)
        {
            StartCoroutine("waiting");
            if (waitingEnd)
            {
                ending = 1;
                GameManager.instance.findEnding(ending);     //엔딩을 획득했으면 StageManager의 endings의 해당엔딩을 true로 바꿈
                GameManager.instance.goEndingScene01();

                Destroy(gameObject);
            }
        }     //부모님의 생명이 둘 다 0이 되면: 엔딩1 "뿌리를 뽑다"
        else if (lava)                                       //부모님이 죽지 않고
        {
            if (killWitch)
            {
                StartCoroutine("waiting");
                if (waitingEnd)
                {
                    ending = 3;
                    GameManager.instance.findEnding(ending);     //엔딩을 획득했으면 StageManager의 endings의 해당엔딩을 true로 바꿈
                    GameManager.instance.goEndingScene03();
                    Destroy(gameObject);
                }
            }                   //Stage04에 진입하고, 마녀를 처치했으면: 엔딩3 "금의환향"
            else if (life == 0)
            {
                StartCoroutine("waiting");
                if (waitingEnd)
                {
                    ending = 4;
                    GameManager.instance.findEnding(ending);     //엔딩을 획득했으면 StageManager의 endings의 해당엔딩을 true로 바꿈
                    GameManager.instance.goEndingScene04();
                    Destroy(gameObject);
                }
            }              //Stage04에 진입했지만, 마녀를 처치하지 못하고 생명이 0이 되면: 엔딩4 "좋은 단백질 공급원"
        }
        else if (life == 0)
        {
            StartCoroutine("waiting");
            if (waitingEnd)
            {
                ending = 2;
                GameManager.instance.findEnding(ending);     //엔딩을 획득했으면 StageManager의 endings의 해당엔딩을 true로 바꿈
                GameManager.instance.goEndingScene02();
                Destroy(gameObject);
            }
        }                   //부모님이 죽지않고 Stage04에 진입하지 못했지만 생명이 0이 되면: 엔딩2 "우린 영원히 함께"

    }

    private IEnumerator waiting()
    {
        yield return new WaitForSeconds(2.0f);
        waitingEnd = true;
    }

    public void goNextStage()
    {
        stage = GameManager.instance.getSceneNum();
        if (stage == 0) { GameManager.instance.goForestScene(); }
        else if (stage == 1) { GameManager.instance.goCandyScene(); }
        else if (stage == 2) { GameManager.instance.goLavaScene(); }
    }

    public void save()
    {
        GameManager.instance.goSaveScene();
    }

    //진엔딩. 바탕화면에 txt파일 생성
    public void write_End()
    {

        int end_Length = FindObjectOfType<GameManager>().endings.Length;
        bool[] endings_GameManager = FindObjectOfType<GameManager>().endings;
        bool all_Ending = true;

        for (int i = 1; i < end_Length; i++)
            if (!endings_GameManager[i])
                all_Ending = false;

        if (all_Ending)
        {
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string txtFilePath = deskPath + "\\달님에게_보내는_편지.txt";
            //if (File.Exists(txtFilePath))
            //    return;
            FileStream fs = new FileStream(txtFilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("안녕하세요 달님! 저는 그레텔이에요\n" +
                "저희 오빠는 안믿는 눈치지만, 저는 알구있어요\n" +
                "달님 덕분에 저희가 무서운 숲을 무사히 통과하고, 마녀를 무찔렀다는 걸요!\n\n" +
                "달님 덕에 저희 가족은 정말 행복한 나날을 보내고 있어요\n" +
                "마녀를 쓰러뜨리고 얻은 금은보화로 저희는 고기랑 우유를 맘껏 먹고있어요!\n" +
                "달님도 꼬옥 저희 집으로 놀러오세요. 저희 가족이랑 맛있는거 먹어요!\n" +
                "오빤 얼마나 많이 먹었던지 살이 포동포동 해졌어요. 그래도 제겐 제일 세상에서 멋있는 오빠에요!\n" +
                "또 저는 얼마전에 엄마 아빠가 새로운 친구를 사주셨어요.\n" +
                "바로 분홍색 토끼인형이에요! 복슬복슬하고 정말 말랑말랑해요.\n" +
                "오, 사랑스러운 내 친구 토끼! 이름은 뭘로 지을까요?\n" +
                "토깽이랑 핑순이 중에 고민이에요. 더 좋은 이름이 있다면 알려주세요.\n\n" +
                "아, 이런. 오빠가 편지를 쓰는걸 또 한심한 눈으로 쳐다보기 시작했어요.\n" +
                "오빠는 달님한테 편지쓰는걸 되게 싫어해요.너무 상상력이 풍부해도 문제라나 뭐라나.\n" +
                "으악! 오빠가 제 편지를 뺏기 전에 오늘은 여기까지 쓸게요.\n" +
                "달님 오늘도 좋은 하루 보내세요!\n\n\n" +
                "- 달님을 누구보다도 믿고 응원하는 그레텔이");
            sw.Close();
            fs.Close();
        }

    }

    //각 멤버변수들의 설정자, 접근자
    public int getLife() { return life; }

    public void minusLife() { if (life > 0) this.life -= 1; }

    public void plusLife() { if (life < 3) this.life += 1; }

    public int getMomLife() { return momLife; }

    public void minusMomLife() { this.momLife -= 1; }

    public int getDadLife() { return dadLife; }

    public void minusDadLife() { this.dadLife -= 1; }

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

    public void setAllBread(bool allBread) { this.allBread = allBread; }

    public bool getAllBread() { return this.allBread; }

    public void setAllMonster(bool allMonster) { this.allMonster = allMonster; }

    public bool getAllMonster() { return this.allMonster; }

    public void setGo(bool go) { this.go = go; }

    public bool getGo() { return this.go; }

    public void setSkill01(bool Skill01)
    {
        this.Skill01 = Skill01;
    }

    public void setSkill02(bool Skill02)
    {
        this.Skill02 = Skill02;
    }

    public bool getSkill01() { return Skill01; }

    public bool getSkill02() { return Skill02; }

    public int getStage() { return stage; }

    public bool getSceneChanged() { return sceneChanged; }

    public void setSceneChanged(bool changed) { this.sceneChanged = changed; }

    public bool getIsPause() { return IsPause; }

    public void setIsPause()
    {
        if (IsPause) { IsPause = false; }
        else
        {
            IsPause = true;
            GameManager.instance.lastHeart = life;
            GameManager.instance.lastBread = bread;
            GameManager.instance.lastMonster = monster;
        }
    }

    public void setBread(int bread) { this.bread = bread; }

    public int getBread() { return this.bread; }

    public void setMonster(int monster) { this.monster = monster; }

    public int getMonster() { return this.monster; }
}