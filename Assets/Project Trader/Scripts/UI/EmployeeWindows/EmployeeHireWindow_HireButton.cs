using UnityEngine;
using System.Collections;

public class EmployeeHireWindow_HireButton : MonoBehaviour
{
    [SerializeField]
    CandiateInfo candiateInfo;

    private void Start()
    {
        if (candiateInfo == null)
        {
            candiateInfo = GetComponentInParent<CandiateInfo>();
            if (candiateInfo == null)
                Debug.LogError("EmployeeHireWindow_HireButton가 초기화 되지 않았습니다.");
        }
    }

    public void Click()
    {
        candiateInfo.Disable();
    }
}
