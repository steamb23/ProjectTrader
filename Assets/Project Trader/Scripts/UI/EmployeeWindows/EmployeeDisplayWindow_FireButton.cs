using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeDisplayWindow_FireButton : MonoBehaviour
{
    [SerializeField]
    CandiateInfo candiateInfo;
    [SerializeField]
    EmployeeCount employeeCount;
    GameObject savedata;
    // Start is called before the first frame update
    void Start()
    {
        savedata = GameObject.Find("SaveData");
        if (candiateInfo == null)
        {
            candiateInfo = GetComponentInParent<CandiateInfo>();
            if (candiateInfo == null)
                Debug.LogError("EmployeeHireWindow_FireButton가 초기화 되지 않았습니다.");
        }
        if (employeeCount == null)
        {
            Debug.LogError("EmployeeHireWindow_FireButton에 오브젝트가 연결되지 않았습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (0<employeeCount.Count)
        {
            candiateInfo.Disable();
            savedata.GetComponent<DataSave>().FHireEmp(candiateInfo.GetComponent<EmployeeInfo>().Name,0);
            employeeCount.Count--;
            Debug.LogError("정상작동");
        }
    }
}
