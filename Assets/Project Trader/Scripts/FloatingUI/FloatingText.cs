using UnityEngine;
using System.Collections;
using TMPro;

public class FloatingText : FloatingUIBase
{
    public const string PrefabPath = "Prefabs/FloatingUI/FloatingText";

    public static FloatingText Create(Transform targetTransform, string text = "") => Create(targetTransform, default, text);

    public static FloatingText Create(Transform targetTransform, Vector3 offset, string text="")
    {
        var floatingText = CreateFloatingUI<FloatingText>(PrefabPath);
        var behaviour = floatingText.behaviour;

        behaviour.Text = text;

        return behaviour;
    }

    [SerializeField] TextMeshProUGUI textUi;

    public string Text
    {
        get => textUi.text;
        set => textUi.text = value;
    }
}
