using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using System.Linq;
using System.Dynamic;
using System.ComponentModel;

//새로 사용할 공방 스크립트

public class MakerUI : MonoBehaviour
{

    public GameObject slot;
    public GameObject materialSlot;
    public GameObject makerpopupwindow;
    public GameObject makewindow;
    GameObject data;

    Item[] slotItem;           //슬롯 아이템
    ItemData[] recipeData;     //슬롯 데이터 받아올 아이템데이터

    GameObject con;

    Image[] itemImage;

    ItemData disRecipeData;
    Item materialSample;

    //제작할 아이템
    ItemData[] makeItemData;
    Item[] makeItem;
    Item materialItem; //재료용

    TextMeshProUGUI[] slotText;     //슬롯에 있는 텍스트
    GameObject[] recipe;            //레시피 > 데이터로 받아오기
    GameObject[] material;          //재료   > 데이터로 받아오기

    int materialNum;                //재료 갯수 > 슬롯에서 데이터로 받아오기
    int[] maNeeds;

    bool[] employeeInfo;  //임시로 알바생이 있다는 표시
    bool[] working;       //슬롯이 일하고 있다면
    int clickEmployee;  //알바선택창

    public Sprite b_on;
    public Sprite b_off;
    RectTransform[] rt;

    bool canMake=false;

    GameObject tim;

    void Start()
    {
        data = GameObject.Find("SaveData");
        maNeeds = new int[4];
        con = GameObject.Find("RecipeContent");
        //초기화기준=총 아이템 개수만큼(csv)진행하고, 안쓰는 공간은 비워둔채로
        makeItemData = new ItemData[3];
        makeItem = new Item[3];
        employeeInfo = new bool[3];
        working = new bool[3];
        tim = GameObject.Find("makeroom");

        //임시 초기화
        for (int i = 0; i < 3; i++)
        {
            working[i] = false;  //일하고 있지 않고
            employeeInfo[i] = true; //알바생이 있다
        }
    }


    void Update()
    {

    }


