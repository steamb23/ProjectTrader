using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;


public class MakePopScript : MonoBehaviour
{

    public Slider makeNumSlider;
    public TextMeshProUGUI maxNum;
    public Image itemImage;

    Item popItem;
    GameObject choiceTableData;

    GameObject[] sellingItem;

    GameObject uiText;
    //자기자신
    public GameObject popUpWindow;
    TextMeshProUGUI chaneT;
    bool sell;
    int fullNum;

    void Start()
    {
        uiText = GameObject.Find("TextUiControl");
    }


    void Update()
    {
        PrintValue();
    }

    public void Openpopup()
    {
        popUpWindow.SetActive(true);

    }

    public void Closepopup()
    {
        popUpWindow.SetActive(false);
    }

    void SetNum()
    {
        //재료들의 공통상한선을 계산(개수가 가장 많은 재료-(개수가 가장 많은 재료-가장 적은 재료))로 계산
        makeNumSlider.maxValue = popItem.Count;
        makeNumSlider.minValue = 1;
    }

    void PrintValue()
    {
        //인덱스를 읽어와서 차례로 출력
        maxNum.text = makeNumSlider.value.ToString() + "/" + makeNumSlider.maxValue.ToString();
    }

    public void SetPopupItem(int cunt, int cod,GameObject obj)
    {
        popItem.Count = cunt;
        popItem.Code = cod;
        itemImage.sprite = ItemSpriteData.GetItemSprite(popItem.Code);
        choiceTableData = obj;
        SetNum();

    }

    //바꾸는 함수 테이블받아와야함
    public void SetItem()
    {
        
        sell = SellItemCheck(popItem.Code);
        if (sell != false)
        {
            Item reitem = choiceTableData.GetComponent<DisplayedItem>().Item;
            reitem.Code = popItem.Code;
            reitem.Count = popItem.Count;
            choiceTableData.GetComponent<DisplayedItem>().Item = reitem;
            //임의로
            GameObject gogo = GameObject.Find("selltimewindow");
            gogo.GetComponent<MakerWindow>().CloseMakerWindow();
            Closepopup();
        }

    }

    //배치하기 눌렀을때 
    bool SellItemCheck(int cod)
    {
        sellingItem = GameObject.FindGameObjectsWithTag("Item");
        Item selling;
        for (int i = 1; i < sellingItem.Length; i++)
        {
            selling = sellingItem[i].GetComponent<DisplayedItem>().Item;
            if (sellingItem[i].name != choiceTableData.name)
            {
                if (cod == selling.Code)
                {
                    uiText.GetComponent<TextUiControl>().CreativeTextBox(0, 0, 50, "이미 판매중인 아이템입니다", 2);
                    return false;
                }
            }

        }
        return true;
    }
    //임시코드

}
