using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectTrader.Datas;

public class EmployeeSlot :EmployeeInfo
{
    [SerializeField]
    TextMeshProUGUI name;
    [SerializeField]
    TextMeshProUGUI state;
    int disCode;
    bool working = false; //일하고 있는가? > 배치되면 true로 추가고용이 불가하도록 한다.
    EmployeeInfo emp;
    GameObject assign;

    void Start()
    {
        
    }


    void Update()
    {
        if (emp == null)
        {
            name.text = " ";
            state.text = " ";
        }
    }
    public void SetEmpInfo(EmployeeInfo hireemployee)
    {
        emp = hireemployee;
        name.text = emp.Name;
        state.text = emp.State;

    }


    public void Click()
    {
        assign=GameObject.Find("EmployeeUICanvas");
        assign.GetComponent<EmployeeDataCatch>().AssignDataSet(disCode, emp);
    }

    //
    public void SetInfo(int c)
    {
        disCode = c;
        //UnityEngine.Debug.Log("discode 값 : "+disCode.ToString());
    }
}
