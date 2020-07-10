using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TownShopTutorial : MonoBehaviour
{
    GameObject movetown;
    [SerializeField]
    GameObject shopwindow;
    [SerializeField]
    GameObject shoppopupbutton;
    [SerializeField]
    GameObject makerwindow;
    [SerializeField]
    GameObject makerpopup;

    GameObject[] shopslot;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void MoveTown(int num)
    {

        if (num == 9)
        {
            movetown = GameObject.Find("GoToTownClick");
            movetown.GetComponent<GoToTownClick>().Click();
        }
        if (num == 17)
        {
            movetown = GameObject.Find("GoToShopClick");
            movetown.GetComponent<GoToShopClick>().Click();
        }
    }
    public void ShopWindowTutorial(int num)
    {
        switch (num)
        {
            case 10:
                shopwindow.GetComponent<ShopWindow>().OpenShopWindow();
                break;
            case 11:
                shopslot = GameObject.FindGameObjectsWithTag("Slot");
                shopslot[0].GetComponent<SlotIn>().MakeShopPopup();
                break;
            case 12:
                shoppopupbutton.GetComponent<MakePopScript>().SetBuyItem();
                shoppopupbutton.GetComponent<MakePopScript>().CloseShopPopup();
                shopwindow.GetComponent<ShopWindow>().CloseShopWindow();
                break;
        }
    }

    public void MakerWindowTutorial(int num)
    {
        switch (num)
        {
            case 13:
                makerwindow.GetComponent<MakerUI>().OpenMakeRoom();
                break;
            case 14://슬롯누르기
                shopslot = GameObject.FindGameObjectsWithTag("Slot");
                shopslot[0].GetComponent<MakeSlot>().MakerslotPushButton();
                break;
            case 15://팝업생성
                makerwindow.GetComponent<MakerUI>().CreateMakePopup();
                break;
            case 16:
                makerpopup.GetComponent<MakePopup>().SetMakeItem();
                makerwindow.GetComponent<MakerUI>().CloseMakeRoom();
                break;

        }
    }
}
