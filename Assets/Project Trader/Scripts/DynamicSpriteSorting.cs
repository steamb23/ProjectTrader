using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSpriteSorting : MonoBehaviour
{
    const int unitPerPixel = 100;

    public bool isStatic = false;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer==null)
            {
                Destroy(this);
                return;
            }
        }
        if (isStatic)
        {
            SetSortingOrder();
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetSortingOrder();
    }

    void SetSortingOrder()
    {
        var position = transform.position;

        var y = position.y * unitPerPixel;

        spriteRenderer.sortingOrder = -(int)y;
    }
}
