using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupEX : MonoBehaviour
{

    public Slider makeNumSliders;
    public TextMeshProUGUI maxNums;
    public Image itemImages;
    public GameObject popUpWindow;

    // Start is called before the first frame update
    void Start()
    {
        Closepopup();
    }

    // Update is called once per frame
    void Update()
    {
        PrintValue();
    }

    void PrintValue()
    {
        //인덱스를 읽어와서 차례로 출력
        maxNums.text = makeNumSliders.value.ToString() + "/" + makeNumSliders.maxValue.ToString();
    }
    void SetNum()
    {
        //재료들의 공통상한선을 계산(개수가 가장 많은 재료-(개수가 가장 많은 재료-가장 적은 재료))로 계산
        makeNumSliders.maxValue = 3;
        makeNumSliders.minValue = 1;
    }
    public void Openpopup()
    {
        popUpWindow.SetActive(true);
        SetNum();
    }

    //아이템슬롯에서 받아오기
    public void SetPopupItem(int cunt, int cod)
    {
        //popItem.Count = cunt;
        //popItem.Code = cod;
        //itemImages.sprite = ItemSpriteData.GetItemSprite(popItem.Code);
        Openpopup();
    }

    public void Closepopup()
    {
        popUpWindow.SetActive(false);
    }

}
