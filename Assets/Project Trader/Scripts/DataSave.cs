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
        pda.Money = loadData.Money;
        pda.Level = loadData.Level;
        pda.Awareness = loadData.Awareness;
        pda.ShopName = loadData.ShopName;
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
        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", objson);
    }
    string LoadJson()
    {
        string loadJson = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        return loadJson;
    }

    //출력,updown함수

    void PrintData(string Data)
    {
        switch (Data)
        {
            case "Money":
                textUi.GetComponent<TextUiControl>().PrintMoney(pda.Money);
                break;
            case "Tired":
                textUi.GetComponent<TextUiControl>().PrintTired(pda.MaxFatigue, pda.Fatigue);
                break;
            case "Level":
                textUi.GetComponent<TextUiControl>().PrintLevel(pda.Level);
                break;
            case "Famous":
                textUi.GetComponent<TextUiControl>().PrintFamous(pda.Awareness);
                break;
            case "ShopName":
                textUi.GetComponent<TextUiControl>().PrintShopName(pda.ShopName);
                break;
            //case "Date":
            //    textUi.GetComponent<TextUiControl>().PrintDate(pda.date);
            //    break;
            //Date에 int로 수 저장하고 TimeSpan이용하여 더하여 출력
        }
    }

    public void UseMoney(int mon)
    {
        if (pda.Money + mon > 0)
            pda.Money += mon;

        //돈이 부족할시 + 돈이 부족합니다 출력
        else
            return;
        PrintData("Money");
    }
    public void UseLevel(int lev)
    {
        pda.Level += lev;
        PrintData("Level");
    }
    public void UseFamous(int fam)
    {
        if (pda.Awareness + fam > 0)
            pda.Awareness += fam;
        //인지도는 0이하로 떨어지지 않는다
        else
            pda.Awareness = 0;
    }
}