using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public abstract class FloatingUIBase : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;

    protected static (GameObject gameObject, T behaviour, Image image, Transform transform) CreateFloatingUI<T>(string resourcePath) where T : FloatingUIBase
    {
        var gameObjectAsset = Resources.Load<GameObject>(resourcePath);
        var gameObject = Instantiate(gameObjectAsset);

        var behaviour = gameObject.GetComponent<T>();
        var image = gameObject.GetComponent<Image>();

        // 부모관계 설정
        var floatingCanvas = GameObject.Find("FloatingCanvas");
        var transform = gameObject.transform;
        transform.SetParent(floatingCanvas.transform, true);
        transform.localScale = Vector3.one;

        if(image!=null)
        image.SetNativeSize();

        return (gameObject, behaviour as T, image, transform);
    }

    protected virtual void Update()
    {
        SetPosition();
    }

    public void SetPosition()
    {
        if (targetTransform != null)
        {
            transform.position = targetTransform.position + offset;
        }
    }

    public void SetTarget(Transform targetTransform, Vector3 offset = default)
    {
        this.targetTransform = targetTransform;
        this.offset = offset;

        SetPosition();
    }
}
