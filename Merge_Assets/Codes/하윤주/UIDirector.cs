using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    public GameObject NowChracter;
    public GameObject SwitchChracter;
    public GameObject CharacterCool;
    public GameObject NowBar;
    public GameObject SwitchBar;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    public GameObject PauseUI;
    public GameObject ClosePanel;
    public GameObject SavePanel;
    public GameObject ASkill;
    public GameObject DSkill;
    public GameObject ASkillCool;
    public GameObject DSkillCool;
    public GameObject CountBoard;
    public GameObject Count;
    public GameObject CountIcon;
    public GameObject CountX;
    private Image nowImage;
    private Image switchImage;
    private Image characterCoolImage;
    private Image nowBar;
    private Image switchBar;
    private Image ASkillCoolImg;
    private Image DSkillCoolImg;
    private Image ASkillImg;
    private Image DSkillImg;
    private Image CountIconImg;
    private Text CountTxt;
    public Sprite HanselImg;
    public Sprite GretelImg;
    public Sprite HanselBar;
    public Sprite GretelBar;
    public Sprite HSkillA;
    public Sprite HSkillD;
    public Sprite GSkillA;
    public Sprite GSkillD;
    public Sprite BreadIcon;
    public Sprite MonsterIcon;
    private bool IsHansel = true;
    private bool nowHansel = true;
    private bool IsPause = false;
    private int heart = 3;
    private int nowHeart = 3;
    private int targetBread;
    private int targetMonster;
    private int bread;
    private int monster;
    private int IconType = 0;     // 0: UI표시 안함   1: 빵   2: 몬스터


    // Start is called before the first frame update
    void Awake()
    {
        nowImage = NowChracter.GetComponent<Image>();
        switchImage = SwitchChracter.GetComponent<Image>();
        characterCoolImage = CharacterCool.GetComponent<Image>();
        nowBar = NowBar.GetComponent<Image>();
        switchBar = SwitchBar.GetComponent<Image>();
        ASkillCoolImg = ASkillCool.GetComponent<Image>();
        DSkillCoolImg = DSkillCool.GetComponent<Image>();
        ASkillImg = ASkill.GetComponent<Image>();
        DSkillImg = DSkill.GetComponent<Image>();
        CountIconImg = CountIcon.GetComponent<Image>();
        CountTxt = Count.GetComponent<Text>();
        ClosePanel.SetActive(true);
        SavePanel.SetActive(false);
        PauseUI.SetActive(false);
    }

    void Start()
    {
        targetBread = GameDirector.instance.targetBread;
        targetMonster = GameDirector.instance.targetMonster;
        setCountUI();
    }

    // Update is called once per frame
    void Update()
    {
        characterUI();
        heartUI();
        pauseUI();
        skillUI();
        CountUI();
    }

    public void closePanelY()
    {
        StageManager.instance.setIsPause();
        Time.timeScale = 1;
        PauseUI.SetActive(false);
    }

    public void closePanelN()
    {
        ClosePanel.SetActive(false);
        SavePanel.SetActive(true);
    }

    public void savePanelY()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        StageManager.instance.setIsPause();
        StageManager.instance.save();
    }

    public void savePanelN()
    {
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        StageManager.instance.setIsPause();
        GameManager.instance.goMainScene();
    }

    private void pauseUI()
    {
        this.IsPause = StageManager.instance.getIsPause();
        if (IsPause)
        {
            Time.timeScale = 0;
            PauseUI.SetActive(true);
        }
        else
        {
            PauseUI.SetActive(false);
        }
    }

    private void heartUI()
    {
        this.heart = StageManager.instance.getLife();
        if (heart != nowHeart)
        {
            if (heart == 2)
            {
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(false);
            }
            else if (heart == 1)
            {
                Heart1.SetActive(true);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
            }
            else if (heart == 0)
            {
                Heart1.SetActive(false);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
            }
            else if (heart == 3)
            {
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(true);
            }
            nowHeart = heart;
        }
    }

    //스킬 UI를 표시하는 함수
    //플레이어가 스킬을 사용하여 StageManager의 변수 값이 바뀌면 이를 확인하고 쿨타임 코루틴 호출
    private void skillUI()
    {
        if (!StageManager.instance.getSkill01() && nowHansel) { StartCoroutine(skillCollTime(0.2f, ASkillCoolImg, 1)); }
        else if (!StageManager.instance.getSkill02() && nowHansel) { StartCoroutine(skillCollTime(3f, DSkillCoolImg, 2)); }
        else if (!StageManager.instance.getSkill01() && !nowHansel) { StartCoroutine(skillCollTime(0.5f, ASkillCoolImg, 1)); }
        else if (!StageManager.instance.getSkill02() && !nowHansel) { StartCoroutine(skillCollTime(10f, DSkillCoolImg, 2)); }

    }

    //스킬 쿨타임을 담당하는 코루틴
    private IEnumerator skillCollTime(float coolTime, Image img, int skillnum)   //각 스킬의 쿨타임, 쿨타임 이미지, 스킬 번호를 인자로 받음
    {
        img.fillAmount = 1;                   //쿨타임 이미지의 채워진 정도. 처음에는 쿨타임 이미지가 스킬 이미지를 완전히 덮음
        float leftTIme = coolTime;            //남은 시간. 처음에는 쿨타임을 저장

        while (img.fillAmount > 0)
        {
            leftTIme -= 0.01f;                         //while문을 한번 반복할때마다 남은 시간을 0.01f 감소
            float fillRatio = (leftTIme / coolTime);   //채워지는 정도는 남은시간/쿨타임으로 계산
            img.fillAmount = fillRatio;                //계산된 fiilRatio를 이미지에 적용

            yield return new WaitForSeconds(.01f);     //0.01f마다 while문 실행
        }
        //while문의 실행이 끝나면 StageManager의 스킬 변수의 값을 바꿔 플레이어가 사용할 수 있게 만듦
        if (skillnum == 1) { StageManager.instance.setSkill01(true); }
        else if (skillnum == 2) { StageManager.instance.setSkill02(true); }
    }

    private void characterUI()
    {
        this.IsHansel = StageManager.instance.getIsHansel();
        if ((IsHansel && !nowHansel) || (!IsHansel && nowHansel))
        {
            characterChangeFunc();
        }
    }

    private IEnumerator characterCoolTime(Image img, float coolTime)
    {
        img.fillAmount = 0;
        float leftTIme = coolTime;
        float delta = 0;

        while (img.fillAmount < 1)
        {
            delta += 0.01f;
            leftTIme = coolTime - delta;
            float fillRatio = (leftTIme / coolTime);
            img.fillAmount = 1 - fillRatio;

            yield return new WaitForSeconds(.01f);
        }
        StageManager.instance.setChangeCoolOver(true);
    }

    private void characterChangeFunc()
    {
        if (IsHansel)
        {
            nowImage.sprite = HanselImg;
            switchImage.sprite = GretelImg;
            nowBar.sprite = HanselBar;
            switchBar.sprite = GretelBar;
            ASkillImg.sprite = HSkillA;
            DSkillImg.sprite = HSkillD;
            nowHansel = true;
        }
        else
        {
            nowImage.sprite = GretelImg;
            switchImage.sprite = HanselImg;
            nowBar.sprite = GretelBar;
            switchBar.sprite = HanselBar;
            ASkillImg.sprite = GSkillA;
            DSkillImg.sprite = GSkillD;
            nowHansel = false;
        }
        StartCoroutine(characterCoolTime(switchBar, 3.0f));
        StageManager.instance.setSkill01(true);
        StageManager.instance.setSkill02(true);
        ASkillCoolImg.fillAmount = 0;
        DSkillCoolImg.fillAmount = 0;
    }

    private void setCountUI()
    {
        if (targetBread != 0)
        {
            CountTxt.text = string.Format("{0:D2}", targetBread);
            CountIconImg.sprite = BreadIcon;
            IconType = 1;
            bread = targetBread;
        }
        else if (targetMonster != 0)
        {
            CountTxt.text = string.Format("{0:D2}", targetMonster);
            CountIconImg.sprite = MonsterIcon;
            IconType = 2;
            monster = targetMonster;
        }
        else { countUIOff(); IconType = 0; }
    }

    private void CountUI()
    {
        if (IconType == 1)
        {
            int newBread = GameDirector.instance.getBread();
            if (bread != targetBread - newBread)
            {
                bread = targetBread - newBread;
                CountTxt.text = string.Format("{0:D2}", bread);
            }
        }
        else if (IconType == 2)
        {
            int newMonster = GameDirector.instance.getMonster();
            if (monster != targetMonster - newMonster)
            {
                monster = targetMonster - newMonster;
                CountTxt.text = string.Format("{0:D2}", monster);
            }
        }
        else { }
    }


    private void countUIOff()
    {
        Count.SetActive(false);
        CountBoard.SetActive(false);
        CountIcon.SetActive(false);
        CountX.SetActive(false);
    }
}
