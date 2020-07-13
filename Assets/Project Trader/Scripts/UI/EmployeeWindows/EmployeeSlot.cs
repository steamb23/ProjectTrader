using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectTrader.Datas;
using ProjectTrader;

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
        state.text = "일당: "+ empData.Cost.ToString();
    }

    //playdata에 추가(고용된 곳->버튼 값을 받아와서 추가
    public void Click()
    {
        if (indata == true && emp.IsWork==false)
        {
            sound.GetComponent<SoundControl>().ButtonSound2();
            emp.IsWork = true;
            //정보가 바꼈으면 바뀐 emp를 다시 playdata에 저장
            //배치될 곳 찾아서 저장하기
            FindObjectOfType<EmployeeDataCatch>().CheckHireEmp(emp, disCode);
            FindObjectOfType<EmployeeDataCatch>().ReSetEmpList(emp);
            //FindObjectOfType<EmployeeDataCatch>().CheckIsWork(emp);
            FindObjectOfType<EmployeeUiManager>().CloseWaiting();
        }
    }

    //배치 위치를 보내주는 함수 0-캐셔,1-청소,2-공방
    public void SetInfo(int c)
    {
        disCode = c;
    }
}
