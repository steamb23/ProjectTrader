using ProjectTrader;
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
    // 직원이 존재 할경우
    [SerializeField]
    private bool isEmployee;

    public bool IsEmployee
    {
        get => isEmployee;
        set
        {
            isEmployee = value;
            GetComponentInChildren<SpriteRenderer>().enabled = value;
        }
    }

    private new CashierAnimation animation;

    private void Start()
    {
        animation = GetComponentInChildren<CashierAnimation>();

        IsEmployee = IsEmployee;
    }

#if UNITY_EDITOR
    private void Update()
    {
        IsEmployee = IsEmployee;
    }
#endif


    /// <summary>
    /// 물품을 거래합니다.
    /// </summary>
    /// <param name="visitor">거래할 손님</param>
    public void ItemDeal(VisitorAi visitor, Action dealCompleted)
    {
        var timer = FloatingTimer.Create(this.transform, new Vector3(0.2f, 0), 5, (floatingTimer) =>
        {
            floatingTimer.FadeoutWithDestory();

            if (visitor.WishItems.Count > 0)
                PlayData.CurrentData.Money += visitor.WishItems[0].GetData().SellPrice;
            PlayData.CurrentData.Awareness += 1;

            dealCompleted?.Invoke();
        });
        timer.boostRatio = 3;

        if (animation != null)
            animation.PlayDealAnimation(null);
    }
}