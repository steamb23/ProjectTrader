using ProjectTrader.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectTrader
{
    [Serializable]
    public class PlayData
    {
        static PlayData()
        {
            // 기본 데이터 설정.
            // 추후에 게임 진입시 초기화하도록 할 예정
            SetCurrentData(new PlayData());
        }

        public static PlayData CurrentData
        {
            get;
            private set;
        }

        public static void SetCurrentData(PlayData playData)
        {
            CurrentData = playData;
        }

        #region 인스펙터 변수 & 초기 값
        [SerializeField] int money = 10000;
        [SerializeField] int level = 1;
        [SerializeField] int maxStamina;
        [SerializeField] int stamina;
        [SerializeField] float awareness;
        [SerializeField] string shopName;
        [SerializeField] GameDateTime date = new GameDateTime(hour: 8); // 오픈 시간
        [SerializeField] int shopSize;
        [SerializeField] List<Item> ownedItems = new List<Item>();
        [SerializeField] Item[] displayedItems;
        [SerializeField] List<Employee> hiredEmployees;
        [SerializeField] Employee[] cashers;
        [SerializeField] Employee[] cleaners;
        [SerializeField] Employee[] crafter;
        [SerializeField] int remainedRest = 10; // 기본값은 0
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
        public int MaxStamina
        {
            get => maxStamina;
            set => maxStamina = value;
        }

        /// <summary>
        /// 현재 피로도
        /// </summary>
        public int Stamina
        {
            get => stamina;
            set => stamina = value;
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
        /// <summary>
        /// 소유중인 아이템<br/>
        /// 데이터가 중복되면 중복되는 데이터의 카운트값을 올려야합니다.
        /// </summary>
        public List<Item> OwnedItems
        {
            get => this.ownedItems;
            set => this.ownedItems = value;
        }
        /// <summary>
        /// 진열된 아이템
        /// </summary>
        public Item[] DisplayedItems
        {
            get => this.displayedItems;
            set => this.displayedItems = value;
        }
        /// <summary>
        /// 고용된 직원들<br/>
        /// 고용 윈도우에서 고용가능한 직원을 표시할 때, 여기에 포함되어있는 직원은 제외해야합니다.
        /// </summary>
        public List<Employee> HiredEmployees
        {
            get => this.hiredEmployees;
            set => this.hiredEmployees = value;
        }
        /// <summary>
        /// 고용된 직원 중 계산원 업무가 할당된 직원들
        /// </summary>
        public Employee[] Cashers
        {
            get => this.cashers;
            set => this.cashers = value;
        }
        /// <summary>
        /// 고용된 직원 중 청소부 업무가 할당된 직원들
        /// </summary>
        public Employee[] Cleaners
        {
            get => this.cleaners;
            set => this.cleaners = value;
        }
        /// <summary>
        /// 고용된 직원 중 제작 업무가 할당된 직원들
        /// </summary>
        public Employee[] Crafter
        {
            get => this.crafter;
            set => this.crafter = value;
        }

        /// <summary>
        /// 남은 휴식
        /// </summary>
        public int RemainedRest
        {
            get => this.remainedRest;
            set => this.remainedRest = value;
        }

        //public PlayData()
        //{

        //}
    }
}