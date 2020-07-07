using UnityEngine;
using System.Collections;
using ProjectTrader.Datas;
using ProjectTrader;
using UnityEngine.UI;
using System.Collections.Generic;

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
        //InitializeGuideQuestCards();
    }

    private void OnEnable()
    {
        WindowOpened();
    }

    /// <summary>
    /// 윈도우를 열었을때의 작업입니다.
    /// </summary>
    public void WindowOpened()
    {
        // 카드 초기화후 다시 생성
        ClearGuideQuestCards();
        InitializeGuideQuestCards();

        // 스크롤 뷰 위치 초기화
        ScrollToCurrentActiveCard();
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
        guideQuestCard.transform.localScale = Vector3.one; // 720p외의 환경에서 스케일 문제 발생 수정
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

    /// <summary>
    /// 현재 활성화된 카드로 스크롤합니다.
    /// </summary>
    public void ScrollToCurrentActiveCard()
    {
        GuideQuestCard activeCard = null;
        
        // 현재 활성화된 퀘스트 카드 검색
        foreach (var card in guideQuestCards)
        {
            if (card.IsInteractable)
            {
                activeCard = card;
                break;
            }
        }

        StartCoroutine(NextFrame());
        IEnumerator NextFrame()
        {
            yield return null;
            ScrollToCard(activeCard);
        }
    }

    /// <summary>
    /// 해당 카드의 위치로 스크롤합니다. 가능하면 패널에서 관리되는 카드가 확실할 경우에만 호출하세요.
    /// </summary>
    /// <param name="card">패널에서 관리되는 퀘스트 카드 컴포넌트</param>
    public void ScrollToCard(GuideQuestCard card)
    {
        var scrollRect = GetComponent<ScrollRect>();

        if (card != null)
        {
            var cardRectTransform = card.GetComponent<RectTransform>();
            // 활성화된 카드의 로컬 위치 알아내기
            var horizontalActiveCardLocalPosition = cardRectTransform.offsetMin.x;

            // Rect 마스크와 컨텐츠의 넓이 가져오기 (Scale이 항상 1이라는 가정하에)
            var rectMaskWidth = GetComponentInChildren<RectMask2D>().rectTransform.rect.width;
            var contentsWidth = scrollRect.content.rect.width;

            // 오른쪽에서 움직임이 제한되는 위치
            var horizontalMaxPosition = Mathf.Max(contentsWidth - rectMaskWidth, 0);
            //카드의 노멀라이즈된 위치 알아내기
            var horizontalNormalizedPosition = horizontalMaxPosition != 0 ?
                horizontalActiveCardLocalPosition / horizontalMaxPosition :
                0f;

            // 스크롤 렉트 설정
            scrollRect.horizontalNormalizedPosition = Mathf.Min(horizontalNormalizedPosition, 1f);
        }
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
