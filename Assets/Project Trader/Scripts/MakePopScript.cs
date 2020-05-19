using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MakePopScript : MonoBehaviour
{
    public Slider makeNumSlider;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI maxNum;
    TextMeshProUGUI chaneT;
    int fullNum;

    void Start()
    {
        SetNum();
    }


    void Update()
    {
        PrintValue();
    }

    public void Closepopup()
    {
        Destroy(GameObject.Find("MakePopup(Clone)"));
    }

    void SetNum()
    {
        //재료들의 공통상한선을 계산(개수가 가장 많은 재료-(개수가 가장 많은 재료-가장 적은 재료))로 계산
        makeNumSlider.maxValue = 50;
    }

    void PrintValue()
    {
        //인덱스를 읽어와서 차례로 출력
        itemName.text = "pine";
        maxNum.text = makeNumSlider.value.ToString() + "/" + makeNumSlider.maxValue.ToString();
    }
}
