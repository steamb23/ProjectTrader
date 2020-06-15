﻿using System.Collections;
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

    //상점
    public Slider shopslider;
    public TextMeshProUGUI shopitemname;
    public Image shopitemsprite;
    public TextMeshProUGUI shopitemnum;


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
        MAKERPOPUP,
        SHOPPOPUP,
    };

    //자기자신 배치 팝업, 공방 팝업
    public GameObject popUpWindow;
    public GameObject makerPopupwindow;
    public GameObject shopPopup;

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
            case PopupState.SHOPPOPUP:
                shopslider.maxValue = popItem.Count;
                shopslider.minValue = 1;
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
            case PopupState.SHOPPOPUP:
                shopitemnum.text = shopslider.value.ToString() + "/" + shopslider.maxValue.ToString();
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
        makerItemImage.sprite = popItemData.GetSprite();
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



    //상점
    public void ShopPopupOpen()
    {
        popupstate = PopupState.SHOPPOPUP;
    }


    public void SetShopItem(int cod,int cunt)
    {
        UnityEngine.Debug.Log(cod.ToString());
        popItem.Code = cod;
        popItem.Count = cunt;
        popItemData = popItem.GetData();
        shopitemsprite.sprite = popItemData.GetSprite();
        shopitemname.text = popItemData.Name;
        SetNum();
    }

    public void SetBuyItem()
    {
        //이곳에서 돈 확인
        GameObject go = GameObject.Find("itemshop");
        go.GetComponent<ShopWindow>().SetbuyNum(popItem.Code, (int)shopslider.value);
        go.GetComponent<ShopTimer>().SetInfo((int)shopslider.value, popItem.Code);
        //이곳에서 인벤토리?에 아이템추가(불러오기)
        CloseShopPopup();
    }

    public void CloseShopPopup()
    {
        popupstate = PopupState.NONE;
        shopPopup.SetActive(false);
    }
}