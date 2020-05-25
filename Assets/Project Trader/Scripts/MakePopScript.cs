using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;


public class MakePopScript : MonoBehaviour
{

    public Slider makeNumSlider;
    public TextMeshProUGUI maxNum;
    public Image itemImage;

    Item popItem;

    //자기자신
    public GameObject popUpWindow;
    TextMeshProUGUI chaneT;

    int fullNum;

    void Start()
    {
        Closepopup();

    }


    void Update()
    {
        PrintValue();

    }

    void Openpopup()
    {
        popUpWindow.SetActive(true);
        SetNum();
    }

    public void Closepopup()
    {
        popUpWindow.SetActive(false);
    }

    void SetNum()
    {
        //재료들의 공통상한선을 계산(개수가 가장 많은 재료-(개수가 가장 많은 재료-가장 적은 재료))로 계산
        makeNumSlider.maxValue = popItem.Count;
        makeNumSlider.minValue = 1;
    }

    void PrintValue()
    {
        //인덱스를 읽어와서 차례로 출력
        maxNum.text = makeNumSlider.value.ToString() + "/" + makeNumSlider.maxValue.ToString();
    }

    public void SetPopupItem(int cunt, int cod)
    {
        popItem.Count = cunt;
        popItem.Code = cod;
        itemImage.sprite = ItemSpriteData.GetItemSprite(popItem.Code);
        Openpopup();
    }

    //바꾸는 함수 테이블받아와야함
    public void SetItem()
    {
        //임의로 한개 변환
        GameObject ii = GameObject.Find("Item");
        Item reitem = ii.GetComponent<DisplayedItem>().Item;
        reitem.Code = popItem.Code;
        reitem.Count = popItem.Count;
        ii.GetComponent<DisplayedItem>().Item = reitem;
        //임의로
        GameObject gogo = GameObject.Find("selltimewindow");
        gogo.GetComponent<MakerWindow>().CloseMakerWindow();

        Closepopup();


    }
}
