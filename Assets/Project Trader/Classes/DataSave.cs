using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    GameObject textUi;
    PlayData pda = new PlayData();
    void Start()
    {
        textUi = GameObject.Find("TextUiControl");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameLoad();
            pda.PrintPlayData();
        }
    }

    public void GameSave()
    {
        string jsondata = ObjsonMake(pda);
        SaveJson(jsondata);
        textUi.GetComponent<TextUiControl>().CreativeTextBox(0,0,100,"Game Save!",2);
    }

    public void GameLoad()
    {
        string load = LoadJson();
        Debug.Log(load);
        var loadData = JsonToObject<PlayData>(load);
        pda.SetMoney(loadData.money);
        pda.SetLevel(loadData.level);
        pda.SetFamous(loadData.famous);
        pda.SetShopName(loadData.shopName);

    }

    string ObjsonMake(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    void SaveJson(string objson)
    {
        Debug.Log("save");
        File.WriteAllText(Application.dataPath + "/Project Trader/Classes/SaveData.json", objson);
    }
    string LoadJson()
    {
        string loadJson = File.ReadAllText(Application.dataPath + "/Project Trader/Classes/SaveData.json");
        return loadJson;
    }
}