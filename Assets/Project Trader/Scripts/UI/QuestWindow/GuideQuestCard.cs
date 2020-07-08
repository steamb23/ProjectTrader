using UnityEngine;
using UnityEngine.UI;
using System;
using ProjectTrader.Datas;
using TMPro;

public class GuideQuestCard : MonoBehaviour
{

    /// <summary>
    /// false면 밝게, true면 어둡게 처리
    /// </summary>
    public bool IsRewarded
    {
        get => isRewarded;
        set
        {
            this.isRewarded = value;
            //CheckInteractable();
        }
    }

    public bool IsInteractable
    {
        get => isInteractable;
        set
        {
            isInteractable = value;
            rewardButton.interactable = isInteractable && isGoal;
            if (isInteractable)
            {
                foreach (var image in images)
                {
                    image.color = Color.white;
                }
            }
            else
            {
                foreach (var image in images)
                {
                    image.color = Color.gray;
                }
            }
        }
    }

    public QuestState QuestState
    {
        get => this.questState;
        set
        {
            this.questState = value;

            //CheckQuestState();
        }
    }

    public float Progress
    {
        get => this.progress;
        set
        {
            this.progress = value;

            progress = Mathf.Clamp01(progress);
            var maxWidth = ProgressBarMaxWidth;
            var width = progress * maxWidth;

            var sizeDelta = progressBarImage.rectTransform.sizeDelta;
            sizeDelta.x = width;
            progressBarImage.rectTransform.sizeDelta = sizeDelta;
        }
    }

    [Tooltip("퀘스트 상태")]
    [SerializeField] QuestState questState;
    [Header("UI 컴포넌트")]
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Button rewardButton;
    [SerializeField] TextMeshProUGUI rewardButtonText;
    [SerializeField] Image progressBarImage;

    [Header("디버그")]
    [SerializeField] GuideQuestPanel guideQuestPanel;
    [SerializeField] Image[] images;
    [SerializeField] bool isRewarded;
    [SerializeField] bool isInteractable;
    [SerializeField] bool isGoal;
    [Range(0, 1)]
    [SerializeField] float progress;

    float progressBarMaxWidth;
    bool isProgressBarMaxWidthCached;
    float ProgressBarMaxWidth
    {
        get
        {
            if (!isProgressBarMaxWidthCached)
            {
                progressBarMaxWidth = progressBarImage.rectTransform.rect.width;
                isProgressBarMaxWidthCached = true;
            }
            return progressBarMaxWidth;
        }
    }



    // Use this for initialization
    void Start()
    {

        descriptionText.text = questState.GetQuestData().Summary;
    }

    public void Initialize()
    {
        if (guideQuestPanel == null)
            guideQuestPanel = GameObject.FindObjectOfType<GuideQuestPanel>();

        images = GetComponentsInChildren<Image>(true);

        // UI컴포넌트가 설정이 됬는지 체크
        if (rewardButtonText == null ||
        progressBarImage == null)
        {
            ThrowSetUIComponentException();
        }

        void ThrowSetUIComponentException()
        {
            throw new Exception("GuideQuestCard에서 하나 이상의 UI 컴포넌트를 설정 하지 않았습니다. 디자인을 변경했다면 코드도 변경이 필요할 수 있습니다.");
        }
    }

    private void OnEnable()
    {
        if (guideQuestPanel != null)
            Check();
    }

    /// <summary>
    /// 보상 받기 터치
    /// </summary>
    public void TouchRewardButton()
    {
        if (!questState.IsRewarded)
        {
            questState.Reward();

            // 패널 갱신
            guideQuestPanel.ClearGuideQuestCards();
            guideQuestPanel.InitializeGuideQuestCards();
        }
    }

    /// <summary>
    /// 퀘스트 데이터를 체크하여 UI에 반영합니다.
    /// </summary>
    public void Check()
    {
        if (questState != null)
        {
            // 퀘스트 스테이트에서 IsRewarded 복사
            IsRewarded = questState.IsRewarded;

            CheckProgress();
            CheckInteractable();
        }
        else
        {
            throw new Exception("questState가 null입니다.");
        }
    }

    // 현재 진행상황을 체크합니다.
    void CheckProgress()
    {
        var goalAmount = questState.GetQuestData().GoalAmount;
        var currentAmount = questState.CurrentAmount;

        var progress = currentAmount / (float)goalAmount;
        if (float.IsNaN(progress))
            progress = 1;
        Progress = progress;

        if (1 - progress < 1e-6f) // (progress >= 1)
        {
            rewardButtonText.text = "받기";
            // isRewarded가 false일때만 상호작용 가능
            //rewardButton.interactable = !IsInteractable;
            isGoal = true;
        }
        else
        {
            rewardButtonText.text = "미달성";
            //rewardButton.interactable = false;
            isGoal = false;
        }
    }

    void CheckInteractable()
    {
        // 이전 퀘스트가 완료되었는지
        bool prevQuestIsRewarded = false;
        // 현재 인덱스 확인
        var currentCardsIndex = guideQuestPanel.GuideQuestCards.IndexOf(this);
        if (currentCardsIndex >= 0)
        {
            if (currentCardsIndex == 0 ||
                guideQuestPanel.GuideQuestCards[currentCardsIndex - 1].isRewarded)
            {
                prevQuestIsRewarded = true;
            }
        }

        IsInteractable = !IsRewarded && prevQuestIsRewarded;
    }
}
