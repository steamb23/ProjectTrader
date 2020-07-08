using ProjectTrader.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader
{
    /// <summary>
    /// 퀘스트 관련 메소드를 모아둔 정적 클래스입니다.
    /// </summary>
    public static class QuestManager
    {

        /// <summary>
        /// 퀘스트 트리거, 모든 퀘스트 상태 데이터의 <see cref="QuestData.goalAmount"/>에 매개변수 <paramref name="amount"/>의 값을 추가합니다.
        /// </summary>
        public static void Trigger(QuestData.GoalType goalType, int amount = 1) =>
            AddAmount(amount, (data) => data.GetQuestData().GoalTypeData == goalType);


        static void AddAmount(int amount, Predicate<QuestState> match)
        {
            // 가이드 퀘스트
            var guideQuests = PlayData.CurrentData.GuideQuestStates.FindAll(match);
            if (guideQuests != null)
            {
                foreach (var guideQuest in guideQuests)
                {
                    guideQuest.CurrentAmount += amount;
                }
            }

            // 일일 퀘스트
            var dailyQuest = PlayData.CurrentData.DailyQuestStates.Find(match);
            if (dailyQuest != null)
            {
                dailyQuest.CurrentAmount += amount;
            }
        }
    }
}
