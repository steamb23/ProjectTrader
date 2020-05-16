using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTimer : FloatingUIBase
{
    public const string PrefabPath = "Prefabs/FloatingUI/FloatingTimer";

    public static FloatingTimer Create(Transform targetTransform, float targetTime, Action<FloatingTimer> timeoutCallback) =>
        Create(targetTransform, default, targetTime, timeoutCallback);

    public static FloatingTimer Create(Transform targetTransform, Vector3 offset, float targetTime, Action<FloatingTimer> timeoutCallback)
    {
        var floatingTimer = CreateFloatingUI<FloatingTimer>(PrefabPath);
        var behaviour = floatingTimer.behaviour;
        var image = floatingTimer.image;
        // 초기화
        image.fillAmount = 0;
        behaviour.targetTime = targetTime;
        behaviour.timeoutCallback = timeoutCallback;

        behaviour.targetTransform = targetTransform;
        behaviour.offset = offset;
        behaviour.SetTarget(targetTransform, offset);

        return behaviour;
    }

    public UnityEngine.UI.Image uiImage;
    public float targetTime = 0;
    public float currentTime = 0;

    public float timeRatio = 1f;
    // 클릭시 부스팅될 값
    public float boostRatio = 1f;

    public Action<FloatingTimer> timeoutCallback;
    bool isDestory = false;

    // Start is called before the first frame update
    void Start()
    {
        if (uiImage == null)
            uiImage = GetComponentInChildren<UnityEngine.UI.Image>();
        //Fadein();
    }

    public void Click()
    {
        timeRatio = boostRatio;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!isDestory)
        {
            if (currentTime <= targetTime)
            {
                currentTime += Time.deltaTime * timeRatio;

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
                    FadeoutWithDestory();
            }
        }
    }

    //public void Fadein()
    //{
    //    StartCoroutine(FadeinCoroutine());
    //    IEnumerator FadeinCoroutine()
    //    {
    //        var color = uiImage.color;
    //        color.a = 0;
    //        uiImage.color = color;
    //        yield return null;
    //        while (true)
    //        {
    //            color = uiImage.color;
    //            color.a += 1 / 0.2f * Time.deltaTime;
    //            if (color.a >= 1)
    //            {
    //                color.a = 1;
    //                break; // 루프 탈출
    //            }
    //            else
    //            {
    //                uiImage.color = color;
    //            }
    //            yield return null;
    //        }
    //    }
    //}

    public void FadeoutWithDestory()
    {
        isDestory = true;
        StartCoroutine(FadeoutWithDestoryCoroutine());
        IEnumerator FadeoutWithDestoryCoroutine()
        {
            while (true)
            {
                var color = uiImage.color;
                color.a -= 1 / 0.2f * Time.deltaTime;
                if (color.a <= 0)
                {
                    Destroy(gameObject);
                    break; // 루프 탈출
                }
                else
                {
                    uiImage.color = color;
                }
                yield return null;
            }
        }
    }
}
