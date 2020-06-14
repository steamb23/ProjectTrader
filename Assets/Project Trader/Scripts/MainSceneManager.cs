using UnityEngine;
using System.Collections;
using ProjectTrader;
using System;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] GameObject logo;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] float logoFrequency;
    [SerializeField] float logoAmplitude;
    [SerializeField] float textFrequency;

    Vector3 logoOriginPosition;

    // Use this for initialization
    void Start()
    {
        if (logo == null)
        {
            Debug.LogError($"logo가 지정되어있지 않습니다.", this);
        }
        else
        {
            logoOriginPosition = logo.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var logoPos = logoOriginPosition;
        logoPos.y += Mathf.Sin(Time.time * logoFrequency * Mathf.PI) * logoAmplitude;
        logo.transform.position = logoPos;

        var textColor = text.color;
        textColor.a = (Mathf.Cos(Time.time * textFrequency * Mathf.PI) + 1) * 0.5f;
        text.color = textColor;
    }

    public void Click()
    {
        // 클릭하면 데이터 로딩 및 씬 전환

        // 플레이 데이터 초기화
        // TODO: 저장된 데이터가 있으면 저장된 데이터를 불러오도록 수정해야함.
        PlayData.SetCurrentData(new PlayData());

        var sceneLoadManager = SceneLoadManager.Instance;

        // TODO: 불러온 플레이 데이터에 따라 로드되도록 수정해야함.
        sceneLoadManager.LoadScene(SceneLoadManager.ShopScene.Shop1);
    }
}
