using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadDirector : MonoBehaviour
{
    public Text Bt01Txt;
    public Text Bt02Txt;
    public Text Bt03Txt;
    private string defaultTxt = "No Data";
    private string printTxt = "No Data";
    private string saveTime = "";
    private int sceneNum = -1;
    private DataStruct data;


    // Start is called before the first frame update
    void Start()
    {
        setAllButton();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void setAllButton()
    {
        for(int i = 1; i < 4; i++) { setButton(i); }
    }

    public void saveS01()
    {
        DataController.instance.Save(1);
        setButton(1);
    }
    
    public void saveS02()
    {
        DataController.instance.Save(2);
        setButton(2);
    }
    
    public void saveS03()
    {
        DataController.instance.Save(3);
        setButton(3);
    }

    public void LoadS01()
    {
        DataController.instance.Load(1);
        loadScene(DataController.instance.data.saveSceneNum);
    }

    public void LoadS02()
    {
        DataController.instance.Load(2);
        loadScene(DataController.instance.data.saveSceneNum);
    }

    public void LoadS03()
    {
        DataController.instance.Load(3);
        loadScene(DataController.instance.data.saveSceneNum);
    }

    public void goMain() { GameManager.instance.goMainScene(); }

    private void loadScene(int num)
    {
        if(num == 0) { GameManager.instance.goTutorialScene(); }
        if(num == 1) { GameManager.instance.goForestScene(); }
        if(num == 2) { GameManager.instance.goCandyScene(); }
        if(num == 3) { GameManager.instance.goLavaScene(); }
    }

    private void setText()
    {   if (data.saveSceneNum != -1)
        {
            this.saveTime = data.saveTime;
            this.sceneNum = data.saveSceneNum;
            printTxt = saveTime + "   " +sceneText(sceneNum);
        }
    else { printTxt = defaultTxt;}
    }

    private string sceneText(int num)
    {
        string txt = "";
        if(num == 0) { txt = "Tutorial"; }
        else if(num == 1) { txt = "Forest"; }
        else if(num == 2) { txt = "Candy"; }
        else if(num == 3) { txt = "Lava"; }
        return txt;
    }

    private void setButton(int num)
    {
        if (num == 1)
        {
            data = DataController.instance.saveData.save01;
            setText();
            Bt01Txt.text = printTxt;
        }
        else if (num == 2)
        {
            data = DataController.instance.saveData.save02;
            setText();
            Bt02Txt.text = printTxt;
        }
        else if (num == 3)
        {
            data = DataController.instance.saveData.save03;
            setText();
            Bt03Txt.text = printTxt;
        }
    }
}
