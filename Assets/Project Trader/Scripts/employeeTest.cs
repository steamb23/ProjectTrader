using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;

public class employeeTest : MonoBehaviour
{
    Employee empdata;
    Item ap;
    ItemData itemm;
    EmployeeData abc;

    int i = 0;
    void Start()
    {


    }

    void Update()
    {
        if (i == 0)
            Test();
    }

    void Test()
    {
        ap.Code = 2;
        empdata = 2;
        itemm = ap.GetData();
        UnityEngine.Debug.Log(itemm.Name);
        abc = empdata.GetData();
        UnityEngine.Debug.Log(abc.Name);
        i++;
    }
}
