using UnityEngine;
using System.Collections;
using Boo.Lang;
using ProjectTrader.Datas;
using ProjectTrader;

public class GuideQuestPanel : MonoBehaviour
{
    public List<GuideQuestCard> GuideQuestCards
    {
        get => this.guideQuestCards;
        set => this.guideQuestCards = value;
    }

    [Header("프리팹")]
    [SerializeField] GameObject guideQuestCardPrefab;
    [SerializeField] Transform componentTransform;

    [SerializeField] List<GuideQuestCard> guideQuestCards = new List<GuideQuestCard>();

    private void Start()
    {
        InitializeGuideQuestCards();
    }

    /// <summary>
    /// 윈도우를 열었을때의 작업입니다.
    /// </summary>
    public void WindowOpened()
    {
        ClearGuideQuestCards();

    }

    /// <summary>
    /// 가이드 퀘스트 카드의 인스턴스를 생성합니다.
    /// </summary>
    /// <param name="questState"></param>
    /// <returns></returns>
    public GuideQuestCard CreateGuideQuestCard(QuestState questState)
    {
        var gameObject = Instantiate(guideQuestCardPrefab);

        var guideQuestCard = gameObject.GetComponent<GuideQuestCard>();
        guideQuestCard.Initialize();

        guideQuestCard.QuestState = questState;
        guideQuestCard.transform.SetParent(componentTransform);
        // 목록에 추가
        guideQuestCards.Add(guideQuestCard);

        // 데이터 체크
        guideQuestCard.Check();

        return guideQuestCard;
    }

    public void InitializeGuideQuestCards()
    {
        // 퀘스트 카드들 초기화
        foreach (var questState in PlayData.CurrentData.GuideQuestStates)
        {
            // 생성
            CreateGuideQuestCard(questState);
        }
    }

    public void ClearGuideQuestCards()
    {
        foreach (var card in guideQuestCards)
        {
            if (card != null)
            {
                // 퀘스트 카드의 오브젝트 파괴
                Destroy(card.gameObject);
            }
        }

        // 리스트 클리어
        guideQuestCards.Clear();
    }

    // 필요 없을듯... 나중에 필요하면 다시 활성화
    ///// <summary>
    ///// <see cref="guideQuestCards"/>에서 null, 파괴된 유니티 오브젝트를 모두 제거합니다.
    ///// </summary>
    //public void CleanGuideQuestCards()
    //{
    //    guideQuestCards.RemoveAll((guideQuestCard) => guideQuestCard == null);
    //}
}
