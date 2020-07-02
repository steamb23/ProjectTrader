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

        public QuestData GetQuestData() => GetQuestData(QuestTypeData);

        public QuestData GetQuestData(QuestType questType)
        {
            switch (questType)
            {
                default:
                case QuestType.Guide:
                    return QuestDatabase.GuideQuestDatas[code];
                case QuestType.Daily:
                    return QuestDatabase.GuideQuestDatas[code];
            }
        }

        [SerializeField]
        int code;
        [SerializeField]
        bool isRewarded;
        [SerializeField]
        QuestType questTypeData;
    }
}