    //재료창 생성 
    void SetMaterial()
    {
        if (material != null)
        {
            for (int i = 0; i < material.Length; i++)
                Destroy(material[i]);
        }

        GameObject m_standard = GameObject.Find("MaterialmakeSlot");
        RectTransform standard = m_standard.GetComponent<RectTransform>();
        Image[] standardImage = m_standard.GetComponentsInChildren<Image>();
        standardImage[1].sprite = disRecipeData.GetSprite();
        switch (materialNum)
        {
            case 2:
                material = new GameObject[2];
                for (int i = 0; i < 2; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("MakerRoom")).transform);
                    material[i].transform.localScale = Vector3.one;
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 140 * i), standard.anchoredPosition.y);
                }
                break;
            case 3:
                material = new GameObject[3];
                for (int i = 0; i < 3; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("MakerRoom")).transform);
                    material[i].transform.localScale = Vector3.one;
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    if (i != 2)
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 140 * i), standard.anchoredPosition.y - 70);
                    else
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - 280, standard.anchoredPosition.y + 60);
                }
                break;
            case 4:
                material = new GameObject[4];
                for (int i = 0; i < 4; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("MakerRoom")).transform);
                    material[i].transform.localScale = Vector3.one;
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    if (i < 2)
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 140 * i), standard.anchoredPosition.y - 70);
                    else
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 140 * (i - 2)), standard.anchoredPosition.y + 60);
                }
                break;
            default:
                break;
        }
        MaterialSlotSetting();
    }
    
    //스크롤에 슬롯 생성
    void SetRecipeScroll()
    {
        FindObjectOfType<MakeEmpslot>().PushOneButton();
        //PushOneButton();//첫번째 눌러놓기
        recipe = new GameObject[6];
        slotItem = new Item[5];
        recipeData = new ItemData[5];

        for (int i = 0; i < 4; i++)
        {
            recipe[i] = Instantiate(slot) as GameObject;
            SetRecipeSlot(i);

            //재료별로 갯수받아와서 가능한 갯수출력후
            //setslotindata로 슬롯에 데이터 부여

            recipe[i].transform.SetParent((GameObject.Find("RecipeContent")).transform);
            recipe[i].transform.localScale = Vector3.one;
            SlotImageSet(i);
        }
        INSlotScriptSet();
    }

    //슬롯 이미지 바꾸기
    void SlotImageSet(int i)
    {

        itemImage = recipe[i].GetComponentsInChildren<Image>();//5
        itemImage[4].sprite = ItemSpriteData.GetItemSprite(slotItem[i].Code);
    }


    //순서대로 이름,소모 피로도,최대 제작 가능 개수
    //데이터 받아오기
    void SetRecipeSlot(int i)
    {
        slotItem[i].Code = i + 1;
        recipeData[i] = slotItem[i].GetData();
        slotText = recipe[i].GetComponentsInChildren<TextMeshProUGUI>();
        slotText[0].text = recipeData[i].Name;
        slotText[1].text = recipeData[i].Tier.ToString();
        slotText[2].text = 5.ToString();//제작가능한 숫자->재료들의 양을 계산해서 최솟값찾기, 이걸 반환해서 별다른 계산없이 팝업에서 사용하도록
    }

    //임시코드
    //타이머 추가
    //데이터 관리 추가
    //슬롯 데이터 부여 추가

    //슬롯 세팅할때
    void INSlotScriptSet()
    {
        GameObject[] slotsetting = GameObject.FindGameObjectsWithTag("Slot");
        if (slotsetting == null)
            UnityEngine.Debug.Log("슬롯없음");
        UnityEngine.Debug.Log("슬롯 갯수  :  "+slotsetting.Length.ToString());
        for (int i = 0; i < slotsetting.Length; i++)
        {
            int k = UnityEngine.Random.Range(5, 10);
            slotsetting[i].GetComponent<MakeSlot>().SetSlotInData(k,slotItem[i].Code);
            if (i == 0)
                slotsetting[i].GetComponent<MakeSlot>().MakerslotPushButton();
        }
    }

    //슬롯에서 받아와 제작창 세팅
    public void SetMakerBg(int cunt, int cod)
    {
        int i;
        
        if (materialNum == null)
            return;
        materialSample.Code = cod;
        disRecipeData= materialSample.GetData();
        maNeeds = disRecipeData.MaterialNeeds;

        for(i = 0; i < maNeeds.Length; i++)
        {
            UnityEngine.Debug.Log(maNeeds[i].ToString());
            if (maNeeds[i] == 0)
                break;
        }
        materialNum = i;
        //materialNum=disRecipeData.
        SetMaterial();
    }

    //직원이 일할 슬롯 선택(버튼으로 설정)
    void SetWorkingSlot()
    {

    }

    //재료창을 수정해서 출력 레시피의 코드를 받아서 그 아이템이 필요한 material의
    //스프라이트(스프라이트는 아이템에 같이)를 불러옴
    void MaterialSlotSetting()
    {
        int[] mtcod = new int[4];
        mtcod=disRecipeData.MaterialCodes;
        //ItemData matData;
        //material의 이미지를 받아서 변경
        Image[] mimg;
        for(int i = 0; i < materialNum; i++)
        {
            mimg=material[i].GetComponentsInChildren<Image>();
            //matData.Code = mtcod[i];
            mimg[1].sprite = ItemSpriteData.GetItemSprite(mtcod[i]);
        }
    }
    
    //만들 아이템코드와 갯수 설정 하고 팝업으로
    void MakeItemInfo(int cod, int count)
    {
        clickEmployee=FindObjectOfType<MakeEmpslot>().clickEmployee;
        makeItem[clickEmployee-1].Code = cod;
        makeItem[clickEmployee-1].Count = count;
        makeItemData[clickEmployee-1] = makeItem[clickEmployee-1].GetData();
        if(working[clickEmployee-1]==false && employeeInfo[clickEmployee-1] == true)
        {
            canMake = true;
        }
    }

    //팝업만들기
    public void CreateMakePopup()
    {
        MakeItemInfo(materialSample.Code, 5);
        if (canMake == true)
        {
            makerpopupwindow.SetActive(true);
            //makerpopupwindow.GetComponent<MakePopScript>().OpenMakePopup();
            makerpopupwindow.GetComponent<MakePopup>().SetMakerPopupData(5, materialSample.Code, clickEmployee);//갯수코드슬롯
        }
    }


    public void OpenMakeRoom()
    {
        makewindow.SetActive(true);
        SetRecipeScroll();
    }

    public void CloseMakeRoom()
    {
        for (int i = 0; i < recipe.Length; i++)
            Destroy(recipe[i]);
        makewindow.SetActive(false);
    }

    //제작시 count,money-하는 코드
    public void CheckCost()
    {
        int[] mt = new int[4];
        int[] ct = new int[4];
        //bool[] ok = new bool[4];
        mt = disRecipeData.MaterialCodes;
        ct = disRecipeData.MaterialNeeds;

        for (int i = 0; i < materialNum; i++)
        {

            materialItem.Code = mt[i];
            materialItem.Count = -ct[i];

            //FindObjectOfType<DataSave>().ItemListAdd(materialItem); //임시로 가려두기
        }
        data.GetComponent<DataSave>().UseMoney(-(disRecipeData.CraftCost)); //교체

    }

    public void MakeSuccess(Item makeItem)
    {

        FindObjectOfType<DataSave>().ItemListAdd(makeItem);
    }
}
