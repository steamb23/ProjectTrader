using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 퀘스트 윈도우 전체를 관리하는 비헤이비어입니다.
/// </summary>
public class QuestWindow : MonoBehaviour
{
    [SerializeField] GuideQuestPanel guideQuestPanel;
    [SerializeField] DailyQuestPanel dailyQuestPanel;
    [SerializeField] Button guideQuestButton;
    [SerializeField] Button dailyQuestButton;

    [SerializeField] Sprite clickedSprite;
    [SerializeField] Sprite releasedSprite;

    private void OnEnable()
    {
        // 기본적으로 가이드 퀘스트를 띄움.
        SwitchToGuideQuestPanel();
    }

    /// <summary>
    /// 가이드 퀘스트 패널로 전환
    /// </summary>
    public void SwitchToGuideQuestPanel()
    {
        guideQuestPanel.gameObject.SetActive(true);
        dailyQuestPanel.gameObject.SetActive(false);

        guideQuestButton.image.sprite = clickedSprite;
        dailyQuestButton.image.sprite = releasedSprite;
    }

    /// <summary>
    /// 데일리 퀘스트 패널로 전환
    /// </summary>
    public void SwitchToDailyQuestPanel()
    {
        guideQuestPanel.gameObject.SetActive(false);
        dailyQuestPanel.gameObject.SetActive(true);

        guideQuestButton.image.sprite = releasedSprite;
        dailyQuestButton.image.sprite = clickedSprite;
    }
}
