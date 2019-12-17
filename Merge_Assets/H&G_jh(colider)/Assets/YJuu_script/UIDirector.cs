using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour
{
    public GameObject NowChracter;
    public GameObject SwitchChracter;
    public GameObject CharacterCool;
    private Image nowImage;
    private Image switchImage;
    private Image characterCoolImage;
    public Sprite HanselImg;
    public Sprite GretelImg;
    private bool IsHansel = true;
    private bool nowHansel = true;
    private bool characterCoolOver = false;

    // Start is called before the first frame update
    void Start()
    {
        nowImage = NowChracter.GetComponent<Image>();
        switchImage = SwitchChracter.GetComponent<Image>();
        characterCoolImage = CharacterCool.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        this.IsHansel = StageManager.instance.getIsHansel();
        this.characterCoolOver = StageManager.instance.getChangeCoolOver();
        characterUI();
        //delta = Time.deltaTime;
    }

    private void characterUI()
    {
        if ((IsHansel && !nowHansel) || (!IsHansel && nowHansel))
        {
            characterChangeFunc();
        }
    }
/*
    private IEnumerator characterCoolTime(Image img, float coolTime)
    {
        float leftTIme = coolTime;
        float delta = 0;

        while (img.fillAmount < 1)
        {
            delta = Time.deltaTime;
            img.fillAmount = 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }
        if (!characterCoolOver)
        {
            delta = Time.deltaTime;
            if (leftTIme > 0)
            {
                leftTIme = coolTime - delta;
                fillRatio = (leftTIme / coolTime);
                img.fillAmount = fillRatio;
            }
            else { coolOver = true; }
        }
    }
    */
    private void characterChangeFunc()
    {
        if (IsHansel)
        {
            nowImage.sprite = HanselImg;
            switchImage.sprite = GretelImg;
            nowHansel = true;
        }
        else
        {
            nowImage.sprite = GretelImg;
            switchImage.sprite = HanselImg;
            nowHansel = false;
        }
        //characterCoolOver = false;
    }

}
