using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;

public class ReviewResultWindow : MonoBehaviour
{

    [Tooltip("심사 성공시의 결과 연출")]
    [SerializeField] GameObject successReviewResultVariation;
    [Tooltip("심사 실패시의 결과 연출")]
    [SerializeField] GameObject failReviewResultVariation;
    [Tooltip("상금 금액")]
    [SerializeField] TextMeshProUGUI rewardText;

    int reward;
    public int Reward
    {
        get => reward;
        set
        {
            reward = value;

            rewardText.text = $"{reward:n0} 골드";
        }
    }

    /// <summary>
    /// 심사 성공
    /// </summary>
    public void ShowSucess(int reward)
    {
        successReviewResultVariation.SetActive(true);
        failReviewResultVariation.SetActive(false);

        Reward = reward;

        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 심사 실패
    /// </summary>
    public void ShowFail(int reward)
    {
        successReviewResultVariation.SetActive(false);
        failReviewResultVariation.SetActive(true);

        Reward = reward;

        this.gameObject.SetActive(true);
    }
}
