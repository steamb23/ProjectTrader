using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using System.Collections.Specialized;

public class MakerWindow : MonoBehaviour
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

    GameObject[] itemnum;
    TextMeshProUGUI[] slotText;
    Image[] ItemImage;
    GameObject[] arrowSprite; //화살표
    public GameObject makerWindow;
    //임시로 선언하는 화살표용 bool
    bool setArrow = false;

    void Start()
    {
        makerWindow.SetActive(true);
        //생성창으로 이동
        SetItemslot();
        SetCheckdisplay(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //OpenMakerWindow();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            //CloseMakerWindow();
        }
            /*
            if (Input.GetKeyDown(KeyCode.G))
            {
                SetItemslot();
            }
            if (Input.GetMouseButtonDown(0))
            {
                CheckingMouse();

            }*/
        }

    /*
    void CheckingMouse()
    {
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(clickPos,Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (rayHit.collider.gameObject.tag == "ItemTable")
        {
            tablename = rayHit.collider.gameObject.name;
            UnityEngine.Debug.Log("djfwlejfklsdjfklsjdf");
        }

    }
    */
    void OpenMakerWindow()
    {
        makerWindow.SetActive(true);
    }

    public void CloseMakerWindow()
    {
        makerWindow.SetActive(false);
    }
    void SetItemslot()
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
    }
    
    void SlotScriptSet()
    {
        GameObject[] slotsetting = GameObject.FindGameObjectsWithTag("Slot");
        for(int i=0;i<slotsetting.Length;i++)
            slotsetting[i].GetComponent<SlotIn>().SetSlotInData(slotItem[i].Count, slotItem[i].Code);
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
        ItemImage[4].sprite = ItemSpriteData.GetItemSprite(slotItem[i].Code); //스프라이트 바꿈

    }


    //배치 아이템 바꾸는 함수 -> 태그로 찾아서 만들면 된다!!
    void SetItem()
    {
        GameObject ii = GameObject.Find("Item");
        Item reitem = ii.GetComponent<DisplayedItem>().Item;
        reitem.Code = 5;
        ii.GetComponent<DisplayedItem>().Item = reitem;
    }


    //배치된 아이템 수를 검사하고 0일때 화살표,버튼 호출(ui호출은 버튼에서 처리) ->그다음에 코드, 카운트 교체(배치버튼으로)
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


    public void SetCheckdisplay(int num, int code)
    {
        display.Code = code;
        display.Count = num;
        ItemData displayData = display.GetData();
        setPrice.text = displayData.SellPrice.ToString();
        setImage.sprite= ItemSpriteData.GetItemSprite(display.Code);
        //텍스트랑 image찾아서 변경
    }

    //팝업윈도우 생성,할당
    public void SetPopUpWindow()
    {
        popupWindow.GetComponent<MakePopScript>().SetPopupItem(display.Count, display.Code);
    }
}

