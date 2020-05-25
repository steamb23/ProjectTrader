using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkRoom : MonoBehaviour
{

    public GameObject makePopup;
    public GameObject slot;
    public GameObject materialSlot;

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
    }

    void Update()
    {

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
        for(int i = 0; i < 6; i++)
        {
            recipe[i] = Instantiate(slot) as GameObject;
            SetRecipeSlot(i);
            recipe[i].transform.SetParent(con.transform);
            rcslot[i] = false;

        }
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

        GameObject m_standard = GameObject.Find("MaterialSlot");
        RectTransform standard = m_standard.GetComponent<RectTransform>();

        switch (materialNum)
        {
            case 2:
                material = new GameObject[2];
                for(int i = 0; i < 2; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("WorkRoom")).transform);
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x-(180+100*i), standard.anchoredPosition.y);
                }
                break;
            case 3:
                material = new GameObject[3];
                for (int i = 0; i < 3; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("WorkRoom")).transform);
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    if(i!=2)
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (180 + 100 * i), standard.anchoredPosition.y-30);
                    else
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - 230, standard.anchoredPosition.y+40);
                }
                break;
            case 4:
                material = new GameObject[4];
                for (int i = 0; i < 4; i++)
                {
                    material[i] = Instantiate(materialSlot) as GameObject;
                    material[i].transform.SetParent((GameObject.Find("WorkRoom")).transform);
                    RectTransform tbPos = material[i].GetComponent<RectTransform>();
                    if (i < 2)
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (180 + 100 * i), standard.anchoredPosition.y - 30);
                    else
                        tbPos.anchoredPosition = new Vector3(standard.anchoredPosition.x - (180 + 100 * (i-2)), standard.anchoredPosition.y + 40);
                }
                break;
            default:
                break;
        }
    }

    public void CloseWorkShop()
    {
        GameObject wR = GameObject.Find("WorkRoom");
        RectTransform tbpos = wR.GetComponent<RectTransform>();
        tbpos.anchoredPosition = new Vector3(1500, 0);
    }
}