using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using System;
using TMPro;
using System.Threading;
using System.Linq;

public class MakerTimer : MonoBehaviour
{
    Item[] make;
    public ItemData[] making
    {
        get;
        set;
    }
    public TextMeshProUGUI timePrint;
    Item stopitem;
    GameObject makeui;
    float[] timer;
    int makeTableNum;//들어오는 제작창
    int printtype;
    public bool[] inTimer
    {
        get;
        set;
    }//타이머가 굴러가야 하는지
    bool[] timerstop= new bool[3];
    void Start()
    {
        makeui = GameObject.Find("makeroom");
        make = new Item[3];
        making = new ItemData[3];
        timer = new float[3];
        inTimer = new bool[3];
        for (int i = 0; i < 3; i++)
            inTimer[i] = false;
        printtype = 0;
    }


    void Update()
    {
        PrintTime(printtype);
        
    }

    //타이머를 세팅하고 돌림
    public void StartTimer(int makertable,int cod,int cout)
    {
        if (inTimer[makertable] != true || timerstop[makertable]==true)
        {
            make[makertable].Code = cod;
            make[makertable].Count = cout;

            making[makertable] = make[makertable].GetData();
            makeTableNum = makertable;

            if (making[makertable].CraftDelay>0)
                timer[makeTableNum] = making[makertable].CraftDelay;
            else
                timer[makeTableNum] = 10;

            inTimer[makertable] = true;
            timerstop[makeTableNum] = false;

            UnityEngine.Debug.Log("타이머 코루틴 작동시작");
            StartCoroutine(Timer(inTimer[makertable], makertable));

        }
        else
            UnityEngine.Debug.Log("이미 작동중입니다");
    }

    IEnumerator Timer(bool inTimer,int i)
    {
        if (inTimer == true&& timerstop[i] ==false)
        {
            UnityEngine.Debug.Log((i+1).ToString()+"번 : " + timer[i].ToString());
            timer[i] -= 1f;
            if (timer[i] <= 0f)
            {
                MakeItem(i);
                yield break;
            }
        }
        else
            yield break;

        yield return new WaitForSeconds(1f);
        StartCoroutine(Timer(inTimer, i));
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
        }
        else
            timePrint.text = "00:00";

    }


    public void NumSet(int slotcod)
    {
        printtype = slotcod;
    }

    //count에서 1을 빼고 0이 아닐시 코루틴 호출/true로 변형
    void MakeItem(int i)
    {
        make[i].Count -= 1;
        UnityEngine.Debug.Log((i+1).ToString()+"번 count="+(make[i].Count).ToString());
        if (make[i].Count > 0)
        {
            FindObjectOfType<MakerUI>().MakeSuccess(make[i]);
            if (making[i].CraftDelay > 0)
                timer[i] = making[i].CraftDelay;
            else
                timer[i] = 10;
            StartCoroutine(Timer(inTimer[i],i));
        }
        else
        {
            inTimer[i] = false;
            UnityEngine.Debug.Log((i+1).ToString()+"번 종료");
            FindObjectOfType<MakerUI>().MakeSuccess(make[i]);
            FindObjectOfType<MakeEmpslot>().PrintMakeItemSprite();
            //아이템개수+1
        }
    }

    public void StopTimer()
    {
        int slotcod = FindObjectOfType<MakeEmpslot>().clickEmployee - 1;
        UnityEngine.Debug.Log("타이머 중단 호출 / 슬롯 코드 : " + slotcod.ToString());
        timerstop[slotcod] = true;
        FindObjectOfType<MakerUI>().working[slotcod] = false;
        ReturnMaterial(slotcod);
    }

    //제작 중단 시 남은 count만큼 재료 회수
    public void ReturnMaterial(int slotcod)
    {
        if (make[slotcod].Count > 0)
        {
            //makecount 에서 남은 만큼 코드로 재료 불러와서 가진 아이템에 추가
            int[] mtcod = new int[4];
            int[] mtcount = new int[4];

            mtcod = making[slotcod].MaterialCodes;
            mtcount = making[slotcod].MaterialNeeds;

            for (int i = 0; i < 4; i++)
            {
                if (mtcod[i] != 0)
                {
                    stopitem.Code = mtcod[i];
                    stopitem.Count = mtcount[i] * make[slotcod].Count;
                    FindObjectOfType<DataSave>().ItemListAdd(stopitem);
                }
            }
        }
    }
}
