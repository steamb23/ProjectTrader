using ProjectTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;

public class DataSave : MonoBehaviour
{
    GameObject textUi;
    GameObject sell;
    //public PlayData pda = new PlayData();

    //임시 인벤토리/30개만 저장
    public Item[] playerItem;
    //임시 알바고용
    Employee[] empInfo;
    int empsize;

    private void Awake()
    {
        GameLoad();
    }

    void Start()
    {
        textUi = GameObject.Find("TextUiControl");
        sell = GameObject.Find("selltimewindow");
        empInfo = new Employee[9];
        empsize = 0;
        playerItem = new Item[5];
        //또다시 임시 초기화->저장된 데이터에서 받아오도록 수정
        for (int i = 0; i < 5; i++)
        {
            playerItem[i].Code = i + 1;
            playerItem[i].Count = 6;
        }
    }

    void Update()
    {

    }

    public void GameSave()
    {
        var pda = PlayData.CurrentData;

        string jsondata = ObjsonMake(pda);
        SaveJson(jsondata);
        textUi.GetComponent<TextUiControl>().CreativeTextBox(0, 0, 100, "Game Save!", 2);
    }

    public void GameLoad()
    {
        string load = LoadJson();
        Debug.Log($"데이터 로드 : {load}");
        var loadData = JsonToObject<PlayData>(load);

        if (loadData != null && loadData.IsInitialized)
        {
            PlayData.CurrentData = loadData;
            Debug.Log("로드 성공");
        }
        else
        {
            Debug.Log("로드 실패, 새 데이터로 시작");

            // 게임 데이터베이스와 동기화
            PlayData.CurrentData.SyncData();
        }
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
        try
        {
            return File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("파일 없음...");
            Debug.Log(e);

            return "";
        }
    }

    //출력,updown함수

    void PrintData(string Data)
    {
        var pda = PlayData.CurrentData;

        switch (Data)
        {
            case "Money":
                textUi.GetComponent<TextUiControl>().PrintMoney(pda.Money);
                break;
            case "Tired":
                textUi.GetComponent<TextUiControl>().PrintTired(pda.MaxStamina, pda.Stamina);
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

    public bool UseMoney(int mon)
    {
        var pda = PlayData.CurrentData;

        if (pda.Money + mon > 0)
        {
            pda.Money += mon;
        }

        //돈이 부족할시 + 돈이 부족합니다 출력
        else
            return false;
        PrintData("Money");
        return true;
    }
    public void UseLevel(int lev)
    {
        var pda = PlayData.CurrentData;

        pda.Level += lev;
        PrintData("Level");
    }
    public void UseFamous(int fam)
    {
        var pda = PlayData.CurrentData;

        if (pda.Awareness + fam > 0)
            pda.Awareness += fam;
        //인지도는 0이하로 떨어지지 않는다
        else
            pda.Awareness = 0;
    }

    //임시로 코드별로 먼저 선언해 넣은 뒤 count로만 체크
    public bool UseItem(int cod, int count)
    {
        for (int i = 0; i < 5; i++)
        {
            if (playerItem[i].Code == cod)
            {
                if (playerItem[i].Count + count >= 0)
                {
                    playerItem[i].Count += count;
                    return true;
                }
                else
                    return false;
            }
        }
        return false;
    }


    public void SetItemList()
    {
        sell.GetComponent<SellWindow>().SetItem(playerItem);
    }

    //알바생 추가/제거 check가 1이면 추가, 0이면 제거
    public void UseEmp(Employee emp, int check)
    {
        if (check == 1) //추가
        {
            if (empsize < 9)
            {
                empInfo[empsize] = emp;
                empsize++;
            }
        }
        else if (check == 0) //제거
        {
            for (int i = 0; i < empsize; i++)
            {
                if (empInfo[i].Code == emp.Code)
                {
                    if (i == empsize)
                        empsize--;
                    else
                    {
                        for (int j = i; j < empsize - 1; j++)
                        {
                            empInfo[j] = empInfo[j + 1];
                        }
                        empsize--;
                    }
                }
            }
        }
    }

    //임시로 직원을 찾아 해고/고용 하는 코드
    public void FHireEmp(Employee emp, int j)
    {
        for (int i = 0; i < empsize; i++)
        {
            if (empInfo[i] == emp)
                UseEmp(empInfo[i], j);
        }
        //Debug.LogError("검사함");
    }

    //playData 아이템 추가용
    public void ItemListAdd(Item initem)
    {
        bool initemtrue = false;
        Item listItem;
        if (PlayData.CurrentData.OwnedItems.Count > 0)
        {
            for (int i = 0; i < PlayData.CurrentData.OwnedItems.Count; i++)
            {
                if (PlayData.CurrentData.OwnedItems[i].Code == initem.Code)
                {
                    listItem = PlayData.CurrentData.OwnedItems[i];
                    initem.Count += listItem.Count;
                    PlayData.CurrentData.OwnedItems[i]= initem;
                    initemtrue = true;
                }

            }
            if (initemtrue == false)
            {
                PlayData.CurrentData.OwnedItems.Add(initem);
            }
        }
        else
        {
            PlayData.CurrentData.OwnedItems.Add(initem);
        }

        //테스트용
        for(int i = 0; i < PlayData.CurrentData.OwnedItems.Count; i++)
        {
            listItem = PlayData.CurrentData.OwnedItems[i];
            UnityEngine.Debug.Log("들어간 아이템 코드: "+listItem.Code.ToString()+"들어간 아이템 수량 "+listItem.Count.ToString());
        }
    }
}