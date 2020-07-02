using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButtoncontrol : MonoBehaviour
{
    [SerializeField]
    GameObject optionwindow;
    [SerializeField]
    GameObject employeewindow;
    [SerializeField]
    GameObject questwindow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenOption()
    {
        optionwindow.SetActive(true);
    }

    public void OpenEmp()
    {
        employeewindow.SetActive(true);
    }

    public void OpenQuest()
    {

    }

    public void FastSave()
    {

    }
}