using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//배치, 슬롯
//>슬롯들을 배열로 받아서 배치할 슬롯의 데이터로 전부 초기화
//그럼 슬롯은 그 데이터로 주기만하면됨
//배치 버튼별로 코드를 따로 만들자^^..
//급한대로 한명씩만 고용
public class EmployeeAssignWindow : MonoBehaviour
{
    GameObject[] slotset;
    [SerializeField]
    GameObject manager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlotCasherSet()
    {
        manager.GetComponent<EmployeeUiManager>().OpenWatingWindow();
        slotset = GameObject.FindGameObjectsWithTag("EMPSlot");
        for(int i = 0; i < slotset.Length; i++)
        {
            slotset[i].GetComponent<EmployeeSlot>().SetInfo(0);
        }

    }

    public void SlotCleanInfo()
    {
        manager.GetComponent<EmployeeUiManager>().OpenWatingWindow();
        slotset = GameObject.FindGameObjectsWithTag("EMPSlot");
        for (int i = 0; i < slotset.Length; i++)
        {
            slotset[i].GetComponent<EmployeeSlot>().SetInfo(1);
        }

    }

    public void SlotCraftInfo()
    {
        manager.GetComponent<EmployeeUiManager>().OpenWatingWindow();
        slotset = GameObject.FindGameObjectsWithTag("EMPSlot");
        for (int i = 0; i < slotset.Length; i++)
        {
            slotset[i].GetComponent<EmployeeSlot>().SetInfo(2);
        }

    }
}
