using UnityEngine;
using System.Collections;
using ProjectTrader.Datas;
using ProjectTrader;

public class EmployeeHireWindow_HireButton : MonoBehaviour
{
    [SerializeField]
    CandiateInfo candiateInfo;
    [SerializeField]
    EmployeeCount employeeCount;
    GameObject savedata;
    [SerializeField]
    GameObject empcatch;
    Employee emp;
    EmployeeData empdata;
    private void Start()
    {
        //empcatch = GameObject.Find("EmployeeUICanvas");
        savedata = GameObject.Find("SaveData");
        if (candiateInfo == null)
        {
            candiateInfo = GetComponentInParent<CandiateInfo>();
            if (candiateInfo == null)
                Debug.LogError("EmployeeHireWindow_HireButton가 초기화 되지 않았습니다.");
        }
        if (employeeCount == null)
        {
            Debug.LogError("EmployeeHireWindow_HireButton에 오브젝트가 연결되지 않았습니다.");
        }
    }

    public void Click()
    {
        // 실제 구현시 직원 수는 데이터 관리하는 클래스 or 스크립트에서 관리할 예정
        // 이하 임시 구현
        if (employeeCount.Count < employeeCount.MaxCount)
        {
            emp.Code = candiateInfo.Code;
            emp.State = candiateInfo.State;
            emp.Age = candiateInfo.Age;
            emp.IsWork = false;
            empdata = emp.GetData();

            if (PlayData.CurrentData.Money - empdata.Cost >= 0)
            {
                PlayData.CurrentData.Money -= empdata.Cost;
                FindObjectOfType<DataSave>().EmpAdd(emp);

                ProjectTrader.QuestManager.Trigger(QuestData.GoalType.HireEmployee, 1);

                //savedata.GetComponent<DataSave>().FHireEmp(emp,1); //필요없음
                //empcatch.GetComponent<EmployeeDataCatch>().SetEmpData(emp);
                candiateInfo.Disable();
                employeeCount.Count++;

            }
            else
            {
                FindObjectOfType<TextUiControl>().CreativeTextBox(0, 0, 50, "알바생을 고용할 수 없습니다.", 2);
            }
            // 퀘스트 트리거


        }
    }
}
