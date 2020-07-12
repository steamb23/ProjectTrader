using UnityEngine;
using System.Collections;
using System;
using ProjectTrader;
using ProjectTrader.Datas;
using System.Runtime.CompilerServices;

public class EmployeeHireWindow : MonoBehaviour
{
    [SerializeField]
    GameObject candiateInfo0;
    [SerializeField]
    GameObject candiateInfo1;
    [SerializeField]
    GameObject candiateInfo2;
    [SerializeField]
    GameObject empui;

    EmployeeData empdata;
    Employee emp;

    bool isInitialized = false;
    int[] mecod;
    int i = 0;
    // Use this for initialization
    void Start()
    {
        mecod = new int[3];
        if (candiateInfo0 == null ||
            candiateInfo1 == null ||
            candiateInfo2 == null)
        {
            Debug.LogError("EmployeeHireWindow에 오브젝트가 연결되어 있지 않습니다.");
        }
        else
        {
            if (!isInitialized)
            {
                RenewCandiateList();
                isInitialized = true;
            }
        }
    }


    public void RenewCandiateList()
    {


        var candiateInfo0 = this.candiateInfo0.GetComponent<CandiateInfo>();
        var candiateInfo1 = this.candiateInfo1.GetComponent<CandiateInfo>();
        var candiateInfo2 = this.candiateInfo2.GetComponent<CandiateInfo>();

        SetData(candiateInfo0);
        SetData(candiateInfo1);
        SetData(candiateInfo2);

        void SetData(CandiateInfo candiateInfo)
        {
            int cod;
            do
            {
                cod = UnityEngine.Random.Range(1, 19);

                //고용된 리스트에서 찾아서 구별
                if (empui.GetComponent<EmployeeDataCatch>().CheckSameEmp(cod) == false && Checkslot(cod)==true)
                {
                    break;
                }


            } while (true);
            emp.Code = cod;
            empdata = emp.GetData();
            // 임시 데이터
            candiateInfo.Code = cod;
            candiateInfo.Name = empdata.Name;
            candiateInfo.Age = $"{(UnityEngine.Random.Range(0, 2) > 0 ? "남" : "여")} / {UnityEngine.Random.Range(20, 40)}";
            candiateInfo.Charisma = empdata.Charisma.ToString();
            candiateInfo.Inteligent = empdata.Intelligent.ToString();
            candiateInfo.Dexturity = empdata.Dexterity.ToString();
            candiateInfo.State = UnityEngine.Random.Range(0, 100) > 80 ?
                "무경력" :
                $"{(UnityEngine.Random.Range(0, 2) > 0 ? "정규직" : "비정규직")} {UnityEngine.Random.Range(1, 10)}년";
            //State는 일단 냅두기
            candiateInfo.Enable();
        }
    }
    bool Checkslot(int cod)
    {
        if (i == 0)
        {
            mecod[0] = cod;
            i++;
            return true;
        }
        if (i == 1)
        {
            if (mecod[0] == cod)
                return false;
            mecod[1] = cod;
            i++;
            return true;
        }
        if (i == 2)
        {
            if (mecod[0] == cod)
                return false;
            if (mecod[1] == cod)
                return false;
            i = 0;
            return true;
        }
        return false;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
