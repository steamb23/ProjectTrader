using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EmpButtonGroup : MonoBehaviour
{
    [SerializeField]
    Sprite select;
    [SerializeField]
    Sprite nonSelect;
    [SerializeField]
    Image assignbutton;
    [SerializeField]
    Image assign;
    [SerializeField]
    Image hirebutton;
    [SerializeField]
    Image hire;
    [SerializeField]
    GameObject assignwindow;
    [SerializeField]
    GameObject hirewindow;
    bool hireb;
    bool assignb;
    // Start is called before the first frame update
    void Start()
    {
        hireb = false;
        assignb = false;
    }

    public void AssignButtonOn()
    {
        if (hirewindow.activeSelf == false&&assignb==false)
        {
            assignb = true;
            hireb = false;
            assignbutton.sprite = select;
            hirebutton.sprite = nonSelect;
            CanvasGroupControlOn(hire, hirebutton, assign, assignbutton);
            CanvasGroupControlOff(assign);
        }

    }

    public void HireButtonOn()
    {
        if (assignwindow.activeSelf == false&&hireb==false)
        {
            hireb = true;
            assignb = false;
            hirebutton.sprite = select;
            assignbutton.sprite = nonSelect;
            CanvasGroupControlOn(assign, assignbutton, hire, hirebutton);
            CanvasGroupControlOff(hire);
        }
    }

    void CanvasGroupControlOn(Image abc,Image def,Image ghi,Image jkl)//켜지는거(선택
    {
        var canvasGroup = abc.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.alpha = 0.5f;
        RectTransform rt = ghi.GetComponent<RectTransform>();
        RectTransform bg = abc.GetComponent<RectTransform>();
        bg.anchoredPosition = new Vector3(bg.anchoredPosition.x + 3, bg.anchoredPosition.y);
        rt.anchoredPosition = new Vector3(bg.anchoredPosition.x-3, rt.anchoredPosition.y);

        rt =jkl.GetComponent<RectTransform>();
        bg = def.GetComponent<RectTransform>();
        bg.anchoredPosition = new Vector3(bg.anchoredPosition.x + 8, bg.anchoredPosition.y);
        rt.anchoredPosition = new Vector3(bg.anchoredPosition.x - 8, rt.anchoredPosition.y);

    }

    void CanvasGroupControlOff(Image abc)//꺼지는거(비선택
    {
        var canvasGroup = abc.GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.alpha = 1.0f;

    }
}
