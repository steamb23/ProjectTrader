using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FloatingButton : FloatingUIBase
{
    public const string PrefabPath = "Prefabs/FloatingUI/FloatingButton";

    public static FloatingButton Create(Transform targetTransform, Sprite sprite, Action<FloatingButton> buttonClickCallback) =>
        Create(targetTransform, default, sprite, buttonClickCallback);

    public static FloatingButton Create(Transform targetTransform, Vector3 offset, Sprite sprite, Action<FloatingButton> buttonClickCallback)
    {
        var floatingUI = CreateFloatingUI<FloatingButton>(PrefabPath);
        var behaviour = floatingUI.behaviour;
        var image = floatingUI.image;

        // 초기화
        behaviour.buttonClickCallback = buttonClickCallback;
        if (sprite != null)
            image.sprite = sprite;

        behaviour.SetTarget(targetTransform, offset);

        return behaviour;
    }

    public void ButtonClick()
    {
        if (buttonClickCallback != null)
            buttonClickCallback(this);
        else
            Debug.LogWarning($"buttonClickCallback이 지정되어 있지 않습니다.");
    }

    public Action<FloatingButton> buttonClickCallback;
}
