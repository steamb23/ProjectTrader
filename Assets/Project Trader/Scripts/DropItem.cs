using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    GameObject[] visitor;
    public GameObject dropItem;
    public GameObject trashItem;

    GameObject datasave;
    GameObject mouseOb;
    GameObject coin;
    GameObject[] trash;

    bool inCoin = false;
    bool inTrash = false;
    int trashNum = 0;

    //enum값으로 변경예정 1이면 코인, 2면 쓰레기 생성

    int coAndTr = 0;


    void Start()
    {
        datasave = GameObject.Find("SaveData");
        if (datasave == null)
            UnityEngine.Debug.Log("datasave error");
        trash = new GameObject[5];
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CheckMouse();

        }

        int ch = UnityEngine.Random.Range(0, 1000);
        if (ch < 10)
            TrashChance();


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
                FindVisitor(inCoin,1);
            }
        }
    }

    void TrashChance()
    {
        if (inTrash == false)
        {
            int tg = UnityEngine.Random.Range(0, 11);
            UnityEngine.Debug.Log(tg);
            if (tg <2)
            {
                FindVisitor(inTrash,2);
            }
        }
    }
    void FindVisitor(bool item,int code)
    {

        visitor = GameObject.FindGameObjectsWithTag("Visitor");
        if (visitor == null && trashNum>=5)
            item = false;

        else
        {
            int randVisitor = UnityEngine.Random.Range(0, visitor.Length + 1);
            if (randVisitor < visitor.Length)
            {
                if (code == 1)
                {
                    CreateCoin(visitor[randVisitor]);
                }
                else if(code==2)
                {
                    UnityEngine.Debug.Log("쓰레기만들기 호출");
                    CreateTrash(visitor[randVisitor]);

                }
                visitor = null;
            }
            else
            {
                if(code==1)
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
            if(mouseOb.name=="cup_trash(Clone)")
            {
                Destroy(mouseOb);
                CleanUp();
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

    void CleanUp()
    {
        trashNum--;
        inTrash = false;
        //ui연결해서 처리
    }

    public void GetCoin()
    {
        datasave.GetComponent<DataSave>().UseMoney(1);
    }

    //반복문으로 빈 배열 검사해서 그곳에 새로 쓰레기 만들기-> trashNum으로 만드는게 아니라 반복문i값으로 만들어지게 수정
    void CreateTrash(GameObject obj)
    {
        if (inTrash == false)
        {
            inTrash = true;
            for (int i = 0; i < 5; i++)
            {
                if (trash[i] == null)
                {
                    trash[trashNum] = Instantiate(trashItem) as GameObject;
                    trash[trashNum].transform.position = obj.transform.position;
                    trashNum++;
                    break;
                }
            }
            if (trashNum >= 5)
                inTrash = true;
            else
                inTrash = false;
        }
    }
}