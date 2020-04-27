using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    //게임일자
    DateTime gameDay = new DateTime(2020, 01, 01);
    //차 계산시 기준점
    DateTime game;
    // 현재시간
    DateTime day;
    //시간계산용
    TimeSpan g_Time;
    //게임내 하루 분
    public int day_Time = 1;
    //하루가 시작됐는가?
    bool startDay = false;

    int countDay;
    GameObject day_print;

    void Start()
    {
        day_print = GameObject.Find("UIControl");
        if (day_print == null)
            Debug.Log("day_print is null");
        countDay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DayUpdate();
    }
    void DayUpdate()
    {
        if (startDay == true)
        {
            day = DateTime.Now;
            g_Time = day - game;

            //두 시간 차(현재시간-기준시간)가 하루보다 높아지면 하루끝
            if (g_Time.TotalMinutes >= day_Time)
            {
                EndDay();
            }
        }
    }

    void StartDay()
    {
        Debug.Log("Day start");
        countDay++;
        //하루가 시작되었으니 날짜를 증가시키고 기준시간 정비
        day_print.GetComponent<TextUiControl>().CreativeTextBox(0, 150, 200, gameDay.ToString("MM - dd  ") + countDay + ("Day"), 15);
        gameDay = gameDay.AddDays(1);
        game = DateTime.Now;
        startDay = true;
    }

    //하루가 끝나고 정산 전 부르는 함수
    void EndDay()
    {
        Debug.Log("Day end");
        startDay = false;
    }
}
