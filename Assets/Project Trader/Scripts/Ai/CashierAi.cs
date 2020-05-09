using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 계산원 Ai
/// </summary>
class CashierAi : MonoBehaviour
{
    /// <summary>
    /// 물품을 거래합니다.
    /// </summary>
    /// <param name="visitor">거래할 손님</param>
    public void ItemDeal(VisitorAi visitor)
    {
        // 가격
        int price = 0;
        foreach (var item in visitor.WishItems)
        {
            // 아래와 같이 구현 예정
            price += item.Data.SellPrice * item.Count;
        }

        // 구매할 아이템 초기화
        visitor.WishItems.Clear();
    }

    public void ItemDealAnimation()
    {

    }
}