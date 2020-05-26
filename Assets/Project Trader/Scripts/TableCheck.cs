using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCheck : MonoBehaviour
{
    //string tablename;
    GameObject[] table;
    GameObject[] item;
    GameObject displayWindow;

    public GameObject choiceTable
    {
        set;
        get;
    }

    void Start()
    {
        displayWindow = GameObject.Find("selltimewindow");
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckingMouse();
        }
    }
    void CheckingMouse()
    {

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(clickPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
        if (rayHit.collider != null)
        {
            if (rayHit.collider.gameObject.tag == "ItemTable")
            {
                //tablename = rayHit.collider.gameObject.name;
                //UnityEngine.Debug.Log("djfwlejfklsdjfklsjdf");
                choiceTable=SetDisplayTable(rayHit.collider.gameObject);
                CreateDisplayWindow();
            }
        }

    }

    //해당 오브젝트를 반환하여 사용?
    GameObject SetDisplayTable(GameObject hitTable)
    {
        int i;
        table = GameObject.FindGameObjectsWithTag("ItemTable");
        item = GameObject.FindGameObjectsWithTag("Item");
        for (i = 0; i < table.Length; i++)
        {
            if (table[i] == hitTable)
            {

                return item[i+1];
            }
        }
        return item[i];
    }

    //여기서 배치 ui생성필수

    void CreateDisplayWindow()
    {
        displayWindow.GetComponent<MakerWindow>().OpenMakerWindow();
    }
}
