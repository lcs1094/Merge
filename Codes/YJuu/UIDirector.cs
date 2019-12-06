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
    private Image nowImage;
    private Image switchImage;
    private Image characterCoolImage;
    private Image nowBar;
    private Image switchBar;
    public Sprite HanselImg;
    public Sprite GretelImg;
    public Sprite HanselBar;
    public Sprite GretelBar;
    private bool IsHansel = true;
    private bool nowHansel = true;
    private bool characterCoolOver = false;

    // Start is called before the first frame update
    void Start()
    {
        nowImage = NowChracter.GetComponent<Image>();
        switchImage = SwitchChracter.GetComponent<Image>();
        characterCoolImage = CharacterCool.GetComponent<Image>();
        nowBar = NowBar.GetComponent<Image>();
        switchBar = SwitchBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        this.IsHansel = StageManager.instance.getIsHansel();
        this.characterCoolOver = StageManager.instance.getChangeCoolOver();
        characterUI();
    }

    private void characterUI()
    {
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

        while (img.fillAmount <1)
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
            nowHansel = true;
        }
        else
        {
            nowImage.sprite = GretelImg;
            switchImage.sprite = HanselImg;
            nowBar.sprite = GretelBar;
            switchBar.sprite = HanselBar;
            nowHansel = false;
        }
        StartCoroutine(characterCoolTime(switchBar, 3.0f));
    }

}
