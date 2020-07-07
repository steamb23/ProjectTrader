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
        /// 직원 고용 트리거
        /// </summary>
        public static void TriggerHireEmployee(int amount = 1) =>
            AddAmount(amount, (data) => data.GetQuestData().GoalTypeData == QuestData.GoalType.HireEmployee);


        static void AddAmount(int amount, Predicate<QuestState> match)
        {
            // TODO:일일 퀘스트는 해당 데이터 구조 수정하는대로 추가
            var guideQuests = PlayData.CurrentData.GuideQuestStates.Find(match);
            guideQuests.CurrentAmount += amount;
        }
    }
}
