using UnityEngine;
using System.Collections;
using System;

public class EmployeeHireWindow : MonoBehaviour
{
    [SerializeField]
    GameObject candiateInfo0;
    [SerializeField]
    GameObject candiateInfo1;
    [SerializeField]
    GameObject candiateInfo2;

    bool isInitialized = false;

    // Use this for initialization
    void Start()
    {
        if (candiateInfo0 == null ||
            candiateInfo1 == null ||
            candiateInfo2 == null)
        {
            Debug.LogError("EmployeeHireWindow에 오브젝트가 연결되어 있지 않습니다.");
        }
        else
        {
            if (!isInitialized)
            {
                RenewCandiateList();
                isInitialized = true;
            }
        }
    }

    public void RenewCandiateList()
    {
        // 임시 초기화
        var names = new string[]
        {
            "돌돌이",
            "삼숙이",
            "쵸코",
            "제임스",
            "어거스트",
            "민트",
            "호롤로",
            "한조",
            "김근육",
            "야옹이",
            "뀨뀨"
        };

        var candiateInfo0 = this.candiateInfo0.GetComponent<CandiateInfo>();
        var candiateInfo1 = this.candiateInfo1.GetComponent<CandiateInfo>();
        var candiateInfo2 = this.candiateInfo2.GetComponent<CandiateInfo>();

        SetData(candiateInfo0);
        SetData(candiateInfo1);
        SetData(candiateInfo2);

        void SetData(CandiateInfo candiateInfo)
        {
            // 임시 데이터
            candiateInfo.Name = names[UnityEngine.Random.Range(0, names.Length)];
            candiateInfo.Age = $"{(UnityEngine.Random.Range(0, 2) > 0 ? "남" : "여")} / {UnityEngine.Random.Range(20, 40)}";
            candiateInfo.Charisma = $"{UnityEngine.Random.Range(0, 999)}";
            candiateInfo.Inteligent = $"{UnityEngine.Random.Range(0, 999)}";
            candiateInfo.Dexturity = $"{UnityEngine.Random.Range(0, 999)}";
            candiateInfo.State = UnityEngine.Random.Range(0, 100) > 80 ?
                "무경력" :
                $"{(UnityEngine.Random.Range(0, 2) > 0 ? "정규직" : "비정규직")} {UnityEngine.Random.Range(1, 10)}년";
            candiateInfo.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
