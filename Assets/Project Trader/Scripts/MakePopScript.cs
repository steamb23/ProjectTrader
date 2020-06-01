using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;


//코드에 따라 공방인지, 배치인지 확인
public class MakePopScript : MonoBehaviour
{
    //배치
    public Slider sellNumSlider;
    public TextMeshProUGUI maxNum;
    public Image itemImage;

    //공방
    public Slider makerNumSlider;
    public TextMeshProUGUI makermaxNum;
    public Image makerItemImage;
    public TextMeshProUGUI makerName;


    Item popItem;
    ItemData popItemData;

    int employeeslot;
    GameObject choiceTableData;

    GameObject[] sellingItem;     //판매중인 아이템 검색용

    GameObject uiText;

    PopupState popupstate;

    enum PopupState
    {
        NONE=0,
        SELLPOPUP,
        MAKERPOPUP
    };

    //자기자신 배치 팝업, 공방 팝업
    public GameObject popUpWindow;
    public GameObject makerPopupwindow;

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
        popupstate = PopupState.SELLPOPUP;

    }

    public void Closepopup()
    {
        popUpWindow.SetActive(false);
        popupstate = PopupState.NONE;
    }

    void SetNum()
    {
        //재료들의 공통상한선을 계산(개수가 가장 많은 재료-(개수가 가장 많은 재료-가장 적은 재료))로 계산
        switch (popupstate)
        {
            case PopupState.SELLPOPUP:
                sellNumSlider.maxValue = popItem.Count;
                sellNumSlider.minValue = 1;
                break;
            case PopupState.MAKERPOPUP:
                makerNumSlider.maxValue = popItem.Count;
                makerNumSlider.minValue = 1;
                break;
            case PopupState.NONE:
                break;
        }
    }

    void PrintValue()
    {
        switch (popupstate)
        {
            case PopupState.SELLPOPUP:
                maxNum.text = sellNumSlider.value.ToString() + "/" + sellNumSlider.maxValue.ToString();
                break;
            case PopupState.MAKERPOPUP:
                makermaxNum.text = makerNumSlider.value.ToString() + "/" + makerNumSlider.maxValue.ToString();
                break;
            case PopupState.NONE:
                break;
        }
    }

    //공방용으로 하나 만드는 편이 낫다
    public void SetPopupItem(int cunt, int cod,GameObject obj)
    {
        popItem.Count = cunt;
        popItem.Code = cod;
        itemImage.sprite = ItemSpriteData.GetItemSprite(popItem.Code);
        choiceTableData = obj;
        SetNum();
    }

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
            gogo.GetComponent<SellWindow>().CloseMakerWindow();
            Closepopup();
        }

    }

    //아이템이 판매중인가
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

    //아래부턴 공방ui
    //알바탭으로 제작하라는 신호 보내기(타이머는 알바탭에서 관리? 공방은 정보를 아이템이아니라 알바탭으로 보내기

    public void OpenMakePopup()
    {
        popupstate = PopupState.MAKERPOPUP;
    }

    public void CloseMakePopup()
    {
        popupstate = PopupState.NONE;
        makerPopupwindow.SetActive(false);
    }

    public void SetMakerPopupData(int cunt, int cod,int emplslot)
    {
        popItem.Count = cunt;
        popItem.Code = cod;
        popItemData = popItem.GetData();
        makerItemImage.sprite = ItemSpriteData.GetItemSprite(popItem.Code);
        makerName.text = popItemData.Name;
        employeeslot = emplslot;
        SetNum();
    }

    public void SetMakeItem()
    {
        GameObject go = GameObject.Find("makeroom");
        go.GetComponent<MakerTimer>().StartTimer(employeeslot-1,popItem.Code,popItem.Count);//버튼,코드,갯수
        CloseMakePopup();
    }
}
