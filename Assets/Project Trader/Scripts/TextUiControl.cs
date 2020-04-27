using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextUiControl : MonoBehaviour
{
    //textbox프리팹 적용(이미지-텍스트)
    public GameObject[] textBox;

    GameObject canvas;
    RectTransform tbPos;

    //텍스트가 나타났다 사라지는 시간
    //public int textBoxTime=2;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        tbPos = GetComponent<RectTransform>();
    }


    void Update()
    {
        
    }
    // 말풍선의 종류, x위치,y위치,문자열+유지시간 추가가능,유지시간
    public void CreativeTextBox(int a, int x, int y, string gg, int ti)
    {
        GameObject te = Instantiate(textBox[a]) as GameObject;
        TextMeshProUGUI text_re = te.GetComponentInChildren<TextMeshProUGUI>();

        //string적용
        text_re.text = gg;

        //캔버스에 생성
        te.transform.SetParent(canvas.transform);
        tbPos = te.GetComponent<RectTransform>();

        //위치조절
        tbPos.anchoredPosition = new Vector3(x, y);

        //제거
        Destroy(te, ti);
    }
}
