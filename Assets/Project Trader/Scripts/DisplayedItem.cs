using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectTrader;
using ProjectTrader.Datas;
using ProjectTrader.SpriteDatas;

/// <summary>
/// 진열된 아이템을 나타냅니다.
/// </summary>
public class DisplayedItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private Item item;

    [SerializeField]
    //private int itemCount;

    public Item Item
    {
        get => item;
        set
        {
            item = value;

            // null 이면 SpriteRenderer를 검색
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            spriteRenderer.sprite = ItemSpriteData.GetItemSprite(item.Code);
        }
    }

    public int ItemCount
    {
        get => item.Count;
        set
        {
            item.Count = value;

            // 스프라이트 표시 변경
            // null 이면 SpriteRenderer를 검색
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer == null)
                    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            spriteRenderer.enabled = item.Count > 0;
        }
    }

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

        // 프로퍼티 초기화
        Item = Item;
        ItemCount = ItemCount;
    }
}
