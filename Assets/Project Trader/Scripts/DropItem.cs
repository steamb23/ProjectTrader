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
                FindVisitor(inCoin, 1);
            }
        }
    }

    void TrashChance()
    {
        if (inTrash == false)
        {
            int tg = UnityEngine.Random.Range(0, 11);
            //UnityEngine.Debug.Log(tg);
            if (tg < 2)
            {
                FindVisitor(inTrash, 2);
            }
        }
    }
    void FindVisitor(bool item, int code)
    {

        visitor = GameObject.FindGameObjectsWithTag("Visitor");
        if (visitor == null && trashNum >= 5)
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
                else if (code == 2)
                {
                    UnityEngine.Debug.Log("쓰레기만들기 호출");
                    CreateTrash(visitor[randVisitor]);

                }
                visitor = null;
            }
            else
            {
                if (code == 1)
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
            if (mouseOb.name == "Dropcoin(Clone)")
            {
                GetCoin();
                Destroy(coin);
                inCoin = false;

            }
            if (mouseOb.name == "cup_trash(Clone)")
            {
                CollectTrash(mouseOb);
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

    public GameObject FindTrash()
    {
        for (int i = 0; i < 5; i++)
        {
            if (trash[i] != null)
            {
                return trash[i];
            }
        }
        return null;
    }

    public GameObject[] FindTrashes()
    {
        List<GameObject> results = new List<GameObject>(trash.Length);
        for (int i = 0; i < 5; i++)
        {
            if (trash[i] != null)
            {
                results.Add(trash[i]);
            }
        }

        return results.ToArray();
    }

    //반복문으로 빈 배열 검사해서 그곳에 새로 쓰레기 만들기-> trashNum으로 만드는게 아니라 반복문i값으로 만들어지게 수정
    public void CreateTrash(GameObject obj)
    {
        if (inTrash == false)
        {
            inTrash = true;
            for (int i = 0; i < 5; i++)
            {
                if (trash[i] == null)
                {
                    trash[i] = Instantiate(trashItem) as GameObject;
                    trash[i].transform.position = obj.transform.position;
                    trashNum++;
                    break;
                }
            }
            if (FindTrashes().Length >= 5)
                inTrash = true;
            else
                inTrash = false;
        }
    }

    /// <summary>
    /// 쓰레기를 수거합니다.
    /// </summary>
    /// <param name="gameObject">수거할 쓰레기의 게임 오브젝트</param>
    public FloatingTimer CollectTrash(GameObject gameObject, float collectingTime = 5, Action<FloatingTimer> timeoutCallback = null)
    {
        // 이 스크립트에서 관리중인 오브젝트인지 체크
        bool isTrash = false;
        for (int i = 0; i < 5; i++)
        {
            if (trash[i] == gameObject)
            {
                // 관리중인 오브젝트에서 빼버림
                trash[i] = null;
                isTrash = true;
                break;
            }
        }

        if (isTrash)
        {
            // 타이머 표시
            if (timeoutCallback == null)
            {
                timeoutCallback = (timer) =>
                {
                    if (gameObject != null)
                        Destroy(gameObject);
                    timer.FadeoutWithDestory();
                };
            }
            return FloatingTimer.Create(gameObject.transform, new Vector3(0f, -0.1f, 0), collectingTime, timeoutCallback);
        }
        else
        {
            UnityEngine.Debug.LogError($"{gameObject}는 DropItem 스크립트에서 관리되는 쓰레기 오브젝트가 아닙니다.");
        }

        return null;
    }
}