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
    //정보가 들어가는 위치를 위한 int값
    int disCode;
    //고용상태를 알기 위해 배치를 누르면 무조건적으로 슬롯에 신호를 전달해야 한다
    //슬롯안에 데이터가 들어있는가?
    public bool indata
    {
        set;
        get;
    }

    GameObject sound;
    Employee emp;
    EmployeeData empData;
    GameObject assign;

    void Start()
    {
        sound = GameObject.Find("SoundControler");
    }


    void Update()
    {
        if (indata==false)
        {
            name.text = " ";
            state.text = " ";
        }
    }

    public void SetEmpInfo(Employee emp2)
    {
        emp = emp2;
        empData = emp.GetData();
        name.text = empData.Name;
        state.text = emp2.State;
    }


    public void Click()
    {
        if (indata == true)
        {
            sound.GetComponent<SoundControl>().ButtonSound2();
            assign = GameObject.Find("EmployeeUICanvas");
            assign.GetComponent<EmployeeDataCatch>().AssignDataSet(disCode, emp);
        }
    }

    //배치 위치를 보내주는 함수
    public void SetInfo(int c)
    {
        disCode = c;
        //UnityEngine.Debug.Log("discode 값 : "+disCode.ToString());
    }
}
