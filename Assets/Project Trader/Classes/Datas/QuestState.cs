using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.Datas
{
    /// <summary>
    /// 퀘스트의 진행상태를 체크하기 위한 데이터입니다.
    /// </summary>
    [Serializable]
    public class QuestState
    {
        public enum QuestType
        {
            Guide,
            Daily
        }

        /// <summary>
        /// 퀘스트 코드
        /// </summary>
        public int Code
        {
            get => this.code;
            set => this.code = value;
        }
        /// <summary>
        /// 현재 달성량
        /// </summary>
        public int CurrentAmount
        {
            get => this.currentAmount;
            set => this.currentAmount = value;
        }
        /// <summary>
        /// 보상을 수령했는지 여부
        /// </summary>
        public bool IsRewarded
        {
            get => this.isRewarded;
            set => this.isRewarded = value;
        }

        public QuestType QuestTypeData
        {
            get => this.questTypeData;
            set => this.questTypeData = value;
        }

        /// <summary>
        /// 퀘스트 데이터의 인스턴스를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public QuestData GetQuestData() => GetQuestData(QuestTypeData);

        /// <summary>
        /// 퀘스트 데이터의 인스턴스를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public QuestData GetQuestData(QuestType questType)
        {
            switch (questType)
            {
                default:
                case QuestType.Guide:
                    return QuestDatabase.GuideQuestDatas[code];
                case QuestType.Daily:
                    return QuestDatabase.DailyQuestDatas[code];
            }
        }

        /// <summary>
        /// 보상을 지급합니다. 보상여부는 체크하지 않으니 호출전에 체크 필요!
        /// </summary>
        public void Reward()
        {
            var questData = GetQuestData();

            switch (questData.RewardTypeData)
            {
                case QuestData.RewardType.Gold:
                    PlayData.CurrentData.Money += questData.RewardAmount;
                    break;
            }

            isRewarded = true;
        }

        [SerializeField] int code;
        [SerializeField] int currentAmount;
        [SerializeField] bool isRewarded;
        [SerializeField] QuestType questTypeData;
    }
}
