using ProjectTrader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 테스트용 스크립트
/// </summary>
public class Test : MonoBehaviour
{
    // 테스트 중일때만 실행
#if DEBUG || DEVELOPMENT_BUILD || UNITY_EDITOR
    private void Start()
    {

        Debug.Log("Test");
    }
#endif
}
