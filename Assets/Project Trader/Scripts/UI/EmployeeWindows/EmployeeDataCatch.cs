using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Threading;
using UnityEngine;
using ProjectTrader.Datas;
using ProjectTrader;

public class EmployeeDataCatch : MonoBehaviour
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

    int buttonCode;
    GameObject[] slotInfo;

    //출력용
    Employee printemp;
    EmployeeData pempdata;

    void Start()
    {

    }

    //같은 알바생 뽑기 방지용
    public bool CheckSameEmp(int cod)
    {
        if (PlayData.CurrentData.HiredEmployees.Count < 0)
        {
            return false;
        }
        for(int i = 0; i < PlayData.CurrentData.HiredEmployees.Count; i++)
        {
            if (PlayData.CurrentData.HiredEmployees[i].Code == cod)
                return true;
        }
        return false;
    }

    //알바대기실이 생성될때 작동! 저장해두고 슬롯에 부여하도록->슬롯playdata랑 연결
    public void AddEmp()
    {
        Employee emp;
        if (PlayData.CurrentData.HiredEmployees.Count>0)
        {
            slotInfo = GameObject.FindGameObjectsWithTag("EMPSlot");
            for(int i = 0; i < 9; i++)
            {
                if (i < PlayData.CurrentData.HiredEmployees.Count)
                {
                    emp = PlayData.CurrentData.HiredEmployees[i];
                    slotInfo[i].GetComponent<EmployeeSlot>().SetEmpInfo(emp);
                    slotInfo[i].GetComponent<EmployeeSlot>().indata = true;
                }
                else
                    slotInfo[i].GetComponent<EmployeeSlot>().indata = false;
            }
        }
    }

    public void SetbuttonCode(int cod)
    {
        buttonCode = cod;
    }

    //코드로 바꿈
   public void PrintAssignSlot()
    {
        Employee[] emp=new Employee[3];
        switch (buttonCode)
        {
            case 0:
                emp[0] = PlayData.CurrentData.Cashers[0];
                break;
            case 1:
                emp[0] = PlayData.CurrentData.Cleaners[0];
                break;
            case 2:
                emp[0] = PlayData.CurrentData.Crafter[0];
                emp[1] = PlayData.CurrentData.Crafter[1];
                emp[2] = PlayData.CurrentData.Crafter[2];
                break;
        }

        if (emp[0].IsWork == true)
        {
            pempdata = emp[0].GetData();
            casher.GetComponent<EmployeeInfo>().Name = pempdata.Name;
            casher.GetComponent<EmployeeInfo>().Age = emp[0].Age;
            casher.GetComponent<EmployeeInfo>().Charisma = pempdata.Charisma.ToString();
            casher.GetComponent<EmployeeInfo>().Inteligent = pempdata.Intelligent.ToString();
            casher.GetComponent<EmployeeInfo>().Dexturity = pempdata.Dexterity.ToString();
            casher.GetComponent<EmployeeInfo>().State = emp[0].State;
            casher.GetComponent<EmployeeInfo>().Code = emp[0].Code;
        }
        else
        {

            casher.GetComponent<EmployeeInfo>().Name = " ";
            casher.GetComponent<EmployeeInfo>().Age = " ";
            casher.GetComponent<EmployeeInfo>().Charisma = " ";
            casher.GetComponent<EmployeeInfo>().Inteligent = " ";
            casher.GetComponent<EmployeeInfo>().Dexturity = " ";
            casher.GetComponent<EmployeeInfo>().State = " ";
            casher.GetComponent<EmployeeInfo>().Code = 0;
        }
        if (emp[1].IsWork == true)
        {
            pempdata = emp[1].GetData();
            cleaner.GetComponent<EmployeeInfo>().Name = pempdata.Name;
            cleaner.GetComponent<EmployeeInfo>().Age = emp[1].Age;
            cleaner.GetComponent<EmployeeInfo>().Charisma = pempdata.Charisma.ToString();
            cleaner.GetComponent<EmployeeInfo>().Inteligent = pempdata.Intelligent.ToString();
            cleaner.GetComponent<EmployeeInfo>().Dexturity = pempdata.Dexterity.ToString();
            cleaner.GetComponent<EmployeeInfo>().State = emp[1].State;
            cleaner.GetComponent<EmployeeInfo>().Code = emp[1].Code;
        }
        else
        {
            cleaner.GetComponent<EmployeeInfo>().Name = " ";
            cleaner.GetComponent<EmployeeInfo>().Age = " ";
            cleaner.GetComponent<EmployeeInfo>().Charisma = " ";
            cleaner.GetComponent<EmployeeInfo>().Inteligent = " ";
            cleaner.GetComponent<EmployeeInfo>().Dexturity = " ";
            cleaner.GetComponent<EmployeeInfo>().State = " ";
            cleaner.GetComponent<EmployeeInfo>().Code = 0;
        }
        if (emp[2].IsWork == true)
        {
            pempdata = emp[2].GetData();
            crafter.GetComponent<EmployeeInfo>().Name = pempdata.Name;
            crafter.GetComponent<EmployeeInfo>().Age = emp[2].Age;
            crafter.GetComponent<EmployeeInfo>().Charisma = pempdata.Charisma.ToString();
            crafter.GetComponent<EmployeeInfo>().Inteligent = pempdata.Intelligent.ToString();
            crafter.GetComponent<EmployeeInfo>().Dexturity = pempdata.Dexterity.ToString();
            crafter.GetComponent<EmployeeInfo>().State = emp[2].State;
            crafter.GetComponent<EmployeeInfo>().Code = emp[2].Code;
        }
        else
        {
            crafter.GetComponent<EmployeeInfo>().Name = " ";
            crafter.GetComponent<EmployeeInfo>().Age = " ";
            crafter.GetComponent<EmployeeInfo>().Charisma = " ";
            crafter.GetComponent<EmployeeInfo>().Inteligent = " ";
            crafter.GetComponent<EmployeeInfo>().Dexturity = " ";
            crafter.GetComponent<EmployeeInfo>().State = " ";
            crafter.GetComponent<EmployeeInfo>().Code = 0;
        }
    }

    //이전에 고용된 직원이 있는지/ 있는경우 제거, 없는 경우 추가만 하고 넘어감/직원-배치된 곳 코드(discod는 슬롯 위치!!
    public void CheckHireEmp(Employee emp, int discod)
    {
        switch (buttonCode)
        {
            case 0:
                if (PlayData.CurrentData.Cashers[0].Code == null)
                {
                    PlayData.CurrentData.Cashers[0] = emp;
                }
                else
                {
                    CheckIsWork(PlayData.CurrentData.Cashers[0]);
                    PlayData.CurrentData.Cashers[0] = emp;
                }
                break;
            case 1:
                if (PlayData.CurrentData.Cleaners[0].Code == null)
                {
                    PlayData.CurrentData.Cleaners[0] = emp;
                }
                else
                {
                    CheckIsWork(PlayData.CurrentData.Cleaners[0]);
                    PlayData.CurrentData.Cleaners[0] = emp;
                }
                    break;
            case 2:
                for(int i = 0; i < 3; i++)
                {
                    if(PlayData.CurrentData.Crafter[discod].Code == null)
                    {
                        PlayData.CurrentData.Crafter[discod] = emp;
                    }
                    else
                    {
                        CheckIsWork(PlayData.CurrentData.Crafter[discod]);
                        PlayData.CurrentData.Crafter[discod] = emp;
                    }
                }
                break;
        }
        PrintAssignSlot();
    }

    //배치 바꾸는 과정에서->고용된 직원을 hiredemployees에서 찾아 iswork를 1로
    public void CheckIsWork(Employee emp)
    {
        for(int i = 0; i<PlayData.CurrentData.HiredEmployees.Count; i++)
        {
            Employee listemp = PlayData.CurrentData.HiredEmployees[i];
            if (emp.Code == listemp.Code)
            {
                listemp.IsWork = false;
                PlayData.CurrentData.HiredEmployees[i] = listemp;
                return;
            }
        }
    }

    //해고용
    public void FireEmployee(Employee emp)
    {
        switch (buttonCode)
        {
            case 0:
                PlayData.CurrentData.Cashers[0].IsWork = false;
                break;
            case 1:
                PlayData.CurrentData.Cleaners[0].IsWork = false;
                break;
            case 2:
                for(int i = 0; i < 3; i++)
                {
                    if (PlayData.CurrentData.Crafter[i].Code == emp.Code)
                    {
                        PlayData.CurrentData.Crafter[i].IsWork = false;
                    }
                }
                break;
        }
        PrintAssignSlot();
    }

    //hire에 slot내에서 바뀐 것이 적용되도록

    public void ReSetEmpList(Employee emp)
    {
        for(int i = 0; i < PlayData.CurrentData.HiredEmployees.Count; i++)
        {
            if (PlayData.CurrentData.HiredEmployees[i].Code == emp.Code)
            {
                PlayData.CurrentData.HiredEmployees[i] = emp;
                return;
            }
        }
    }
}
