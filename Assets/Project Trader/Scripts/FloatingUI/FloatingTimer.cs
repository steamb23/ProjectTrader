using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTimer : FloatingUIBase
{
    public const string FloatingTimerResourcePath = "Prefabs/FloatingUI/FloatingTimer";

    public static FloatingTimer CreateFloatingTimer(Transform targetTransform, float targetTime, Action<FloatingTimer> timeoutCallback) =>
        CreateFloatingTimer(targetTransform, Vector3.zero, targetTime, timeoutCallback);

    public static FloatingTimer CreateFloatingTimer(Transform targetTransform, Vector3 offset, float targetTime, Action<FloatingTimer> timeoutCallback)
    {
        var gameObjectAsset = Resources.Load<GameObject>(FloatingTimerResourcePath);
        var gameObject = Instantiate(gameObjectAsset);

        var behaviour = gameObject.GetComponent<FloatingTimer>();

        // 초기화
        behaviour.targetTime = targetTime;
        behaviour.timeoutCallback = timeoutCallback;

        behaviour.targetTransform = targetTransform;
        behaviour.offset = offset;

        // 부모관계 설정
        var floatingCanvas = GameObject.Find("FloatingCanvas");
        var transform = gameObject.transform;
        transform.SetParent(floatingCanvas.transform);

        return behaviour;
    }


    public UnityEngine.UI.Image uiImage;
    public float targetTime = 0;
    public float currentTime = 0;

    public Action<FloatingTimer> timeoutCallback;

    // Start is called before the first frame update
    void Start()
    {
        if (uiImage == null)
            uiImage = GetComponentInChildren<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (currentTime <= targetTime)
        {
            currentTime += Time.deltaTime;

            if (targetTime != 0)
                uiImage.fillAmount = currentTime / targetTime;
            else
                uiImage.fillAmount = 1;
        }
        else
        {
            if (timeoutCallback != null)
                timeoutCallback(this);
            else
                Destroy(gameObject);
        }
    }
}
