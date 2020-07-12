using System.Collections;
using System.Collections.Generic;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//제작할 슬롯을 선택하는 창->알바관리와 같이하도록
public class MakeEmpslot : MonoBehaviour
{
    //제작되는 아이템 확인용
    [SerializeField]
    Image makeItemsprite;
    ItemData makingItem;
    [SerializeField]
    Sprite[] numsprite = new Sprite[6];
    [SerializeField]
    Image[] numImage = new Image[3];

    bool[] employeeInfo;  //임시로 알바생이 있다는 표시
    bool[] working;       //슬롯이 일하고 있다면
    public int clickEmployee
    {
        set;
        get;
    }//알바선택창
    RectTransform[] rt;
    public Sprite b_on;
    public Sprite b_off;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PushOneButton()
    {
        clickEmployee = 1;
        ChangeButtonInfo();
        FindObjectOfType<MakerTimer>().NumSet(0);
        PrintMakeItemSprite();
    }

    public void PushTwoButton()
    {
        clickEmployee = 2;
        ChangeButtonInfo();
        FindObjectOfType<MakerTimer>().NumSet(1);
        PrintMakeItemSprite();
    }

    public void PushThreeButton()
    {
        clickEmployee = 3;
        ChangeButtonInfo();
        FindObjectOfType<MakerTimer>().NumSet(2);
        PrintMakeItemSprite();
    }

    //버튼 위치 스프라이트 바꿔주기..
    void ChangeButtonInfo()
    {
        GameObject[] bu = GameObject.FindGameObjectsWithTag("MPEB");
        Image[] img = new Image[3];
        GameObject go = GameObject.Find("alba_bg");
        RectTransform bg = go.GetComponent<RectTransform>();
        rt = new RectTransform[3];
        for (int j = 0; j < 3; j++)
        {
            img[j] = bu[j].GetComponentInChildren<Image>();
            rt[j] = bu[j].GetComponent<RectTransform>();
        }
        for (int i = 0; i < bu.Length; i++)
        {
            if (clickEmployee == i + 1)
            {
                img[i].sprite = b_on;
                numImage[i].sprite = numsprite[i];
                rt[i].anchoredPosition = new Vector3(bg.anchoredPosition.x - 180, rt[i].anchoredPosition.y);
            }
            else
            {
                img[i].sprite = b_off;
                numImage[i].sprite = numsprite[i + 3];
                rt[i].anchoredPosition = new Vector3(bg.anchoredPosition.x - 160, rt[i].anchoredPosition.y);
            }
        }
    }

    public void PrintMakeItemSprite()
    {
        var canvasGroup = makeItemsprite.GetComponent<CanvasGroup>();

        if (FindObjectOfType<MakerTimer>().inTimer[clickEmployee - 1] == true)
        {
            canvasGroup.alpha = 1.0f;
            makingItem = FindObjectOfType<MakerTimer>().making[clickEmployee - 1];
            makeItemsprite.sprite = ItemSpriteData.GetItemSprite(makingItem.Code);

        }
        else
        {
            canvasGroup.alpha = 0f;
            FindObjectOfType<MakerUI>().working[clickEmployee - 1] = false;
        }
    }
}
