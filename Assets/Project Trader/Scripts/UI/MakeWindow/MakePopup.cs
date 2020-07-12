using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;


public class MakePopup : MonoBehaviour
{
    [SerializeField]
    Slider makerSlider;

    [SerializeField]
    TextMeshProUGUI makermaxNum;

    [SerializeField]
    Image makerItemImage;

    [SerializeField]
    TextMeshProUGUI makerName;

    Item popItem;
    ItemData popItemData;

    int employeeslot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        makermaxNum.text = makerSlider.value.ToString() + "/" + makerSlider.maxValue.ToString();
    }

    public void SetMakerPopupData(int cunt, int cod, int emplslot)
    {
        popItem.Count = cunt;
        popItem.Code = cod;
        popItemData = popItem.GetData();
        makerItemImage.sprite = popItemData.GetSprite();
        makerName.text = popItemData.Name;
        employeeslot = emplslot;
        SetNum();
    }

    void SetNum()
    {
        makerSlider.maxValue = popItem.Count;
        makerSlider.minValue = 1;
    }

    //이곳에서 알바생이 있는지 판별
    public void SetMakeItem()
    {
        GameObject go = GameObject.Find("makeroom");
        go.GetComponent<MakerTimer>().StartTimer(employeeslot - 1, popItem.Code, (int)makerSlider.value);//버튼,코드,갯수
        FindObjectOfType<MakeEmpslot>().PrintMakeItemSprite();
        FindObjectOfType<MakerUI>().CheckCost(popItemData, (int)makerSlider.value);
        FindObjectOfType<MakerUI>().CloseMakeRoom();
        gameObject.SetActive(false);
    }
}