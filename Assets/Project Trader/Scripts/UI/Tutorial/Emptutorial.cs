using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emptutorial : MonoBehaviour
{
    [SerializeField]
    GameObject employeewindow;
    [SerializeField]
    GameObject hirebutton;
    [SerializeField]
    GameObject assignclosebutton;
    [SerializeField]
    GameObject waitingwindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //3~7까지
    public void EmptutorialStart(int num)
    {
        switch (num)
        {
            case 3:
                employeewindow.GetComponent<EmployeeUiManager>().OpenHireWindow();
                break;
            case 5:
                hirebutton.GetComponent<EmployeeHireWindow_HireButton>().Click();
                break;
            case 6:
                employeewindow.GetComponent<EmployeeUiManager>().OpenAssignWindow();
                break;
            case 7:
                employeewindow.GetComponent<EmployeeAssignWindow>().SlotCasherSet();
                break;
            case 8:
                waitingwindow.GetComponent<EmployeeSlot>().Click();
                assignclosebutton.GetComponent<ExitButton>().Click();
                break;
            case 9:
                break;
            default:
                break;
        }
    }
}
