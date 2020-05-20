using ProjectTrader;
using System;
using System.Diagnostics;
using UnityEngine;

class GameDateTimeManager : MonoBehaviour
{
    // 초기화 및 디버그용 변수
    [Serializable]
    public struct GameDateTimeInitData
    {
        public int year;
        public int month;
        public int day;
        public int hour;
        public int minute;
        public int second;

        public static explicit operator GameDateTime(GameDateTimeInitData gameDateTimeInitData)
        {
            return new GameDateTime(
                gameDateTimeInitData.year,
                gameDateTimeInitData.month,
                gameDateTimeInitData.day,
                gameDateTimeInitData.hour,
                gameDateTimeInitData.minute,
                gameDateTimeInitData.second);
        }

        public static explicit operator GameDateTimeInitData(GameDateTime gameDateTime)
        {
            return new GameDateTimeInitData()
            {
                year = gameDateTime.Year,
                month = gameDateTime.Month,
                day = gameDateTime.Day,
                hour = gameDateTime.Hour,
                minute = gameDateTime.Minute,
                second = gameDateTime.Second
            };
        }
    }

    // 현실시간 5분 = 게임시간 1일
    // 현실시간 300초 = 게임시간 1440분 = 게임시간 86400초
    // 현실시간 1초 = 게임시간 288초

    [SerializeField]
    private GameDateTimeInitData data;
    /// <summary>
    /// 게임내 시간 비율. 1초 = gameTimeScale
    /// </summary>
    [SerializeField]
    private float gameTimeScale = 288f;
    [SerializeField]
    private bool isStopped = false;
    [SerializeField]
    private GameDateTime gameDateTime;
    [SerializeField]
    private float timePart = 0f;

    public bool IsStopped
    {
        get => isStopped;
        set => isStopped = value;
    }

    // 현재 게임내 시간을 가져옵니다.
    public GameDateTime GameDateTime
    {
        get => gameDateTime;
        set
        {
            gameDateTime = value;

#if UNITY_EDITOR
            // 초기화
            data = (GameDateTimeInitData)gameDateTime;
#endif
        }
    }

    /// <summary>
    /// 현재 시간의 소수점부를 가져옵니다.
    /// </summary>
    public float TimePart
    {
        get => timePart;
        set => timePart = value;
    }
    public void TimeStart()
    {
        isStopped = false;
    }

    public void TimeStop()
    {
        isStopped = true;
    }

    /// <summary>
    /// 시간을 초기화합니다.
    /// </summary>
    public void Reset()
    {
        gameDateTime = default;
        timePart = default;
        data = default;
        isStopped = false;
    }

    private void Start()
    {
        GameDateTime = (GameDateTime)data;
    }

    private void Update()
    {
        if (!IsStopped)
        {
            // 업데이트
            timePart += gameTimeScale * Time.deltaTime;

            int gameTimeInt = (int)timePart;
            // 소수점부 남기고 0으로 초기화
            timePart -= gameTimeInt;

            var gameDateTime = this.gameDateTime;
            gameDateTime.AddSecond(gameTimeInt);
            this.gameDateTime = gameDateTime;

#if UNITY_EDITOR
            // 출력
            data = (GameDateTimeInitData)gameDateTime;
#endif
        }
    }
}