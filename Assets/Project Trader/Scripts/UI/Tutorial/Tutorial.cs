using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//튜토리얼을 보면 tutorial스크립트 자체를 꺼버리기
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject[] tutorial;
    [SerializeField]
    int num;
    int empnum;
    [SerializeField]
    GameObject[] tutorialwindow;
    [SerializeField]
    GameObject towmcamera;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        empnum = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeTutorial();
        }
    }

    void ChangeTutorial()
    {
        num++;
        //if (num == tutorial.Length)
        //{
        //    this.gameObject.SetActive(false);
        //    return;
        //}
        if (num < 3)
        {
            if (num >= 3)
                tutorial[0].SetActive(false);
            if (num != 1)
                tutorial[num - 1].SetActive(false);
            tutorial[num].SetActive(true);
        }
        if (tutorialwindow[0].activeSelf == true || tutorialwindow[1].activeSelf == true || tutorialwindow[2].activeSelf == true)
        {
            if (tutorial[2].activeSelf == true)
                tutorial[2].SetActive(false);
            EmployeeTutorial();
        }
    }

    void EmployeeTutorial()
    {
        empnum++;
        if (empnum <= 5)
        {
            tutorial[empnum - 1].SetActive(false);
            tutorial[empnum].SetActive(true);
        }
    }

    void ShopTutorial()
    {

    }

    void MakerTutorial()
    {

    }

    void DisplayTutorial()
    {

    }
}
