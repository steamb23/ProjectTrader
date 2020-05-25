using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultWindow : MonoBehaviour
{
    int sellGold;//임시 판매금-이후 판매 시스템에서 처리
    int tip;
    int totalvisitor; //방문자- 방문ai쪽에서 받아오기
    int rent=50;//임시 임대료
    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateResultWindow()
    {

    }

    void ResultText(string textName)
    {
        tmp = (GameObject.Find(textName)).GetComponent <TextMeshProUGUI> ();
        tmp.text = "555555";
    }
}
