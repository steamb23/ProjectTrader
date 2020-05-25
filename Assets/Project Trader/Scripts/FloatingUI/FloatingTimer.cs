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
        var image = floatingTimer.behaviour.uiImage;
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
        Fadein();
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

    public void Fadein()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeinCoroutine());
        IEnumerator FadeinCoroutine()
        {
            canvasGroup.alpha = 0;
            yield return null;
            while (true)
            {
                canvasGroup.alpha += 1 / 0.2f * Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    canvasGroup.alpha = 1;
                    break; // 루프 탈출
                }
                yield return null;
            }
        }
    }

    public void FadeoutWithDestory()
    {
        isDestory = true;
        var canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeoutWithDestoryCoroutine());
        IEnumerator FadeoutWithDestoryCoroutine()
        {
            while (true)
            {
                canvasGroup.alpha -= 1 / 0.2f * Time.deltaTime;
                if (canvasGroup.alpha <= 0)
                {
                    Destroy(gameObject);
                    break; // 루프 탈출
                }
                yield return null;
            }
        }
    }
}
