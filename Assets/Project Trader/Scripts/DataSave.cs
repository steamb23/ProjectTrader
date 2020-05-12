using ProjectTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    GameObject textUi;
    public PlayData pda = new PlayData();

    void Start()
    {
        textUi = GameObject.Find("TextUiControl");
        GameLoad();
    }

    void Update()
    {

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
        UnityEngine.Debug.Log(load);
        var loadData = JsonToObject<PlayData>(load);
        pda.money = loadData.money;
        pda.level = loadData.level;
        pda.famous = loadData.famous;
        pda.shopName = loadData.shopName;
        //출력함수추가
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
        UnityEngine.Debug.Log("save");
        File.WriteAllText(Application.dataPath + "/Project Trader/Classes/SaveData.json", objson);
    }
    string LoadJson()
    {
        string loadJson = File.ReadAllText(Application.dataPath + "/Project Trader/Classes/SaveData.json");
        return loadJson;
    }

    //출력,updown함수

    void PrintData(string Data)
    {
        switch (Data)
        {
            case "Money":
                textUi.GetComponent<TextUiControl>().PrintMoney(pda.money);
                break;
            case "Tired":
                textUi.GetComponent<TextUiControl>().PrintTired(pda.maxTired, pda.tired);
                break;
            case "Level":
                textUi.GetComponent<TextUiControl>().PrintLevel(pda.level);
                break;
            case "Famous":
                textUi.GetComponent<TextUiControl>().PrintFamous(pda.famous);
                break;
            case "ShopName":
                textUi.GetComponent<TextUiControl>().PrintShopName(pda.shopName);
                break;
            //case "Date":
            //    textUi.GetComponent<TextUiControl>().PrintDate(pda.date);
            //    break;
            //Date에 int로 수 저장하고 TimeSpan이용하여 더하여 출력
        }
    }

    public void UseMoney(int mon)
    {
        if (pda.money + mon > 0)
            pda.money += mon;

        //돈이 부족할시 + 돈이 부족합니다 출력
        else
            return;
        PrintData("Money");
    }
    public void UseLevel(int lev)
    {
        pda.level += lev;
        PrintData("Level");
    }
    public void UseFamous(int fam)
    {
        if (pda.famous + fam > 0)
            pda.famous += fam;
        //인지도는 0이하로 떨어지지 않는다
        else
            pda.famous = 0;
    }
}