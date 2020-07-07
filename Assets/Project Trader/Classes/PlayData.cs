using ProjectTrader.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
            CurrentData.isInitialized = true;
        }

        public static PlayData CurrentData
        {
            get;
            set;
        }

        public static void SetCurrentData(PlayData playData)
        {
            CurrentData = playData;
        }

        #region 인스펙터 변수 & 초기 값
        [SerializeField] int money = 10000;
        [SerializeField] int level = 1;
        [SerializeField] int maxStamina = 200;
        [SerializeField] int stamina = 100;
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
        // 가이드 퀘스트 달성 목록
        [SerializeField] List<QuestState> guideQuestStates = new List<QuestState>();
        // 일일 퀘스트 달성 목록
        [SerializeField] List<QuestState> dailyQuestStates = new List<QuestState>();
        // 일일 퀘스트 갱신 시간
        [SerializeField] GameDateTime recentDailyQuestUpdateDate;
        [SerializeField] bool isInitialized = false;
        #endregion

        /// <summary>
        /// 모든 데이터가 데이터베이스와 동기화되있는 상태인지 체크
        /// </summary>
        bool isSynced;

        public bool IsInitialized => isInitialized;

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
            set
            {
                stamina = value;
                // 최댓값으로 고정
                if (MaxStamina < value)
                    stamina = MaxStamina;
            }
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

        /// <summary>
        /// 가이드 퀘스트 상태
        /// </summary>
        public List<QuestState> GuideQuestStates
        {
            get => this.guideQuestStates;
            set
            {
                if (!isSynced) SyncData();
                this.guideQuestStates = value;
            }
        }

        /// <summary>
        /// 일일 퀘스트 진행 상태
        /// </summary>
        public List<QuestState> DailyQuestStates
        {
            get => this.dailyQuestStates;
            set => this.dailyQuestStates = value;
        }

        /// <summary>
        /// 최근 일일 퀘스트 갱신 일자
        /// </summary>
        public GameDateTime RecentDailyQuestUpdateDate
        {
            get => this.recentDailyQuestUpdateDate;
            set => this.recentDailyQuestUpdateDate = value;
        }

        /// <summary>
        /// 게임 데이터 베이스에 동기화
        /// 
        /// TODO:플레이 데이터 초기화 혹은 로딩 과정에서 호출해줄 필요가 있음.
        /// </summary>
        public void SyncData()
        {
            SyncQuestStateFromQuestData();
            isSynced = true;
        }

        /// <summary>
        /// 퀘스트 데이터에 맞게 퀘스트 스테이트 목록을 동기화합니다.
        /// </summary>
        void SyncQuestStateFromQuestData()
        {
            var workQueue = new Queue<QuestState>();

            foreach (var questData in QuestDatabase.GuideQuestDatas)
            {
                var questState = GuideQuestStates.Find((qs) => qs.Code == questData.Code);

                // 데이터가 없으면 추가
                if (questState == null)
                {
                    questState = new QuestState()
                    {
                        Code = questData.Code,
                        QuestTypeData = QuestState.QuestType.Guide
                    };
                    GuideQuestStates.Add(questState);
                }
                workQueue.Enqueue(questState);
            }

            // 내용 복사 (이미 정렬되있는 것으로 간주함)
            GuideQuestStates.Clear();
            foreach (var questState in workQueue)
            {
                GuideQuestStates.Add(questState);
            }
        }

        /// <summary>
        /// 일일 퀘스트를 갱신합니다.
        /// </summary>
        public void UpdateDailyQuest()
        {
            DailyQuestStates.Clear();

            // 가져올 퀘스트 코드
            int index = UnityEngine.Random.Range(0, QuestDatabase.DailyQuestDatas.Count);

            DailyQuestStates.Add(new QuestState()
            {
                QuestTypeData = QuestState.QuestType.Daily,
                Code = QuestDatabase.DailyQuestDatas[index].Code
            });
        }

        ///// <summary>
        ///// 퀘스트 갱신
        ///// </summary>
        //public void UpdateDailyQuest()
        //{
        //    // 퀘스트 데이터 초기화
        //    DailyQuestDatas.Clear();
        //    dailyQuestStates.Clear();

        //    // 인게임 데이터에서 일일 퀘스트 데이터 가져와서 
        //}

        ///// <summary>
        ///// 데이터 무결성 검사 등등...
        ///// </summary>
        //public void CheckData()
        //{
        //    CheckDailyQuest();
        //}



        ///// <summary>
        ///// 일일 퀘스트 체크
        ///// </summary>
        //private void CheckDailyQuest()
        //{
        //    var now = DateTime.Now;
        //    // 최근 갱신 일자에서 하루 이상 지났으면
        //    if (now.Day - RecentDailyQuestUpdateDate.Day >= 1)
        //    {
        //        // 퀘스트 갱신
        //        UpdateDailyQuest();
        //    }
        //}
    }
}