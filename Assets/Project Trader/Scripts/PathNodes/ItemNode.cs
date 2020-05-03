using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNode : PathNode
{
    public DisplayedItem displayedItem;

    void Start()
    {
        if (displayedItem == null)
            Debug.LogWarning($"{name}의 displayedItem 항목에 아무런 오브젝트도 연결되어 있지 않습니다!");
    }

    void Update()
    {
        
    }
}
