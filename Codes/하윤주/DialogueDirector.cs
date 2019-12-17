using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue
{
    public string character;
    public string chat;

    public Dialogue(string character, string chat)
    {
        this.character = character;
        this.chat = chat;
    }
}

public class DialogueDirector : MonoBehaviour
{
    public GameObject DialogueUI;
    public Text chatField;
    public Text character;
    private bool next = true;
    private bool condition = false;
    private bool chatEnd = false;
    private bool skip = false;
    private bool dialogueEnd = false;
    private List<Dialogue> tutorialChat = new List<Dialogue>();
    private List<Dialogue> candyChat = new List<Dialogue>();
    private List<Dialogue> witchChat01 = new List<Dialogue>();
    private List<Dialogue> witchChat02 = new List<Dialogue>();
    private List<Dialogue> witchChat03 = new List<Dialogue>();
    private List<Dialogue> witchChat04 = new List<Dialogue>();
    private List<Dialogue> witchChat05 = new List<Dialogue>();

    // Start is called before the first frame update
    void Awake()
    {
        DialogueUI.SetActive(false);

        tutorialChat.Add(new Dialogue("엄마", "어서 숲으로 가지 않고 뭐하니? \n눈치가 저리도 없어서야…"));
        tutorialChat.Add(new Dialogue("아빠", "자기 앞가림은 할 줄 알아야지. 가서 산딸기라도 캐 와. \n하나도 못따오면 오늘 저녁은 없다."));
        candyChat.Add(new Dialogue("그레텔", "오빠 나 너무 배고파..  \n숲을 너무 오래 걸어서 힘들어…"));
        candyChat.Add(new Dialogue("헨젤", "그레텔 조금만 참... 어? \n저기 좀 봐. 저기 과자집이야"));
        candyChat.Add(new Dialogue("그레텔", "어 진짜? 과자집이다! \n우와아 맛있는 과자가 잔-뜩 붙어있어!"));
        candyChat.Add(new Dialogue("그레텔", "빨리 가보자 오빠!!"));
        witchChat01.Add(new Dialogue("마녀", "함부로 남의 집에 들어온 어린이들에겐 벌을 주겠어요! 오호홋!"));
        witchChat02.Add(new Dialogue("마녀", "어떻게 요리를 해야 잘했다고 소문이 날까~?"));
        witchChat03.Add(new Dialogue("마녀", "피하는 꼴이 꼭 쥐새끼 같구나!"));
        witchChat04.Add(new Dialogue("마녀", "얌전히 잡히지 못해!?"));
        witchChat05.Add(new Dialogue("마녀", "으윽..안 돼..내 보석...내 보물...!!!"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextBtn()
    {
        if (chatEnd) { next = true; }
        else { skip = true; }
    }

    public void chatFunc(int chatCase)
    {
        next = true;
        Time.timeScale = 0;
        DialogueUI.SetActive(true);
        if (chatCase == 1) { StartCoroutine(startChat(tutorialChat)); }
        else if (chatCase == 2) { StartCoroutine(startChat(candyChat)); }
        else if (chatCase == 3) { StartCoroutine(startChat(witchChat01)); }
        else if (chatCase == 4) { StartCoroutine(startChat(witchChat02)); }
        else if (chatCase == 5) { StartCoroutine(startChat(witchChat03)); }
        else if (chatCase == 6) { StartCoroutine(startChat(witchChat04)); }
        else if (chatCase == 7) { StartCoroutine(startChat(witchChat05)); }
    }

    //대사들을 출력하는 코루틴
    private IEnumerator startChat(List<Dialogue> chats)     //출력할 대사들를 Dialogue구조체의 리스트 형태의 chats 인자로 받음
    {
        int i = 0;
        dialogueEnd = false;                                //대사들이 완전히 출력되었는지를 확인하는 변수. 처음엔 false로 설정

        while (i < chats.Count)                             //리스트에 있는 대사의 수만큼 while문을 반복하며 대사를 하나씩 출력
        {
            if (next)                                       //next가 true(다음 대사를 출력해도 되는 경우)면 
            {
                chatEnd = false;                            //대사 하나가 완전히 출력되었는지를 확인하는 변수. 처음엔 false로 설정
                StartCoroutine(NormalChat(chats[i]));       //대사를 출력하는 코루틴 호출
                i++;
            }
            yield return null;                              //1프레임마다 while문 확인
        }
        while (true)                                        //조건이 만족될 때까지 무한루프(위의 while문이 종료된 뒤 실행)
        {
            if (chatEnd && next)                              //대사 하나의 출력이 끝났고 next가 true이면
            {
                DialogueUI.SetActive(false);                //대사를 출력하는 UI를 비활성화
                Time.timeScale = 1;                         //다시 시간을 흐르게 함
                dialogueEnd = true;                         //대사들이 완전히 출력되었으므로 true로 변경
                yield break;                                //while문 탈출
            }
            yield return null;                              //1프레임마다 while문 확인
        }
    }

    //대사 하나를 출력하는 코루틴(대사가 한글자씩 출력되도록 함)
    private IEnumerator NormalChat(Dialogue chat)            //하나의 대사를 Dialogue구조체 형태의 인자로 받음
    {
        next = false;                                        //next를 false로 설정 
        string narrator = chat.character;                    //인자로 받은 Dialogue구조체에서 대사를 하는 캐릭터 변수를 narrator에 저장
        string narration = chat.chat;                        //인자로 받은 Dialogue구조체에서 대사 변수를 narration에 저장
        int a = 0;
        character.text = narrator;                           //대사UI에 대사를 하는 캐릭터를 출력
        string writerText = "";                              //대사UI에 출력할 대사

        for (a = 0; a < narration.Length; a++)               //대사의 길이만큼 for문 반복
        {
            writerText += narration[a];                      //for문을 반복하며 한글자씩 writerText에 추가
            chatField.text = writerText;                     //대사UI에 writerText출력

            if (skip)                                        //대사를 다 출력하기 전에 skip이 눌린다면
            {
                chatField.text = narration;                  //대사 UI에 전체 대사를 출력
                chatEnd = true;                              //대사 하나가 완전히 끝났기 떄문에 chatEnd를 true로 변경
                skip = false;                                //skip변수를 다시 false로 변경
                yield break;                                 //for문 탈출
            }
            yield return new WaitForSecondsRealtime(0.02f);  //0.02f에 한번씩 for문 확인(2프레임에 한 글자씩 출력되도록)
        }
        chatEnd = true;                                      //skip을 하지 않고 대사가 다 출력되었을 경우 역시 chatEnd를 true로 변경
    }

    public void setCondition() { this.condition = true; }

    public bool getDialogueEnd() { return this.dialogueEnd; }
}
