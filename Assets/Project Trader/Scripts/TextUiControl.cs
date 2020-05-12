using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class TextUiControl : MonoBehaviour
{
    //textbox프리팹 적용(이미지-텍스트)
    public GameObject[] textBox;
    GameObject playData;
    GameObject textObject;
    GameObject canvas;
    RectTransform tbPos;
    TextMeshProUGUI tmp;
    //텍스트가 나타났다 사라지는 시간
    //public int textBoxTime=2;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        tbPos = GetComponent<RectTransform>();
        playData = GameObject.Find("SaveData");
        playData.GetComponent<DataSave>();
    }


    void Update()
    {

    }
    // 말풍선의 종류, x위치,y위치,문자열+유지시간 추가가능,유지시간
    public void CreativeTextBox(int a, int x, int y, string gg, int ti)
    {
        GameObject te = Instantiate(textBox[a]) as GameObject;
        TextMeshProUGUI text_re = te.GetComponentInChildren<TextMeshProUGUI>();

        //string적용
        text_re.text = gg;

        //캔버스에 생성
        te.transform.SetParent(canvas.transform);
        tbPos = te.GetComponent<RectTransform>();

        //위치조절
        tbPos.anchoredPosition = new Vector3(x, y);

        //제거
        Destroy(te, ti);
    }

    public void PrintMoney(int money)
    {
        textObject = GameObject.Find("Money");
        tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = money.ToString();

    }

    public void PrintFamous(float famous)
    {
        textObject = GameObject.Find("Famous");
        tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = famous.ToString();

    }

    public void PrintTired(int tired,int remove)
    {
        textObject = GameObject.Find("Tired");
        tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = remove.ToString() + " / " + tired.ToString();

    }

    public void PrintLevel(int level)
    {
        textObject = GameObject.Find("Level");
        tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = "Lv. "+level.ToString();

    }
    
    public void PrintDate()
    {
        GameObject date = GameObject.Find("SaveData");
        string time = date.GetComponent<TimeControl>().gameDay.ToString("MM / dd");

        textObject = GameObject.Find("Date");
        tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = time;

    }
    

    public void PrintShopName(string name)
    {
        textObject = GameObject.Find("ShopName");
        tmp = textObject.GetComponent<TextMeshProUGUI>();
        tmp.text = name;
    }


}
