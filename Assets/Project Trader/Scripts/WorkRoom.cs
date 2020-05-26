using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;

public class WorkRoom : MonoBehaviour
{

    public GameObject makePopup;
    public GameObject slot;
    public GameObject materialSlot;
    public GameObject workroom;

    public Sprite material1;
    public Sprite material2;

    Image[] ItemImage;

    GameObject materialView;
    GameObject recipeScroll;
    GameObject con;

    TextMeshProUGUI[] slotText;
    GameObject[] recipe;
    GameObject[] material;

    bool[] rcslot;
    int materialNum;

    bool possible = false;


    void Start()
    {

        con = GameObject.Find("Content");
        if (con == null)
            UnityEngine.Debug.Log("content없음");
        //material0 = IngameDatabase.ItemDatas[1].GetMaterial(0);
        //전체 생성하는 함수 먼저
        recipeScroll = GameObject.Find("RecipeView");
        CloseWorkShop();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            CloseWorkShop();
        }
    }

    //공방 ui생성
    public void SetWorkRoom()
    {
        GameObject wR = GameObject.Find("WorkRoom");
        RectTransform tbpos = wR.GetComponent<RectTransform>();
        tbpos.anchoredPosition = new Vector3(0, 0);
    }

    //슬롯하나하나 바꿔주는것까지 추가
    void SetRecipeScroll()
    {
        recipe = new GameObject[6];
        rcslot = new bool[6];
        for(int i = 0; i < 4; i++)
        {
            recipe[i] = Instantiate(slot) as GameObject;
            SetRecipeSlot(i);
            recipe[i].transform.SetParent(con.transform);
            recipe[i].transform.localScale = Vector3.one;
            SlotImageSet(i);
            rcslot[i] = false;

        }
    }

    void SlotImageSet(int i)
    {

        ItemImage = recipe[i].GetComponentsInChildren<Image>();
        ItemImage[5].sprite = ItemSpriteData.GetItemSprite(i+1);

    }

    //순서대로 최대 제작 가능 개수, 이름 , 소모 피로도
    void SetRecipeSlot(int i)
    {
        slotText=recipe[i].GetComponentsInChildren<TextMeshProUGUI>();
        slotText[0].text = 5.ToString();
        slotText[1].text = "pine";
        slotText[2].text = 20.ToString();
    }

    void SetMakeRecipe()
    {

    }

    public void CreateMakePopup()
    {
        if (possible == true) {
            GameObject go = Instantiate(makePopup) as GameObject;
            go.transform.SetParent((GameObject.Find("SubUi")).transform);
            RectTransform tbPos = go.GetComponent<RectTransform>();
            tbPos.anchoredPosition = new Vector3(0, 0);
        }
    }

    void SetMaterial()
    {
        if (material != null)
        {
            for (int i = 0; i < material.Length; i++)
                Destroy(material[i]);
        }

        GameObject m_standard = GameObject.Find("MaterialmakeSlot");
        RectTransform standard = m_standard.GetComponent<RectTransform>();

        switch (materialNum)
        {
            case 2:
                material = new GameObject[2];
                for(int i = 0; i < 2; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("WorkRoom")).transform);
                    material[i].transform.localScale = Vector3.one;
                    //MaterialSetSprite(material[i], i);
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x-(210+120*i), standard.anchoredPosition.y);
                }
                break;
            case 3:
                material = new GameObject[3];
                for (int i = 0; i < 3; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("WorkRoom")).transform);
                    material[i].transform.localScale = Vector3.one;
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    if(i!=2)
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 120 * i), standard.anchoredPosition.y-60);
                    else
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - 270, standard.anchoredPosition.y+50);
                }
                break;
            case 4:
                material = new GameObject[4];
                for (int i = 0; i < 4; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("WorkRoom")).transform);
                    material[i].transform.localScale = Vector3.one;
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    if (i < 2)
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 120 * i), standard.anchoredPosition.y - 60);
                    else
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (210 + 120 * (i-2)), standard.anchoredPosition.y + 50);
                }
                break;
            default:
                break;
        }
    }

    void MaterialSetSprite(GameObject go,int i)
    {
        Image[] ma = new Image[2];
        ma = go.GetComponentsInChildren<Image>();
        ma[1].sprite = material1;
        if (i == 1)
            ma[1].sprite = material2;

        TextMeshProUGUI[] te = new TextMeshProUGUI[2];
        te = go.GetComponentsInChildren<TextMeshProUGUI>();
        te[0].text = 5.ToString();
        if (i == 1)
            te[0].text = 6.ToString();

    }

    public void CloseWorkShop()
    {
        workroom.SetActive(false);
    }

    public void Openpopup()
    {
        workroom.SetActive(true);
        materialNum = 2;
        SetMaterial();
        SetRecipeScroll();
    }
    public void Closepopup()
    {
        workroom.SetActive(false);
    }

}