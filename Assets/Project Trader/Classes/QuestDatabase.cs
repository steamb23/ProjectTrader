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
            },
            new QuestData()
            {
                Code = 1,
                Name = "아이템 배치 1",
                Description  = "아이템을 다른 아이템 3개 배치해보자",
                Summary = "다른 종류 아이템 3개 배치",
                GoalTypeData = QuestData.GoalType.SetItem,
                GoalAmount = 3,
                RewardTypeData = QuestData.RewardType.Gold,
                RewardAmount = 100
            },
            new QuestData()
            {
                Code = 2,
                Name = "재료 구입 1",
                Description  = "모험가 길드에서 무화초를 10개 구입하자",
                Summary = "무화초 10개 구입",
                GoalTypeData = QuestData.GoalType.BuyItem,
                GoalAmount = 3,
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
