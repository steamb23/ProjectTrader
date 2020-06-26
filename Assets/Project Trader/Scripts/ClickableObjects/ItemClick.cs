using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemClick : ClickableObject
{
    [SerializeField] DisplayedItem displayedItem;
    [SerializeField] int index;

    public override void Click()
    {
        // 선택된 오브젝트 설정
        FindObjectOfType<TableCheck>().choiceTable = displayedItem.gameObject;

        // 윈도우 열기
        FindObjectOfType<SellWindow>().OpenMakerWindow();
    }
}