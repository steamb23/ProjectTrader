using UnityEngine;
using System.Collections;

public class CandiateInfo : EmployeeInfo
{
    public void Disable()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0.5f;
    }

    public void Enable()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        canvasGroup.alpha = 1f;
    }
}
