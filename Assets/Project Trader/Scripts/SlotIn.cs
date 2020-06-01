using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotIn : MonoBehaviour
{
    //배치일 경우 판매가능한 수, 코드 / 공방일 경우 제작가능한 수, 코드
    public int count;
    public int code;

    public void SetSlotInData(int cunt, int cod)
    {
        count = cunt;
        code = cod;
        UnityEngine.Debug.Log("세팅완료");
    }

    public void PushButton()
    {
        GameObject go = GameObject.Find("selltimewindow");
        go.GetComponent<SellWindow>().SetCheckdisplay(count, code);
    }

    public void MakerslotPushButton()
    {
        GameObject go = GameObject.Find("makeroom");
        go.GetComponent<MakerUI>().SetMakerBg(count, code);
    }

    //공방용 슬롯데이터도 주고받도록
    //코드,피로도,이름


}
