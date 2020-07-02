using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Threading;
using UnityEngine;
using ProjectTrader.Datas;

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
    //버튼 코드가 1일땐 0~2까지, 2일땐 3~5까지, 3일땐 6에서 8까지 ->배치할때/출력할때 같이 사용
    int slotNum;
    int buttonCode;
    GameObject[] slotInfo;
    GameObject hireData;

    //고용한 직원, 배치한 직원 ->전부 코드로
    Employee[] allemp;
    Employee[] hireemp;
    bool[] isWork;
    //출력용
    Employee printemp;
    EmployeeData pempdata;

    void Start()
    {
        slotNum = 0;
        hireemp = new Employee[9];
        allemp = new Employee[9];
        isWork = new bool[9];
        for (int i = 0; i < 9; i++)
            isWork[i] = false;
    }

    //직원추가/해고시 당겨주는 코드 필요 ->코드로 주고받을수있도록 수정
    public void SetEmpData(Employee emp)
    {
        if (emp==null)
            Debug.LogError("안받아와짐");
        allemp[slotNum] = emp;
        slotNum++;

    }

    //같은 알바생 뽑기 방지용
    public bool CheckSameEmp(int cod)
    {
        if (slotNum == 0)
            return false;
        for(int i = 0; i < slotNum; i++)
        {
            if (allemp[i].Code == cod)
                return true;
        }
        return false;
    }

    //알바대기실이 생성될때 작동! 저장해두고 슬롯에 부여하도록
    public void AddEmp()
    {
        if (slotNum >= 0 && slotNum < 10)
        {
            slotInfo = GameObject.FindGameObjectsWithTag("EMPSlot");
            for(int i = 0; i < 9; i++)
            {
                if (i < slotNum)
                {
                    slotInfo[i].GetComponent<EmployeeSlot>().SetEmpInfo(allemp[i]);
                    slotInfo[i].GetComponent<EmployeeSlot>().indata = true;
                }
                else
                    slotInfo[i].GetComponent<EmployeeSlot>().indata = false;
            }
        }
    }

    //배치하려는 알바생슬롯 데이터에 대기창 슬롯이 연결되도록
    //배치슬롯으로 데이터 연결
    //데이터를 넣는 거랑 출력하는 거랑 코드 구분 - >이거를 저장만해놓고 출력은 배치 윈도우에서 처리할 수 있도록
    public void AssignDataSet(int i,Employee emp)
    {
        if (i == 0)//추가로 슬롯이 일하는지(중복으로 고용하는지) 판별필요
        {
            hireemp[0 + (buttonCode * 3)] = emp;
            isWork[0 + (buttonCode * 3)] = true;

        }
        else if (i == 1)
        {
            hireemp[1 + (buttonCode * 3)] = emp;
            isWork[1 + (buttonCode * 3)] = true;
        }
        else if (i == 2)
        {
            hireemp[2 + (buttonCode * 3)] = emp;
            isWork[2 + (buttonCode * 3)] = true;
        }
        else
            Debug.LogError("일하는 중");
        PrintAssignSlot();
        manager.GetComponent<EmployeeUiManager>().OpenAssignWindow();
        
    }

    public void SetbuttonCode(int cod)
    {
        buttonCode = cod;
    }

    //마지막슬롯에 false전달
    public void FireEmployee(Employee femp)
    {
        for(int i = 0; i < 9; i++)
        {
            if (hireemp[i].Code == femp.Code)
            {
                UnityEngine.Debug.Log("고용된곳에서 해고할 직원 찾음");
                hireemp[i].Code = 0;
                isWork[i] = false;
                slotNum--;
                break;
            }
        }
        for(int i = 0; i < slotNum; i++)
        {
            if (allemp[i].Code == femp.Code)
            {
                UnityEngine.Debug.Log("전체에서 해고할 직원 찾음");
                if (i == slotNum)
                    break;
                for (int j = i; j < allemp.Length - 1; j++)
                    allemp[j] = allemp[j + 1];
                break;
            }
        }
        PrintAssignSlot();
        return;
    }

    //코드로 바꿈
   public void PrintAssignSlot()
    {

        if (isWork[0 + (buttonCode * 3)] == true)
        {
            pempdata = hireemp[0 + (buttonCode * 3)].GetData();
            casher.GetComponent<EmployeeInfo>().Name = pempdata.Name;
            casher.GetComponent<EmployeeInfo>().Age = hireemp[0 + (buttonCode * 3)].Age;
            casher.GetComponent<EmployeeInfo>().Charisma = pempdata.Charisma.ToString();
            casher.GetComponent<EmployeeInfo>().Inteligent = pempdata.Intelligent.ToString();
            casher.GetComponent<EmployeeInfo>().Dexturity = pempdata.Dexterity.ToString();
            casher.GetComponent<EmployeeInfo>().State = hireemp[0 + (buttonCode * 3)].State;
            casher.GetComponent<EmployeeInfo>().Code = hireemp[0 + (buttonCode * 3)].Code;
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
        if (isWork[1 + (buttonCode * 3)] == true)
        {
            pempdata = hireemp[1 + (buttonCode * 3)].GetData();
            cleaner.GetComponent<EmployeeInfo>().Name = pempdata.Name;
            cleaner.GetComponent<EmployeeInfo>().Age = hireemp[1 + (buttonCode * 3)].Age;
            cleaner.GetComponent<EmployeeInfo>().Charisma = pempdata.Charisma.ToString();
            cleaner.GetComponent<EmployeeInfo>().Inteligent = pempdata.Intelligent.ToString();
            cleaner.GetComponent<EmployeeInfo>().Dexturity = pempdata.Dexterity.ToString();
            cleaner.GetComponent<EmployeeInfo>().State = hireemp[1 + (buttonCode * 3)].State;
            cleaner.GetComponent<EmployeeInfo>().Code = hireemp[1 + (buttonCode * 3)].Code;
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
        if (isWork[2 + (buttonCode * 3)] == true)
        {
            pempdata = hireemp[2 + (buttonCode * 3)].GetData();
            crafter.GetComponent<EmployeeInfo>().Name = pempdata.Name;
            crafter.GetComponent<EmployeeInfo>().Age = hireemp[2 + (buttonCode * 3)].Age;
            crafter.GetComponent<EmployeeInfo>().Charisma = pempdata.Charisma.ToString();
            crafter.GetComponent<EmployeeInfo>().Inteligent = pempdata.Intelligent.ToString();
            crafter.GetComponent<EmployeeInfo>().Dexturity = pempdata.Dexterity.ToString();
            crafter.GetComponent<EmployeeInfo>().State = hireemp[2 + (buttonCode * 3)].State;
            crafter.GetComponent<EmployeeInfo>().Code = hireemp[2 + (buttonCode * 3)].Code;
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

}
