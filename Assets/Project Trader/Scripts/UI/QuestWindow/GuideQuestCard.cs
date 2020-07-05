using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Assertions;
using ProjectTrader.Datas;
using Unity.Collections;
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
            rewardButton.interactable = !value;
            if (isRewarded)
            {
                foreach (var image in images)
                {
                    image.color = Color.gray;
                }
            }
            else
            {
                foreach (var image in images)
                {
                    image.color = Color.white;
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

            CheckQuestState();
        }
    }

    public float Progress
    {
        get => this.progress;
        set
        {
            this.progress = value;

            progress = Mathf.Clamp01(progress);
            var maxWidth = progressBarMaxWidth;
            var width = progress * maxWidth;

            var sizeDelta = progressBarImage.rectTransform.sizeDelta;
            sizeDelta.x = width;
            progressBarImage.rectTransform.sizeDelta = sizeDelta;
        }
    }

    [Tooltip("퀘스트 상태")]
    [SerializeField] QuestState questState;
    [Header("UI 컴포넌트")]
    [SerializeField] Button rewardButton;
    [SerializeField] TextMeshProUGUI rewardButtonText;
    [SerializeField] Image progressBarImage;

    [Header("디버그")]
    [SerializeField] Image[] images;
    [SerializeField] bool isRewarded;
    [Range(0, 1)]
    [SerializeField] float progress;

    float progressBarMaxWidth;



    // Use this for initialization
    void Start()
    {
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


        progressBarMaxWidth = progressBarImage.rectTransform.sizeDelta.x;
    }

    /// <summary>
    /// 퀘스트 데이터를 체크하여 UI에 반영합니다.
    /// </summary>
    public void CheckQuestState()
    {
        if (questState != null)
        {
            // 퀘스트 스테이트에서 IsRewarded 복사
            IsRewarded = questState.IsRewarded;

            CheckProgress();
        }
        else
        {
            throw new Exception("questState가 null입니다.");
        }

        // 현재 진행상황을 체크합니다.
        void CheckProgress()
        {
            var goalAmount = questState.GetQuestData().GoalAmount;
            var currentAmount = questState.CurrentAmount;

            var progress = currentAmount / (float)goalAmount;
            Progress = progress;

            if (progress - 1 < 1e-6f) // (progress >= 1)
            {
                rewardButtonText.text = "받기";
                // isRewarded가 false일때만 상호작용 가능
                rewardButton.interactable = !isRewarded;
            }
            else
            {
                rewardButtonText.text = "미달성";
                rewardButton.interactable = false;
            }
        }
    }
}
