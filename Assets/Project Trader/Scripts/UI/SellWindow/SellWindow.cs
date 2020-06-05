using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;


public class SellWindow : MonoBehaviour
{
    //배치할때 값주고받을 아이템
    Item arrowC;
    Item[] slotItem;

    ItemData[] slotItemData;
    //배치할 아이템
    Item display;

    public GameObject itemSlot;

    public GameObject arrow;

    public TextMeshProUGUI setPrice;

    public Image setImage;

    public GameObject popupWindow;

    GameObject tableData;
    GameObject setTable;

    GameObject[] itemnum;
    TextMeshProUGUI[] slotText;
    Image[] ItemImage;
    GameObject[] arrowSprite; //화살표
    public GameObject makerWindow;

    //임시로 선언하는 화살표용 bool
    bool setArrow = false;
    //임시로 선언하는 슬롯세팅 bool
    bool setslot = false;

    void Start()
    {
        setTable = GameObject.Find("selltimewindow");
        //생성창으로 이동
    }

    // Update is called once per frame
    void Update()
    {
        SetsettingButton();

    }    

    /*테스트용* 임의로 count를 0으로 만드는 코드
    void minusCont()
    {
        GameObject[] sellingItem = GameObject.FindGameObjectsWithTag("Item");
        Item reitem = sellingItem[1].GetComponent<DisplayedItem>().Item;
        reitem.Count = 0;
        sellingItem[1].GetComponent<DisplayedItem>().Item = reitem;
    }
    */

    //슬롯 제거하는 코드도 추가로 작성(불러오기전에 제거하고 다시 불러올수있도록
    public void OpenMakerWindow()
    {
        makerWindow.SetActive(true);
        SetItemslot();
    }

    public void CloseMakerWindow()
    {
        //슬롯 리로드하는 코드도 추가로 작성
        makerWindow.SetActive(false);
    }

    //슬롯 세팅(임시로 이미 생성되어있으면 추가로 생성하지 않도록 함)
    void SetItemslot()
    {
        if (setslot == false)
        {
            itemnum = new GameObject[5];
            slotItem = new Item[5];
            slotItemData = new ItemData[5];

            //아이템 가진 수만큼
            for (int i = 0; i < 5; i++)
            {
                itemnum[i] = Instantiate(itemSlot) as GameObject;

                SetItemInfo(i);
                itemnum[i].transform.SetParent((GameObject.Find("ItemContent")).transform);
                itemnum[i].transform.localScale = Vector3.one;


            }
            SlotScriptSet();
            setslot = true;
        }
    }
    
    //임시로 슬롯내 스크립트 설정
    void SlotScriptSet()
    {
        GameObject[] slotsetting = GameObject.FindGameObjectsWithTag("Slot");
        for (int i = 0; i < slotsetting.Length; i++)
        {
            slotsetting[i].GetComponent<SlotIn>().SetSlotInData(slotItem[i].Count, slotItem[i].Code);
            if (i == 0)
                slotsetting[i].GetComponent<SlotIn>().PushButton();
        }
    }

    //슬롯생성할때 멤버 바꾸기
    void SetItemInfo(int i)
    {
        slotText = itemnum[i].GetComponentsInChildren<TextMeshProUGUI>();

        //임시로 값 할당
        slotItem[i].Code = i + 1;

        slotItemData[i] = slotItem[i].GetData();
        slotText[0].text = slotItemData[i].SellPrice.ToString();

        slotItem[i].Count= UnityEngine.Random.Range(1, 99);
        slotText[1].text = "x" + slotItem[i].Count.ToString();

        

        SpriteChange(i);

    }


    void SpriteChange(int i)
    {
        ItemImage = itemnum[i].GetComponentsInChildren<Image>();
        ItemImage[4].sprite = ItemSpriteData.GetItemSprite(slotItem[i].Code);

    }

    void SetsettingButton()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Item");

        if (setArrow == false)
        {
            arrowSprite = new GameObject[go.Length - 1];
            for (int j = 1; j < go.Length; j++)
            {
                arrowSprite[j-1] = Instantiate(arrow) as GameObject;
                arrowSprite[j-1].transform.position = new Vector3(go[j].transform.position.x, go[j].transform.position.y + 0.6f, -1);
                arrowSprite[j - 1].SetActive(false);
            }
            setArrow = true;
        }

        for (int i = 1; i < go.Length; i++)
        {
            arrowC = go[i].GetComponent<DisplayedItem>().Item;
            if (arrowC.Count <= 0)
            {
                arrowSprite[i - 1].SetActive(true);
            }

        }

    }

    //디스플레이할 아이템 보여주기

    public void SetCheckdisplay(int num, int code)
    {
        display.Code = code;
        display.Count = num;
        ItemData displayData = display.GetData();
        setPrice.text = displayData.SellPrice.ToString();
        setImage.sprite= ItemSpriteData.GetItemSprite(display.Code);
        //텍스트랑 image찾아서 변경
    }


    public void SetPopUpWindow()
    {
        tableData = setTable.GetComponent<TableCheck>().choiceTable;
        popupWindow.SetActive(true);
        popupWindow.GetComponent<MakePopScript>().Openpopup();
        popupWindow.GetComponent<MakePopScript>().SetPopupItem(display.Count, display.Code,tableData);
    }
}

