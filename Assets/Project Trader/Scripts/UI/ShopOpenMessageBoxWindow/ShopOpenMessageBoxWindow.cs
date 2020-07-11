using JetBrains.Annotations;
using ProjectTrader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOpenMessageBoxWindow : MonoBehaviour
{
    /// <summary>
    /// 윈도우를 표시합니다.
    /// </summary>
    /// <param name="isReview">심사인 경우 메시지와 동작을 다르게 표시합니다.</param>
    public void Show(bool isReview = false)
    {
        // 한달이 지났으면 = 오늘이 1일이면 (0부터 시작하기 때문에 1을 빼야함)
        // 휴식횟수 초기화
        if (PlayData.CurrentData.Date.Day == 1 - 1)
        {
            PlayData.CurrentData.RemainedRest = 10;
        }

        gameObject.SetActive(true);

        // 심사인 경우
        if (isReview)
        {
            Message = "구현안됨!";
        }
        else
        {
            Message = "오늘은 평일입니다.\n바로 오픈하시겠습니까?";
        }

        this.isReiew = isReview;
        UpdateData();

        // 열수 없으면 열기버튼 비활성화
        //if (openButton != null) openButton.interactable = PlayData.CurrentData?.Stamina >= ReductionRestPoint;
        //else Debug.LogError("openButton 필드가 할당되지 않았습니다.");
        // 남은 휴식횟수가 없으면 버튼 비활성화
        if (restButton != null) restButton.interactable = remainedRest > 0;
        else Debug.LogError("restButton 필드가 할당되지 않았습니다.");
    }

    [Tooltip("메시지")]
    [SerializeField] TMPro.TextMeshProUGUI messageText;
    [Tooltip("회복량")]
    [SerializeField] TMPro.TextMeshProUGUI regenerationRestPointText;
    [Tooltip("감소량")]
    [SerializeField] TMPro.TextMeshProUGUI reductionRestPointText;
    [Tooltip("남은 휴식")]
    [SerializeField] TMPro.TextMeshProUGUI remainedRestText;
    [SerializeField] Button openButton;
    [SerializeField] Button restButton;
    [SerializeField]
    GameObject nohpwindow;
    int regenerationRestPoint;
    int reductionRestPoint;
    int remainedRest;

    private bool isReiew;

    public string Message
    {
        get => messageText.text;
        set => messageText.text = value;
    }

    public int RegenerationRestPoint
    {
        get => regenerationRestPoint;
        set
        {
            regenerationRestPoint = value;

            regenerationRestPointText.text = $"{value}(+)";
        }
    }

    public int ReductionRestPoint
    {
        get => reductionRestPoint;
        set
        {
            reductionRestPoint = value;

            reductionRestPointText.text = $"{value}(-)";
        }
    }

    public int RemainedRest
    {
        get => remainedRest;
        set
        {
            remainedRest = value;

            remainedRestText.text = $"※{remainedRest}회 남음";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateData();
    }

    void UpdateData()
    {
        // 적절한 계산식에 따라 설정하도록 수정 예정
        RegenerationRestPoint = 20;
        ReductionRestPoint = 10;
        // TODO: GameDateManager에서 한달 주기로 갱신이 필요함.
        RemainedRest = PlayData.CurrentData.RemainedRest;
    }

    public void RestButtonClick()
    {
        // 휴식시 다음날로 일자 변경
        var gameDateTimeManager = FindObjectOfType<GameDateTimeManager>();

        var gameDateTime = gameDateTimeManager.GameDateTime;
        gameDateTime.AddDay(1);
        gameDateTimeManager.GameDateTime = gameDateTime;

        var playData = PlayData.CurrentData;
        if (playData != null)
        {
            playData.Stamina += RegenerationRestPoint;
            playData.RemainedRest -= 1;
        }

        // 아마 페이드인 아웃 묘사가 들어가야함

        // 창 숨기기
        gameObject.SetActive(false);
    }

    public void OpenButtonClick()
    {
        // 다음날 아침으로 일자 변경
        var gameDateTimeManager = FindObjectOfType<GameDateTimeManager>();
        var playData = PlayData.CurrentData;
        if (playData != null)
        {
            if ((playData.Stamina - ReductionRestPoint) >= 0)
            {
                playData.Stamina -= ReductionRestPoint;
                gameDateTimeManager.Opening();



                // 아마 페이드인 아웃 묘사가 들어가야함

                // 창 숨기기
                gameObject.SetActive(false);
            }
            else
            {
                nohpwindow.SetActive(true);
                gameObject.SetActive(false);
            }

        }

    }
}
