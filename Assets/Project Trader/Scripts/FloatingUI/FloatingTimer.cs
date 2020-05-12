using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        var image = gameObject.GetComponent<Image>();

        // 부모관계 설정
        var floatingCanvas = GameObject.Find("FloatingCanvas");
        var transform = gameObject.transform;
        transform.SetParent(floatingCanvas.transform, true);
        transform.localScale = Vector3.one;
        image.SetNativeSize();
        image.fillAmount = 0;

        // 초기화
        behaviour.targetTime = targetTime;
        behaviour.timeoutCallback = timeoutCallback;

        behaviour.targetTransform = targetTransform;
        behaviour.offset = offset;
        behaviour.SetPosition();

        return behaviour;
    }


    public UnityEngine.UI.Image uiImage;
    public float targetTime = 0;
    public float currentTime = 0;

    public Action<FloatingTimer> timeoutCallback;
    bool isDestory = false;

    // Start is called before the first frame update
    void Start()
    {
        if (uiImage == null)
            uiImage = GetComponentInChildren<UnityEngine.UI.Image>();
        //Fadein();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!isDestory)
        {
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
