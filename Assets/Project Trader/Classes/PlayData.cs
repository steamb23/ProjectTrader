using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectTrader
{
    [Serializable]
    public class PlayData
    {
        #region 인스펙터 변수 & 초기 값
        [SerializeField]
        private int money = 10000;
        [SerializeField]
        private int level = 1;
        [SerializeField]
        private int maxFatigue;
        [SerializeField]
        private int fatigue;
        [SerializeField]
        private float awareness;
        [SerializeField]
        private string shopName;
        [SerializeField]
        private GameDateTime date;
        [SerializeField]
        private int shopSize;
        #endregion

        /// <summary>
        /// 현재 보유 금전
        /// </summary>
        public int Money
        {
            get => money;
            set => money = value;
        }

        /// <summary>
        /// 가게 등급
        /// </summary>
        public int Level
        {
            get => level;
            set => level = value;
        }

        /// <summary>
        /// 최대 피로도
        /// </summary>
        public int MaxFatigue
        {
            get => maxFatigue;
            set => maxFatigue = value;
        }

        /// <summary>
        /// 현재 피로도
        /// </summary>
        public int Fatigue
        {
            get => fatigue;
            set => fatigue = value;
        }

        /// <summary>
        /// 인지도
        /// </summary>
        public float Awareness
        {
            get => awareness;
            set => awareness = value;
        }

        /// <summary>
        /// 가게 이름
        /// </summary>
        public string ShopName
        {
            get => shopName;
            set => shopName = value;
        }

        /// <summary>
        /// 게임 시간
        /// </summary>
        public GameDateTime Date
        {
            get => date;
            set => date = value;
        }

        /// <summary>
        /// 가게 크기
        /// </summary>
        public int ShopSize
        {
            get => shopSize;
            set => shopSize = value;
        }

        //public PlayData()
        //{

        //}
    }
}