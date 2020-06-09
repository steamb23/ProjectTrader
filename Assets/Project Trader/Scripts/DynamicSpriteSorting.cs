using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpriteSorting : MonoBehaviour
{
    const int unitPerPixel = 100;

    public bool isStatic = false;
    public float virtualElevation = 0;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            // 현재 오브젝트에서 검색
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer==null)
            {
                // 자식 오브젝트에서 검색
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer == null)
                {
                    // 자식 오브젝트에서도 존재하지 않으면
                    Destroy(this);
                    return;
                }
            }
        }
        if (isStatic)
        {
            SetSortingOrder();
            Destroy(this);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SetSortingOrder();
    }

    void SetSortingOrder()
    {
        var position = transform.position;

        var y = (position.y - virtualElevation) * unitPerPixel;

        spriteRenderer.sortingOrder = -(int)y;
    }
}
