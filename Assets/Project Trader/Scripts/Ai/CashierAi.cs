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
    new CashierAnimation animation;
    private void Start()
    {
        animation = GetComponentInChildren<CashierAnimation>();
    }


    /// <summary>
    /// 물품을 거래합니다.
    /// </summary>
    /// <param name="visitor">거래할 손님</param>
    public void ItemDeal(VisitorAi visitor, Action dealCompleted)
    {
        if (animation != null)
            animation.PlayDealAnimation(ItemDealCallback);
        else
            ItemDealCallback();

        void ItemDealCallback()
        {
            FloatingTimer.CreateFloatingTimer(this.transform, new Vector3(0.2f,0), 5, (floatingTimer) =>
            {
                floatingTimer.FadeoutWithDestory();
                // 가격
                int price = 0;
                foreach (var item in visitor.WishItems)
                {
                    price += item.Data.SellPrice * item.Count;
                }

                // 구매할 아이템 초기화
                visitor.WishItems.Clear();
                dealCompleted?.Invoke();
            });
        }
    }
}