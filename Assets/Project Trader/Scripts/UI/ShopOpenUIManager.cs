using UnityEngine;
using System.Collections;

public class ShopOpenUIManager : MonoBehaviour
{
    [SerializeField] GameDateTimeManager timeManager;
    [SerializeField] GameObject shopOpenButtonObject;

    // Use this for initialization
    void Start()
    {
        if (timeManager == null)
        {
            Debug.LogWarning("timeManager가 할당되지 않았습니다.");
            timeManager = FindObjectOfType<GameDateTimeManager>();
        }

        CheckTimerStoped();
    }

    void CheckTimerStoped()
    {
        // 시간이 정지되어있으면 버튼 숨기기
        shopOpenButtonObject.SetActive(timeManager.IsStopped);
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
