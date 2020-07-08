using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 둥실둥실 움직임 효과를 넣는 스크립트입니다.
/// </summary>
public class Fluffy : MonoBehaviour
{
    [SerializeField] float frequency;
    [SerializeField] float amplitude;
    Vector3 localPosition;
    Vector3 currentLocalPosition;

    float seed;

    // Use this for initialization
    void Start()
    {
        currentLocalPosition = localPosition = transform.localPosition;

        seed = UnityEngine.Random.Range(-Mathf.PI, Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        // 실제 위치가 바뀌었으면
        if (this.transform.localPosition != currentLocalPosition)
        {
            // 기준 위치도 변경합니다.
            localPosition += this.transform.localPosition - currentLocalPosition;
        }

        currentLocalPosition = localPosition;

        currentLocalPosition.y = localPosition.y + Mathf.Sin((Time.time * Mathf.PI + seed) * frequency) * amplitude;
        this.transform.localPosition = currentLocalPosition;
    }
}
