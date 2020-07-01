using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.Datas
{
    public class QuestData
    {
        public enum RewardTypeData
        {
            Item,
            Gold,
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
        public RewardTypeData RewardType
        {
            get => this.rewardType;
            set => this.rewardType = value;
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

        // 퀘스트 이름(내부용)
        [SerializeField] string name;
        // 내용(설명)
        [SerializeField] string description;
        // 요약
        [SerializeField] string summary;
        // 보상 타입
        [SerializeField] RewardTypeData rewardType;
        // 보상량
        [SerializeField] int rewardAmount;
        // 아이템코드, IngameDatabase에 접근하여 아이템 데이터 가져오면됨.
        [SerializeField] int rewardItemCode;
    }
}
