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
    IdleStateType prevIdleState;
    [SerializeField] IdleStateType idleState;
    [SerializeField] bool easterEgg;

    public IdleStateType IdleState
    {
        get => this.idleState;
        set
        {
            this.idleState = value;
            if (idleState != prevIdleState)
            {
                prevIdleState = idleState;
                frame = 0;
                time = 0;
            }

            easterEgg = Random.Range(0f, 1f) < 0.000005f;
        }
    }

    protected override void IdleAnimation()
    {
        switch (IdleState)
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
        if (easterEgg)
            frame++;
        for (int i = 0; i < animationDatas.Length; i++)
        {
            var spriteRenderer = animationDatas[i].spriteRenderer;
            var movableSpriteData = animationDatas[i].movableSpriteData as EmployeeSpriteData;

            if (movableSpriteData.Cleaning.Length > 0)
                spriteRenderer.sprite = movableSpriteData.Cleaning[frame % movableSpriteData.Cleaning.Length];
            else base.IdleAnimation();
        }
    }
}
