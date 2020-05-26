using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotIn : MonoBehaviour
{
    public int count;
    public int code;
    public void SetSlotInData(int cunt, int cod)
    {
        count = cunt;
        code = cod;
    }

    public void PushButton()
    {
        GameObject go = GameObject.Find("selltimewindow");
        go.GetComponent<MakerWindow>().SetCheckdisplay(count, code);
    }
}
