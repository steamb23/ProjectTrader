using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class EmployeeHireWindow_ResetButton : MonoBehaviour
{
    [SerializeField]
    EmployeeHireWindow employeeHireWindow;
    [SerializeField]
    TMPro.TextMeshProUGUI textField;

    [Space]
    [SerializeField]
    float maxCount = 3;
    [SerializeField]
    float count = 3;

    public float MaxCount
    {
        get => this.maxCount;
        set => this.maxCount = value;
    }
    public float Count
    {
        get => this.count;
        set
        {
            this.count = value;
            UpdateTextField();
        }
    }

    // Use this for initialization

    private void Start()
    {
        if (employeeHireWindow == null)
        {
            employeeHireWindow = GetComponentInParent<EmployeeHireWindow>();
            if (employeeHireWindow == null)
                Debug.LogError("EmployeeHireWindow_ResetButton가 초기화 되지 않았습니다.");
        }
        if (textField == null)
        {
            Debug.LogError("EmployeeHireWindow_ResetButton의 textField가 초기화되지 않았습니다.");
        }
        UpdateTextField();
    }

    public void Click()
    {
        if (Count > 0)
        {
            Count--;
            employeeHireWindow.RenewCandiateList();
        }
    }

    public void UpdateTextField()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        if (Count > 0)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0.5f;
            canvasGroup.interactable = false;
        }
        textField.text = $"{Count} / {MaxCount}";
    }
}
