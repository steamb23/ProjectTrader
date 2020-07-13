using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;

public class ResultWindow : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI sell;
    [SerializeField]
    TextMeshProUGUI tip;
    [SerializeField]
    TextMeshProUGUI rent;
    [SerializeField]
    TextMeshProUGUI salary;
    [SerializeField]
    TextMeshProUGUI total;
    [SerializeField]
    TextMeshProUGUI visitor;

    private void OnEnable()
    {
        // 열릴때 데이터 가져오기
        sell.text = $"{PlayData.CurrentData.DailyStatisticsData.SellMoney}";
        visitor.text = $"손님 수 {PlayData.CurrentData.DailyStatisticsData.VisitorCount}";


        total.text = $"{PlayData.CurrentData.DailyStatisticsData.SellMoney}";
    }

}
