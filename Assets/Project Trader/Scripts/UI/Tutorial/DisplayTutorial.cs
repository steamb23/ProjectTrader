using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTutorial : MonoBehaviour
{
    GameObject itemtable;
    [SerializeField]
    GameObject displaywindow;
    [SerializeField]
    GameObject displaypopup;
    GameObject[] shopslot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTutorialSet(int num)
    {
        switch (num)
        {
            case 18:
                itemtable = GameObject.Find("ItemClick 0");
                itemtable.GetComponent<ItemClick>().Click();
                //displaywindow.GetComponent<SellWindow>().OpenMakerWindow();
                break;
            case 19:
                shopslot = GameObject.FindGameObjectsWithTag("Slot");
                shopslot[0].GetComponent<SlotIn>().PushButton();
                break;
            case 20://팝업생성
                displaywindow.GetComponent<SellWindow>().SetPopUpWindow();
                break;
            case 21:
                displaypopup.GetComponent<MakePopScript>().SetItem();
                displaywindow.GetComponent<SellWindow>().CloseMakerWindow();
                break;
            default:
                break;
        }
    }
}
