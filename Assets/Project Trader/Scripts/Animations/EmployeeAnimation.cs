using UnityEngine;
using System.Collections;
using ProjectTrader.SpriteDatas;
using ProjectTrader;

public class EmployeeAnimation : MoveableAnimation
{
    public enum IdleStateType
    {
        Default,
        Cleaning
    }

    [SerializeField] IdleStateType idleState;

    public IdleStateType IdleState
    {
        get => this.idleState;
        set => this.idleState = value;
    }

    protected override void IdleAnimation()
    {
        switch (idleState)
        {
            default:
                base.IdleAnimation();
                break;
            case IdleStateType.Cleaning:
                CleaningAnimation();
                break;
        }
    }

    protected virtual void CleaningAnimation()
    {
        for (int i = 0; i < animationDatas.Length; i++)
        {
            var spriteRenderer = animationDatas[i].spriteRenderer;
            var movableSpriteData = animationDatas[i].movableSpriteData as EmployeeSpriteData;

            if (movableSpriteData.Cleaning.Length > 0)
                spriteRenderer.sprite = movableSpriteData.Cleaning[frame % movableSpriteData.Cleaning.Length];
            else base.IdleAnimation();
            break;
        }
    }
}
