using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using System;
using TMPro;


public class MakerTimer : MonoBehaviour
{

    Item[] make;
    ItemData[] making;
    public TextMeshProUGUI timePrint;

    bool start = false;
    float[] timer;
    int makeTableNum;//들어오는 제작창
    int printtype;
    bool[] inTimer;   //타이머가 굴러가야 하는지
    float[] allTime; //총 시간
    // Start is called before the first frame update
    void Start()
    {
        allTime = new float[3];
        make = new Item[3];
        making = new ItemData[3];
        timer = new float[3];
        inTimer = new bool[3];
        for (int i = 0; i < 3; i++)
            inTimer[i] = false;
        printtype = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PrintTime(printtype);
        
    }



    //타이머를 세팅하고 돌림
    public void StartTimer(int makertable,int cod,int cout)
    {
        if (inTimer[makertable] != true)
        {
            make[makertable].Code = cod;
            making[makertable] = make[makertable].GetData();
            makeTableNum = makertable;
            //timer[makeTableNum] = making[makeTableNum].CraftDelay*cout;
            timer[makeTableNum] = 125;
            allTime[makeTableNum] = timer[makeTableNum];
            inTimer[makertable] = true;

            if (start == false) //시작되지 않음
            {
                UnityEngine.Debug.Log("타이머 코루틴 작동시작");
                StartCoroutine("Timer");
                start = true; //시작됨
            }
        }
        else
            UnityEngine.Debug.Log("이미 작동중입니다");
    }


    IEnumerator Timer()
    {
        if (inTimer[0] == true)
        {
            timer[0] -= 1f;
            if (timer[0] <= 0)
                inTimer[0] = false;
            UnityEngine.Debug.Log("1번 : "+timer[0].ToString());
        }
        if (inTimer[1] == true)
        {
            timer[1] -= 1f;
            if (timer[1] <= 0)
                inTimer[1] = false;
            UnityEngine.Debug.Log("2번 : "+timer[1].ToString());
        }
        if (inTimer[2] == true)
        {
            timer[2] -= 1f;
            if (timer[2] <= 0)
                inTimer[2] = false;
            UnityEngine.Debug.Log("3번 : " + timer[2].ToString());
        }

        yield return new WaitForSeconds(1f);
        for (int i=0;i<3;i++)
        {
            if (inTimer[i] == true)
            {
                StartCoroutine("Timer");
                i = 4;
            }
        }
    }

    public void PrintTime(int slotcod)
    {
        if (inTimer[slotcod] ==true)
        {
            float minute;
            float second;

            minute = (int)(timer[slotcod] / 60);
            second = (timer[slotcod] - (60 * minute));
            timePrint.text = string.Format("{0:00}", minute) + ":" + string.Format("{0:00}", second);
            //timePrint.text = minute.ToString() +":"+ second.ToString();
        }
        else
            timePrint.text = "00:00";

    }
    //만드는 중이라는 신호를 보내줘야함

    public void NumSet(int slotcod)
    {
        printtype = slotcod;
    }
}
