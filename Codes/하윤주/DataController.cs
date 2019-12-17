using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    static DataController Instance;
    public static DataController instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<DataController>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newDataController = new GameObject("DataController").AddComponent<DataController>();
                    Instance = newDataController;
                }
            }
            return Instance;
        }
        private set
        {
            Instance = value;
        }
    }
    public string SaveDataFileName = "TheTell.json";
    GameData _saveData;
    public GameData saveData
    {
        get
        {
            if (_saveData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _saveData;
        }
    }

    string saveTime = "";
    int saveSceneNum = -1;
    int heart = -1;
    int bread = 0;
    int monster = 0;
    public bool[] endings = new bool[5];

    public DataStruct data;

    //저장할 정보를 얻어오는 함수
    private void getData()
    {
        saveTime = DateTime.Now.ToString("yyyy.MM.dd_HH:mm:ss");   //세이브 시간을 정해진 형태로 saveTime에 저장
        saveSceneNum = GameManager.instance.lastPlayScene;         //GameManager로부터 나머지 세이브 정보를 받아와 저장
        heart = GameManager.instance.lastHeart;
        bread = GameManager.instance.lastBread;
        monster = GameManager.instance.lastMonster;
        this.endings = GameManager.instance.endings;
    }

    //저장하기
    public void Save(int num)                                                  //몇 번째 세이브 파일에 저장할 것인지를 인자로 받음
    {
        getData();                                                             //저장할 정보를 얻어옴
        data = new DataStruct(saveTime, saveSceneNum, heart, bread, monster);  //얻어온 정보들도 DataStruct구조체 data에 저장      
        if (num == 1) { saveData.save01 = data; }                               //세이브 번호에 맞게 각각의 세이브 파일에 저장
        else if (num == 2) { saveData.save02 = data; }
        else if (num == 3) { saveData.save03 = data; }
        saveData.endings = this.endings;                                       //엔딩획득 여부 저장
        SaveGameData();                                                        //GameData저장
    }

    //불러온 정보를 저장하는 함수
    private void setData()
    {
        GameManager.instance.lastHeart = data.heart;                //GameManager의 해당 변수에 불러온 정보를 저장
        GameManager.instance.lastBread = data.bread;
        GameManager.instance.lastMonster = data.monster;
    }

    //불러오기
    public void Load(int num)                                       //몇 번째 세이브 파일을 불러올 것인지를 인자로 받음
    {
        LoadGameData();                                             //GameData불러오기
        if (num == 1) { data = saveData.save01; setData(); }         //세이브 번호에 맞게 각각의 세이브 파일에서 불러오기
        else if (num == 2) { data = saveData.save02; setData(); }
        else if (num == 3) { data = saveData.save03; setData(); }
        GameManager.instance.endings = saveData.endings;            //엔딩획득 여부 불러오기
    }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //GameData 불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + SaveDataFileName;   //파일저장 경로를 filePath에 저장

        if (File.Exists(filePath))                                             //경로에 해당 파일이 존재하면
        {
            Debug.Log("불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath);                  //파일을 읽어 FromJsonData에 저장
            _saveData = JsonUtility.FromJson<GameData>(FromJsonData);          //JsonUtility를 이용하여 GameData를 불러와 _saveData에 저장
        }
        else                                                                   //경로에 해당 파일이 존재하지 않으면
        {
            Debug.Log("새로운 파일 생성");
            _saveData = new GameData();                                        //_saveData에 새로운 GameData 저장
        }
    }

    //GameData 저장하기
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(saveData);                     //JsonUtility를 이용하여 saveData의 내용을 ToJsonData에 저장
        string filePath = Application.persistentDataPath + SaveDataFileName;  //파일저장 경로를 filePath에 저장
        File.WriteAllText(filePath, ToJsonData);                              //filePath에 있는 파일에 ToJsonData의 내용을 쓰기
        Debug.Log("저장완료");
        Debug.Log(filePath);
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
