using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeDataCatch :EmployeeInfo
{
    [SerializeField]
    GameObject casher;
    [SerializeField]
    GameObject cleaner;
    [SerializeField]
    GameObject crafter;
    [SerializeField]
    GameObject manager;

    //나중에 시작하면 데이터를 불러와서 추가
    public int slotNum = 0;
    public GameObject[] slotInfo;
    public GameObject hireData;
    EmployeeInfo[] employee;



    void Start()
    {
        employee = new EmployeeInfo[9];
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (employee != null)
            {
                Debug.LogError("받아오기 성공");
            }
        }
    }

    public void SetEmpData(CandiateInfo emp)
    {
        if(emp==null)
            Debug.LogError("안받아와짐");
        employee[slotNum] = emp;
        UnityEngine.Debug.Log(employee[slotNum].Name);
        slotNum++;

    }

    //알바대기실이 생성될때 작동! 저장해두고 슬롯에 부여하도록
    public void AddEmp()
    {
        if (slotNum >= 0 && slotNum < 10)
        {
            slotInfo = GameObject.FindGameObjectsWithTag("EMPSlot");
            for(int i = 0; i < slotNum; i++)
            {
                slotInfo[i].GetComponent<EmployeeSlot>().SetEmpInfo(employee[i]);
                //UnityEngine.Debug.Log(i.ToString());
                //Debug.LogError("넣으려고는했다");
            }
        }
    }
    //배치하려는 알바생슬롯 데이터에 대기창 슬롯이 연결되도록

    public void AssignDataSet(int i, EmployeeInfo emp)
    {
        if (i == 0)
        {
            casher.GetComponent<EmployeeInfo>().Name= emp.Name;
            casher.GetComponent<EmployeeInfo>().Age = emp.Age;
            casher.GetComponent<EmployeeInfo>().Charisma = emp.Charisma;
            casher.GetComponent<EmployeeInfo>().Inteligent = emp.Inteligent;
            casher.GetComponent<EmployeeInfo>().Dexturity = emp.Dexturity;
            casher.GetComponent<EmployeeInfo>().State = emp.State;
        }
        else if (i == 1)
        {
            cleaner.GetComponent<EmployeeInfo>().Name = emp.Name;
            cleaner.GetComponent<EmployeeInfo>().Age = emp.Age;
            cleaner.GetComponent<EmployeeInfo>().Charisma = emp.Charisma;
            cleaner.GetComponent<EmployeeInfo>().Inteligent = emp.Inteligent;
            cleaner.GetComponent<EmployeeInfo>().Dexturity = emp.Dexturity;
            cleaner.GetComponent<EmployeeInfo>().State = emp.State;
        }
        else if (i == 2)
        {
            crafter.GetComponent<EmployeeInfo>().Name = emp.Name;
            crafter.GetComponent<EmployeeInfo>().Age = emp.Age;
            crafter.GetComponent<EmployeeInfo>().Charisma = emp.Charisma;
            crafter.GetComponent<EmployeeInfo>().Inteligent = emp.Inteligent;
            crafter.GetComponent<EmployeeInfo>().Dexturity = emp.Dexturity;
            crafter.GetComponent<EmployeeInfo>().State = emp.State;
        }
        manager.GetComponent<EmployeeUiManager>().OpenAssignWindow();
    }
}
