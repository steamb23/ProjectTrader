using ProjectTrader;
using System;
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
    public float timeScale = 288;

    public GameDateTimeInitData data;

    private GameDateTime gameDateTime;

    // 현재 게임내 시간을 가져옵니다.
    public GameDateTime GameDateTime
    {
        get => gameDateTime;
        set
        {
            gameDateTime = value;

            // 초기화
            gameTime = default;
            data = default;
        }
    }

    //private float time = 0;
    private float gameTime = 0;
    private void Start()
    {
        GameDateTime = (GameDateTime)data;
    }

    private void Update()
    {
        // 업데이트
        gameTime += timeScale * Time.deltaTime;

        int gameTimeInt = (int)gameTime;
        // 소수점부 남기고 0으로 초기화
        gameTime -= gameTimeInt;

        var gameDateTime = GameDateTime;
        gameDateTime.AddSecond(gameTimeInt);
        GameDateTime = gameDateTime;
        //if (time > 1)
        //{
        //    GameDateTime.AddSecond();
        //    time -= 1;
        //}

#if UNITY_EDITOR
        // 출력
        data = (GameDateTimeInitData)GameDateTime;
#endif
    }
}