using UnityEngine;
using System.Collections;
using TMPro;

public class EmployeeCount : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textField;
    [Space]
    [SerializeField]
    int maxCount = 10;
    [SerializeField]
    int count = 0;

    public int MaxCount
    {
        get => this.maxCount;
        set => this.maxCount = value;
    }
    public int Count
    {
        get => this.count;
        set
        {
            this.count = value;
            UpdateTextField();
        }
    }



    // Use this for initialization
    void Start()
    {
        if (textField == null)
        {
            Debug.LogError("EmployeeHireWindow_ResetButton의 textField가 초기화되지 않았습니다.");
        }
        UpdateTextField();
    }

    public void UpdateTextField()
    {
        textField.text = $"{Count} / {MaxCount}";
    }
}
