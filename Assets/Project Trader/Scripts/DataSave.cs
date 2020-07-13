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


        // 아이템 설정

        // 진열된 아이템 오브젝트들 가져오기
        var displayedItemComponents = FindObjectsOfType<DisplayedItem>();

        for (int i = 0; i < PlayData.CurrentData.DisplayedItems.Count && i < displayedItemComponents.Length; i++)
        {
            var displayedItem = PlayData.CurrentData.DisplayedItems[i];
            displayedItemComponents[i].Item = displayedItem;
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
            PlayData.CurrentData.SyncData();
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


    //playData 아이템 추가용
    public void ItemListAdd(Item initem)
    {
        bool initemtrue = false; //같은 코드의 아이템이 있나 검사
        Item listItem;
        if (PlayData.CurrentData.OwnedItems.Count > 0)
        {
            for (int i = 0; i < PlayData.CurrentData.OwnedItems.Count; i++)
            {
                if (PlayData.CurrentData.OwnedItems[i].Code == initem.Code)
                {
                    listItem = PlayData.CurrentData.OwnedItems[i];
                    initem.Count += listItem.Count;
                    if (initem.Count > 0) //개수가 0이 넘으면
                    {
                        PlayData.CurrentData.OwnedItems[i] = initem;
                    }
                    else //갯수가 0이면
                    {
                        PlayData.CurrentData.OwnedItems.RemoveAt(i);
                        // UnityEngine.Debug.Log("제거!");
                    }
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

        ////테스트용
        //for(int i = 0; i < PlayData.CurrentData.OwnedItems.Count; i++)
        //{
        //    listItem = PlayData.CurrentData.OwnedItems[i];
        //    //UnityEngine.Debug.Log("들어간 아이템 코드: "+listItem.Code.ToString()+"들어간 아이템 수량 "+listItem.Count.ToString());
        //}
    }

    public void DisplayItemListRemove(Item item)
    {
        if (PlayData.CurrentData.DisplayedItems.Count <= 0 || PlayData.CurrentData.DisplayedItems == null)
            return;

        for (int i = 0; i < PlayData.CurrentData.DisplayedItems.Count; i++)
        {
            Item listinItem = PlayData.CurrentData.DisplayedItems[i];
            if (listinItem.Code == item.Code)
            {
                PlayData.CurrentData.DisplayedItems.RemoveAt(i);
            }
        }
        //테스트용
        //if (PlayData.CurrentData.DisplayedItems.Count > 0)
        //{
        //    for (int i = 0; i < PlayData.CurrentData.DisplayedItems.Count; i++)
        //    {
        //        Item listItem = PlayData.CurrentData.DisplayedItems[i];
        //        UnityEngine.Debug.Log("배치 아이템 코드: " + listItem.Code.ToString() + "배치된 아이템 수량 " + listItem.Count.ToString());
        //    }
        //}
    }

    //해고
    public void EmployeeListRemove(Employee emp)
    {
        for (int i = 0; i < PlayData.CurrentData.HiredEmployees.Count; i++)
        {
            Employee listemp = PlayData.CurrentData.HiredEmployees[i];
            if (listemp.Code == emp.Code)
            {
                PlayData.CurrentData.HiredEmployees.RemoveAt(i);
            }
        }
    }

    public void EmpAdd(Employee emp)
    {
        PlayData.CurrentData.HiredEmployees.Add(emp);
    }

    //아이템 유무를 확인(코드/개수)
    public bool CheckGetItem(int cod,int count) 
    {
        if (PlayData.CurrentData.OwnedItems.Count <= 0)
            return false;
        for(int i=0;i< PlayData.CurrentData.OwnedItems.Count; i++)
        {
            if (PlayData.CurrentData.OwnedItems[i].Code == cod)
            {
                if (PlayData.CurrentData.OwnedItems[i].Count >= count)
                    return true;
                return false;
            }
        }
        return false;
    }
}