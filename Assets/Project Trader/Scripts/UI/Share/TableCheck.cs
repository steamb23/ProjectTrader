using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCheck : MonoBehaviour
{
    //string tablename;
    GameObject[] table;
    GameObject[] item;
    GameObject displayWindow;
    GameObject makeWindow;
    GameObject itemShop;
    public GameObject choiceTable
    {
        set;
        get;
    }

    void Start()
    {
        displayWindow = GameObject.Find("selltimewindow");
        makeWindow = GameObject.Find("makeroom");
        itemShop = GameObject.Find("itemshop");
    }


    /**************************************
     * 클릭 관련 기능은 각각
     * ClickableObjects/ItemClick.cs
     * ClickableObjects/WorkShopClick.cs
     * 를 참조할것
     **************************************/

    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        CheckingMouse();
    //    }
    //}
    //void CheckingMouse()
    //{

    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);
    //    Ray2D ray = new Ray2D(clickPos, Vector2.zero);
    //    RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
    //    if (rayHit.collider != null)
    //    {
    //        if (rayHit.collider.gameObject.tag == "ItemTable")
    //        {
    //            choiceTable=SetDisplayTable(rayHit.collider.gameObject);
    //            CreateDisplayWindow();
    //        }
    //        if (rayHit.collider.gameObject.tag == "Makeroom")
    //        {
    //            CreateMakeRoom();
    //        }
    //        if (rayHit.collider.gameObject.tag == "ItemShop")
    //        {
    //            CreateItemShop();
    //        }
    //    }

    //}

    GameObject SetDisplayTable(GameObject hitTable)
    {
        int i;
        table = GameObject.FindGameObjectsWithTag("ItemTable");
        item = GameObject.FindGameObjectsWithTag("Item");
        for (i = 0; i < table.Length; i++)
        {
            if (table[i] == hitTable)
            {

                return item[i];
            }
        }
        return item[i];
    }

    //여기서 배치 ui생성필수

    void CreateDisplayWindow()
    {
        displayWindow.GetComponent<SellWindow>().OpenMakerWindow();
    }

    void CreateMakeRoom()
    {
        makeWindow.GetComponent<MakerUI>().OpenMakeRoom();
    }

    void CreateItemShop()
    {
        itemShop.GetComponent<ShopWindow>().OpenShopWindow();
    }
}
