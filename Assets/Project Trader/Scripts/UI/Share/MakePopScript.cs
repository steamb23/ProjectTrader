using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;


//코드에 따라 공방인지, 배치인지 확인
public class MakePopScript : MonoBehaviour
{
    //배치
    public Slider sellNumSlider;
    public TextMeshProUGUI maxNum;
    public Image itemImage;

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
        //MAKERPOPUP,
        SHOPPOPUP,
    };

    //자기자신 배치 팝업, 공방 팝업
    public GameObject popUpWindow;
    //public GameObject makerPopupwindow;
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
            case PopupState.SHOPPOPUP:
                shopitemnum.text = shopslider.value.ToString() + "/" + shopslider.maxValue.ToString();
                break;
            case PopupState.NONE:
                break;
        }
    }

    //배치용->배치 playData에 추가
    public void SetPopupItem(int cunt, int cod,GameObject obj)
    {
        popItem.Count = cunt;
        popItem.Code = cod;
        itemImage.sprite = ItemSpriteData.GetItemSprite(popItem.Code);
        choiceTableData = obj;
        SetNum();
    }

    //이곳에 회수하는 코드, 아이템 count제거하는 코드 추가
    public void SetItem()
    {
        
        sell = SellItemCheck(popItem.Code);
        if (sell)
        {
            Item reitem = choiceTableData.GetComponent<DisplayedItem>().Item;
            reitem.Code = popItem.Code;
            reitem.Count = popItem.Count;
            GameObject gogo = GameObject.Find("selltimewindow");
            if(choiceTableData.GetComponent<DisplayedItem>().Item.Count>0)
                gogo.GetComponent<SellWindow>().DisItemCheck(choiceTableData.GetComponent<DisplayedItem>().Item.Code, choiceTableData.GetComponent<DisplayedItem>().Item.Count); //있는 아이템 회수
            
            choiceTableData.GetComponent<DisplayedItem>().Item = reitem;
            //임의로
            
            gogo.GetComponent<SellWindow>().DisItemCheck(reitem.Code,-reitem.Count); //배치한 수 만큼 가진 수에서 제거
            gogo.GetComponent<SellWindow>().CloseMakerWindow();
            Closepopup();
        }

    }

    //아이템이 판매중인가 ->playData에서 검사
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
        if (PlayData.CurrentData.Money >= popItem.Count * popItemData.SellPrice)
        {
            popItem.Count = (int)shopslider.value;
            PlayData.CurrentData.Money -= popItem.Count * popItemData.SellPrice;
            FindObjectOfType<DataSave>().ItemListAdd(popItem);

            GameObject go = GameObject.Find("itemshop");
            go.GetComponent<ShopWindow>().SetbuyNum(popItem.Code, (int)shopslider.value);
            go.GetComponent<ShopTimer>().SetInfo((int)shopslider.value, popItem.Code);

        }
        else
        {
            uiText.GetComponent<TextUiControl>().CreativeTextBox(0, 0, 50, "돈이 부족합니다.", 2);
        }
        CloseShopPopup();
    }

    public void CloseShopPopup()
    {
        popupstate = PopupState.NONE;
        shopPopup.SetActive(false);
    }
}
