using UnityEngine;
using System.Collections;

public class ShopOpenUIManager : MonoBehaviour
{
    [SerializeField] GameDateTimeManager timeManager;
    [SerializeField] VisitorManager visitorManager;
    [SerializeField] GameObject shopOpenButtonObject;

    // Use this for initialization
    void Start()
    {
        if (timeManager == null)
        {
            Debug.LogWarning("성능 경고: timeManager 필드가 자동으로 할당되었습니다.");
            timeManager = FindObjectOfType<GameDateTimeManager>();
        }
        if (visitorManager == null)
        {
            Debug.LogWarning("성능 경고: vistorManager 필드가 자동으로 할당되었습니다.");
            visitorManager = FindObjectOfType<VisitorManager>();
        }

        CheckTimerStoped();
    }

    void CheckTimerStoped()
    {
        if (visitorManager.visitors.Count <= 0 && timeManager.IsStopped)
            shopOpenButtonObject.SetActive(true);
        else
            // 시간이 정지되어있거나 손님이 남아 있으면 버튼 숨기기
            shopOpenButtonObject.SetActive(false);
    }

    public void ShopOpenButtonClick()
    {
        timeManager.Opening();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimerStoped();
    }
}
