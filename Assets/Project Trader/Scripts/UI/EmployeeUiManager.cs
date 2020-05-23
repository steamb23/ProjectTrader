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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hireWindow.activeSelf ||
            assignWindow.activeSelf)
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
        hireWindow.SetActive(true);
    }

    public void OpenAssignWindow()
    {
        assignWindow.SetActive(true);
    }
}
