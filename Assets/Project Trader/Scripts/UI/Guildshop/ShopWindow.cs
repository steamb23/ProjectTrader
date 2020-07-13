using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using System.ComponentModel;

public class ShopWindow : MonoBehaviour
{
    //플레이어 돈을 받아와서 구매처리하는 코드도 당연히 필요

    public GameObject shopWindow;
    public GameObject shopslot;
    public GameObject shopPopup;

    GameObject useData;
    //슬롯에 필요
    GameObject[] shopItem;
    Item[] shopItemInfo;
    ItemData[] shopItemData;

    int[] buyNum;          //플레이어가 구매한 개수
    int[] itemMaxnum;      //구매가능한 개수(max)
    float[] timeDelay;
    bool setslot = false;  //슬롯세팅이 되었는지
    bool setwindow = false;
    Image[] slotImage;
    TextMeshProUGUI[] slottext;

    Item checkItem;
    ItemData checkItemData;

    void Start()
    {
        useData = GameObject.Find("SaveData");
    }


    void Update()
    {

    }

    public void OpenShopWindow()
    {
        shopWindow.SetActive(true);
        if (setslot == false)
        {
            SetShopslot();
            setslot = true;
        }
    }

    public void CloseShopWindow()
    {
        setwindow = false;
        shopWindow.SetActive(false);
    }

    //슬롯
    void SetShopslot()
    {
        //임시 지정, 판매슬롯은 변동이 없기 때문에 한번 생성후 고정,material개수만큼
        shopItem = new GameObject[85];
        shopItemInfo = new Item[85];
        shopItemData = new ItemData[85];
        buyNum = new int[85];
        itemMaxnum = new int[85];
        timeDelay = new float[85];
        int j = 0;
        for(int i = 0; i < 85; i++)
        {
            checkItem.Code = i + 1;
            checkItemData = checkItem.GetData();
            //임시 지정
            if ((int)checkItemData.Type==1) {
                shopItemInfo[j].Code = i + 1;
                shopItemData[j] = shopItemInfo[j].GetData();
                buyNum[j] = 0;
                itemMaxnum[j] = 30;
                timeDelay[j] = 125f;

                shopItem[j] = Instantiate(shopslot) as GameObject;
                //슬롯세팅
                SetshopslotData(j);
                SlotInDataSet(j);
                SlotImage(j);
                shopItem[j].transform.SetParent((GameObject.Find("ShopContent")).transform);
                shopItem[j].transform.localScale = Vector3.one;
                j++;
            }
            
        }

    }
    
    //슬롯초기화
    public void SetshopslotData(int i)
    {
        slottext=shopItem[i].GetComponentsInChildren<TextMeshProUGUI>();
        slottext[0].text = shopItemData[i].Name;
        slottext[1].text = (itemMaxnum[i] - buyNum[i]).ToString() + "/" + itemMaxnum[i].ToString();
        slottext[2].text = "00:00";
        //slottext[3].text = shopItemData[i].ShopPrice.ToString();
        //int dd = UnityEngine.Random.Range(100, 500);
        //slottext[3].text = dd.ToString();
        //int dd = UnityEngine.Random.Range(shopItemData[i].ShopPrice.min, shopItemData[i].ShopPrice.max);
        slottext[3].text = shopItemData[i].ShopPrice.max.ToString();


    }
    void SlotImage(int i)
    {
        Image[] slotImage = shopItem[i].GetComponentsInChildren<Image>();
        slotImage[6].sprite = shopItemData[i].GetSprite();
    }
    //타이머출력
    public void SetTimePrint(int i, bool timer,float timedelay)
    {
        slottext = shopItem[i].GetComponentsInChildren<TextMeshProUGUI>();
        if (timer == true)
        {
            float minute;
            float second;

            minute = (int)(timedelay / 60);
            second = timedelay - (60 * minute);
            slottext[2].text = string.Format("{0:00}", minute) + ":" + string.Format("{0:00}", second);
        }
        else
            slottext[2].text = "00:00";
    }

    public void OpenShopPopup()
    {
        shopPopup.SetActive(true);
        setwindow = true;
        shopPopup.GetComponent<MakePopScript>().ShopPopupOpen();
    }

    void SlotInDataSet(int i)
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Slot");
        UnityEngine.Debug.Log(go.Length.ToString());
        go[i].GetComponent<SlotIn>().SetSlotInData(itemMaxnum[i] - buyNum[i], shopItemData[i].Code);
    }

    //여기서 타이머로 신호 전달
    public void SetbuyNum(int cod, int value)
    {
        int i;
        for (i = 0; i < shopItemInfo.Length; i++)
        {
            if (shopItemInfo[i].Code == cod)
            {
                buyNum[i] += value;
                if (setwindow==true)
                {
                    SetshopslotData(i);
                    SlotInDataSet(i);
                }
                return;
            }
        }
    }

    ////돈 사용, 아이템 추가,아이템 코드와 카운트, 아이템 가격이 필요함
    //public void InItemUseMoney(Item sellitem,int value)
    //{
    //    //useData.GetComponent<DataSave>().UseMoney(); //가격추가하고
    //    useData.GetComponent<DataSave>().UseItem(sellitem.Code,value);
    //}

}
