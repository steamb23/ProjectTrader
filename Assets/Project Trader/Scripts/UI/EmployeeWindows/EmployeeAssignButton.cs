using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeAssignButton : MonoBehaviour
{
    [SerializeField]
    GameObject data;

    [SerializeField]
    GameObject slot1;

    [SerializeField]
    GameObject slot2;

    public int buttonCode
    {
        set;
        get;
    }

    void Start()
    {
        buttonCode = 0;
    }


    void Update()
    {
        
    }

    public void PushCleanerButton()
    {
        buttonCode = 0;
        SetPrint();
    }

    public void PushCasherButton()
    {
        buttonCode = 1;
        SetPrint();
    }

    public void PushCrafterButton()
    {
        buttonCode = 2;
        SetPrint();
    }

    //버튼 누른뒤 assignwindow로 바로 값을 전달(버튼값을 저장할 변수에 직접 전달?)하여 출력하는 값이 달라지도록

    void SetPrint()
    {
        if(buttonCode==0 || buttonCode == 1)
        {
            var canvasGroup = slot1.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.5f;
            canvasGroup = slot2.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.5f;
        }
        else
        {
            var canvasGroup = slot1.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1.0f;
            canvasGroup = slot2.GetComponent<CanvasGroup>();
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1.0f;
        }
        data.GetComponent<EmployeeDataCatch>().SetbuttonCode(buttonCode);
        data.GetComponent<EmployeeDataCatch>().PrintAssignSlot();
    }

}
