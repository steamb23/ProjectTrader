using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader.Datas
{
    public static class QuestDatabase
    {
        public static IReadOnlyList<QuestData> GuideQuestDatas { get; } = new QuestData[]
        {
            new QuestData()
            {
                Code = 0,
                Name = "알바 고용하기 1",
                Description  = "가게 일을 도와줄 알바를 1명 고용하자",
                Summary = "알바 1명 고용",
                GoalTypeData = QuestData.GoalType.HireEmployee,
                GoalAmount = 1,
                RewardTypeData = QuestData.RewardType.Gold,
                RewardAmount = 100
            }
        };
        public static IReadOnlyList<QuestData> DailyQuestDatas { get; } = new QuestData[]
        {
            new QuestData()
            {
                Code = 0,
                Level = 1,
                Name = "판매신 강림 1",
                Description="아이템 판매 10개 이상 하기",
                Summary = "아이템 10개 이상 판매",
                GoalTypeData = QuestData.GoalType.SellItem,
                GoalAmount = 10,
                RewardTypeData = QuestData.RewardType.Gold,
                RewardAmount = 100
            }
        };
    }
}
