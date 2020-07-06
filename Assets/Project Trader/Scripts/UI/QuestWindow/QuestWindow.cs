using UnityEngine;
using System.Collections;

/// <summary>
/// 퀘스트 윈도우 전체를 관리하는 비헤이비어입니다.
/// </summary>
public class QuestWindow : MonoBehaviour
{
    [SerializeField] GuideQuestPanel guideQuestPanel;
    [SerializeField] DailyQuestPanel dailyQuestPanel;

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
    }

    /// <summary>
    /// 데일리 퀘스트 패널로 전환
    /// </summary>
    public void SwitchToDailyQuestPanel()
    {
        guideQuestPanel.gameObject.SetActive(false);
        dailyQuestPanel.gameObject.SetActive(true);
    }
}
