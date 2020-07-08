using UnityEngine;
using UnityEngine.UI;
using System;
using ProjectTrader.Datas;
using TMPro;
using ProjectTrader;

// 원래는 GuideQuest와 다형성 구현해야함...
// 멍청하게도 둘의 구조가 비슷할거라는걸 생각 못함...
// 나중에 가이드 퀘스트, 일일 퀘스트 외에 추가적인 퀘스트 컨텐츠 추가가 있다면 그때 해당작업을 해야할 수 있음.
public class DailyQuestCard : MonoBehaviour
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
    //[SerializeField] DailyQuestPanel dailyQuestPanel;
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
        Initialize();
        UpdateData();
        //descriptionText.text = questState.GetQuestData().Summary;
    }

    public void Initialize()
    {
        //if (dailyQuestPanel == null)
        //    dailyQuestPanel = GameObject.FindObjectOfType<DailyQuestPanel>();

        images = GetComponentsInChildren<Image>(true);

        // UI컴포넌트가 설정이 됬는지 체크
        if (rewardButtonText == null ||
        progressBarImage == null)
        {
            ThrowSetUIComponentException();
        }

        void ThrowSetUIComponentException()
        {
            throw new Exception("DailyQuestCard에서 하나 이상의 UI 컴포넌트를 설정 하지 않았습니다. 디자인을 변경했다면 코드도 변경이 필요할 수 있습니다.");
        }
    }

    private void OnEnable()
    {
        UpdateData();
        //if (dailyQuestPanel != null)
        //    Check();
    }

    /// <summary>
    /// 보상 받기 터치
    /// </summary>
    public void TouchRewardButton()
    {
        if (!questState.IsRewarded)
        {
            questState.Reward();

            // 갱신
            UpdateData();
        }
    }

    /// <summary>
    /// 데이터 갱신
    /// </summary>
    public void UpdateData()
    {
        if (PlayData.CurrentData.DailyQuestStates.Count > 0)
        {
            questState = PlayData.CurrentData.DailyQuestStates[0];

            descriptionText.text = questState.GetQuestData().Summary;
        }
        else
        {
            questState = null;
        }

        Check();
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
            descriptionText.text = "퀘스트 없음";
            IsInteractable = false;
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

        IsInteractable = !IsRewarded;
    }
}