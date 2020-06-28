using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using ProjectTrader.Datas;


public class EmployeeDisplayWindow_FireButton : MonoBehaviour
{

    [SerializeField]
    GameObject disemployee;

    [SerializeField]
    EmployeeCount employeeCount;

    [SerializeField]
    EmployeeInfo empinfo;
    Employee emp;
    GameObject savedata;
    GameObject dataCatch;


    void Start()
    {
        savedata = GameObject.Find("SaveData");
        dataCatch = GameObject.Find("EmployeeUICanvas");
        //empinfo = disemployee.GetComponent<EmployeeInfo>();
        if (employeeCount == null)
        {
            Debug.LogError("EmployeeHireWindow_FireButton에 오브젝트가 연결되지 않았습니다.");
        }
    }

    void Update()
    {
        
    }

    public void Click()
    {
        MoveempInfo();
        if (0<employeeCount.Count&&emp.Code>0)
        {
            UnityEngine.Debug.Log("해고누름");
            //해고하고 isWork를 false로 해줘야함
            savedata.GetComponent<DataSave>().FHireEmp(emp,0);
            dataCatch.GetComponent<EmployeeDataCatch>().FireEmployee(emp);
            employeeCount.Count--;

        }
    }

    //info를 employee에 보냄
    void MoveempInfo()
    {
        emp.Code = disemployee.GetComponent<EmployeeInfo>().Code;
        emp.Age = disemployee.GetComponent<EmployeeInfo>().Age;
        emp.State = disemployee.GetComponent<EmployeeInfo>().State;
    }
}
