using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeUiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject background = null;

    [SerializeField]
    private GameObject hireWindow = null;
    [SerializeField]
    private GameObject assignWindow = null;
    [SerializeField]
    private GameObject watingWindow = null;
    [SerializeField]
    private GameObject buttongroup = null;
    [SerializeField]
    GameObject ButtonGroup;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CloseButton();
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Input.GetKeyDown(KeyCode.H))
        {
            OpenHireWindow();
        }
#endif

        if (hireWindow.activeSelf ||
            assignWindow.activeSelf ||
            watingWindow.activeSelf)
        {
            background.SetActive(true);
        }
        else
        {
            background.SetActive(false);
        }
    }

    public void OpenHireWindow()
    {
        if (assignWindow.activeSelf || watingWindow.activeSelf)
        {
            assignWindow.SetActive(false);
            watingWindow.SetActive(false);
        }
        buttongroup.SetActive(true);
        hireWindow.SetActive(true);
        ButtonGroup.GetComponent<EmpButtonGroup>().HireButtonOn();
    }

    public void OpenAssignWindow()
    {
        if (hireWindow.activeSelf || watingWindow.activeSelf)
        {
            hireWindow.SetActive(false);
            watingWindow.SetActive(false);
        }
        assignWindow.SetActive(true);
        buttongroup.SetActive(true);
        ButtonGroup.GetComponent<EmpButtonGroup>().AssignButtonOn();
        FindObjectOfType<EmployeeAssignButton>().PushCleanerButton();
    }

    public void OpenWatingWindow()
    {
        if (hireWindow.activeSelf ||assignWindow.activeSelf)
        {
            hireWindow.SetActive(false);
            assignWindow.SetActive(false);
        }
        buttongroup.SetActive(false);
        watingWindow.SetActive(true);
        this.gameObject.GetComponent<EmployeeDataCatch>().AddEmp();

    }

    public void CloseButton()
    {
        if (!hireWindow.activeSelf && !assignWindow.activeSelf)
            buttongroup.SetActive(false);
    }

    public void CloseWaiting()
    {
        assignWindow.SetActive(true);
        buttongroup.SetActive(true);
        watingWindow.SetActive(false);
        ButtonGroup.GetComponent<EmpButtonGroup>().AssignButtonOn();
    }
}
