using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;

public class ShopTimer : MonoBehaviour
{
    GameObject shopwindow;
    bool[] inTimer;
    float[] itemDelay;
    int[] itembuyNum;
    int[] Maxvalue;


    void Start()
    {
        shopwindow = GameObject.Find("itemshop");

        //임시 초기화-material개수만큼으로 변경
        inTimer = new bool[55];
        itemDelay = new float[55];
        itembuyNum = new int[55];

        for (int i = 0; i < 55; i++)
            itembuyNum[i] = 0;

        Maxvalue = new int[55];

    }

    //타이머를 호출하기위한 준비 임시로 delay가아니라 10를 할당
    public void SetInfo(int bn,int cod)
    {
        itembuyNum[cod - 1] += bn;
        if (inTimer[cod-1] != true)
        {
            inTimer[cod - 1] = true;
            StartCoroutine(Timer(10, cod));
        }

    }

    //타이머가0이 됐을때 호출하여 전부 생성된것인지 판단
    void MaxNumCheck(int cod)
    {
        itembuyNum[cod - 1] -= 1;
        shopwindow.GetComponent<ShopWindow>().SetbuyNum(cod, -1);
        shopwindow.GetComponent<ShopWindow>().SetshopslotData(cod - 1);
        if (itembuyNum[cod - 1] <= 0)
        {
            inTimer[cod - 1] = false;
        }
        else
            StartCoroutine(Timer(10, cod));
    }

    //딜레이,아이템 코드
    IEnumerator Timer(int slottimer,int cod)
    {
        
        slottimer -= 1;

        shopwindow.GetComponent<ShopWindow>().SetTimePrint(cod - 1, inTimer[cod - 1], slottimer);
        yield return new WaitForSeconds(1f);

        if (slottimer <= 0)
        {
            MaxNumCheck(cod);
        }
        else
            StartCoroutine(Timer(slottimer, cod));
    }
}
