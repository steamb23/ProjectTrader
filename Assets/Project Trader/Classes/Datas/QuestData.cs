using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.Datas
{
    [Serializable]
    public class QuestData
    {
        public enum RewardType
        {
            /// <summary>
            /// 골드, <see cref="RewardAmount"/>에 영향 받음.
            /// </summary>
            Gold,
            /// <summary>
            /// 아이템, <see cref="RewardAmount"/>에 영향 받지 않음.
            /// </summary>
            Item,
            /// <summary>
            /// 등급, <see cref="RewardAmount"/>에 영향 받지 않음.
            /// </summary>
            Level
        }

        public enum GoalType
        {
            /// <summary>
            /// 직원 고용
            /// </summary>
            HireEmployee,
            /// <summary>
            /// 아이템 배치
            /// </summary>
            SetItem,
            /// <summary>
            /// 아이탬 배치 변경
            /// </summary>
            ChangeItem,
            /// <summary>
            /// 아이탬 판매
            /// </summary>
            SellItem,
            /// <summary>
            /// 하루 방문 손님
            /// </summary>
            VisitorCount,
            /// <summary>
            /// 등급 심사
            /// </summary>
            ReviewRating,
            /// <summary>
            /// 직접 아이템 판매
            /// </summary>
            SelfDeal,
            /// <summary>
            /// 직접 청소
            /// </summary>
            SelfCleaning,
            /// <summary>
            /// 아이템 제작
            /// </summary>
            CraftingItem,
            /// <summary>
            /// 아이템 구매
            /// </summary>
            BuyItem,
        }

        public int Code
        {
            get => this.code;
            set => this.code = value;
        }

        public int Level
        {
            get => this.level;
            set => this.level = value;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }
        public string Description
        {
            get => this.description;
            set => this.description = value;
        }
        public string Summary
        {
            get => this.summary;
            set => this.summary = value;
        }
        public RewardType RewardTypeData
        {
            get => this.rewardTypeData;
            set => this.rewardTypeData = value;
        }
        public int RewardAmount
        {
            get => this.rewardAmount;
            set => this.rewardAmount = value;
        }
        public int RewardItemCode
        {
            get => this.rewardItemCode;
            set => this.rewardItemCode = value;
        }
        public GoalType GoalTypeData
        {
            get => this.goalTypeData;
            set => this.goalTypeData = value;
        }
        public int GoalAmount
        {
            get => this.goalAmount;
            set => this.goalAmount = value;
        }

        [SerializeField] int code;
        [SerializeField] int level;
        // 퀘스트 이름
        [SerializeField] string name;
        // 내용(설명)
        [SerializeField] string description;
        // 요약
        [SerializeField] string summary;
        // 목표 타입
        [SerializeField] GoalType goalTypeData;
        // 목표량
        [SerializeField] int goalAmount;
        // 보상 타입
        [SerializeField] RewardType rewardTypeData;
        // 보상량
        [SerializeField] int rewardAmount;
        // 아이템코드, IngameDatabase에 접근하여 아이템 데이터 가져오면됨.
        [SerializeField] int rewardItemCode;
    }
}
