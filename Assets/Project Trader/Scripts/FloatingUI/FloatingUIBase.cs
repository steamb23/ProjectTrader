using UnityEngine;
using System.Collections;

public class FloatingUIBase : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;

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
}
