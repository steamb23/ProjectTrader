using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    public float chance;
    GameObject[] visitor;
    public GameObject dropItem;
    GameObject datasave;
    GameObject mouseOb;
    GameObject coin;
    bool inCoin = false;
    // Start is called before the first frame update
    void Start()
    {
        datasave = GameObject.Find("SaveData");
        if (datasave == null)
            UnityEngine.Debug.Log("datasave error");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CheckMouse();

        }
        /* 동전
        int ch = UnityEngine.Random.Range(0, 1000);
        if (ch < 10)
            CoinChance();
        */

    }

    void CoinChance()
    {
        if (inCoin == false)
        {
            int cg = UnityEngine.Random.Range(0, 11);
            UnityEngine.Debug.Log(cg);
            if (cg == 0)
            {
                inCoin = true;
                FindVisitor();
            }
        }
    }
    void FindVisitor()
    {

        visitor = GameObject.FindGameObjectsWithTag("Visitor");
        if (visitor == null)
            inCoin = false;

        else
        {
            int randVisitor = UnityEngine.Random.Range(0, visitor.Length+1);
            if (randVisitor < visitor.Length)
            {
                CreateCoin(visitor[randVisitor]);
                visitor = null;
            }
            else
            {
                inCoin = false;
            }
        }
    }

    void CheckMouse()
    {
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D obmouseClick = Physics2D.Raycast(clickPos, Camera.main.transform.forward);
        if (obmouseClick.collider != null)
        {
            mouseOb = obmouseClick.transform.gameObject;
            if (mouseOb.name =="Dropcoin(Clone)")
            {
                GetCoin();
                Destroy(coin);
                inCoin = false;
                
            }
        }
    }
    void CreateCoin(GameObject abc)
    {
        if (inCoin == true)
        {
            coin = Instantiate(dropItem) as GameObject;
            coin.transform.position = abc.transform.position;
        }

    }

    public void GetCoin()
    {
        datasave.GetComponent<DataSave>().UseMoney(1);
    }

}
