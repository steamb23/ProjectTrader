using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeUiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject background = null;

    [SerializeField]
    private GameObject employeeHireWindow = null;
    [SerializeField]
    private GameObject employeeAssignWindow = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (employeeHireWindow.activeSelf ||
            employeeAssignWindow.activeSelf)
        {
            background.SetActive(true);
        }
        else
        {
            background.SetActive(false);
        }
    }

    public void OpenEmployeeHire()
    {
        employeeHireWindow.SetActive(true);
    }
}
