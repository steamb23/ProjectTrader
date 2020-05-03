using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 진열된 아이템을 나타냅니다.
/// </summary>
public class DisplayedItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // TODO: 아이템 데이터, 추가 예정
    // public Item itemData

    /// <summary>
    /// 남은 아이템. 인스펙터 전용. 코드에서 접근하지마세요.
    /// </summary>
    [Obsolete("인스펙터 전용 변수입니다. 대신에 ItemCount 프로퍼티를 사용하세요.")]
    public int itemCount;

#pragma warning disable CS0618
    public int ItemCount
    {
        get => itemCount;
        set
        {
            itemCount = value;

            // 스프라이트 표시 변경
            spriteRenderer.enabled = itemCount > 0;
        }
    }
#pragma warning restore CS0618

    void Start()
    {
        // null 이면 SpriteRenderer를 검색
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        // itemData가 null이 아니면 해당 데이터로 스프라이트 교체

        // 스프라이트 표시 여부 초기화
        ItemCount = ItemCount;
    }
}
