using UnityEngine;
using System.Collections;

/// <summary>
/// 워크샵 윈도우를 엽니다.
/// </summary>
public class WorkshopClick : ClickableObject
{
    public override void Click()
    {
        var makerUI = FindObjectOfType<MakerUI>();
        makerUI.OpenMakeRoom();
    }
}
