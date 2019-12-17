using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//게임의 Savefile을 저장할 DataStruct구조체
[Serializable]
public struct DataStruct
{
    public string saveTime;          //세이브 시간
    public int saveSceneNum;         //저장할 때 플레이했던 스테이지
    public int heart;                //저장할 때의 생명 수
    public int bread;                //저장할 때 먹은 빵가루의 수
    public int monster;              //저장할 때 죽인 몬스터의 수

    //생성자
    public DataStruct(string saveTime, int saveSceneNum, int heart, int bread, int monster)
    {
        this.saveTime = saveTime;
        this.saveSceneNum = saveSceneNum;
        this.heart = heart;
        this.bread = bread;
        this.monster = monster;
    }
}

//게임의 전체 SaveData를 저장할 GameData구조체
[Serializable]
public class GameData
{
    public DataStruct save01 = new DataStruct("", -1, -1, -1, -1);        //세이브 1번파일
    public DataStruct save02 = new DataStruct("", -1, -1, -1, -1);        //세이브 2번파일
    public DataStruct save03 = new DataStruct("", -1, -1, -1, -1);        //세이브 3번파일
    public bool[] endings = new bool[5];                                  //엔딩 획득 여부

}
