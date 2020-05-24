using UnityEngine;
using System.Collections;

public class EmployeeHireWindow_HireButton : MonoBehaviour
{
    [SerializeField]
    CandiateInfo candiateInfo;
    [SerializeField]
    EmployeeCount employeeCount;

    private void Start()
    {
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
            candiateInfo.Disable();
            employeeCount.Count++;
        }
    }
}
