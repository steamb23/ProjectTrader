using UnityEngine;
using System.Collections;
using ProjectTrader.SpriteDatas;
using Pathfinding;
using ProjectTrader;
using System;

public abstract class MoveableAnimation : MonoBehaviour
{
    [Serializable]
    public struct AnimationData
    {
        public MovableSpriteData movableSpriteData;
        public SpriteRenderer spriteRenderer;
    }

    public AnimationData[] animationDatas;
    public AIPath aiPath;
    public FourDirection direction;

    public enum AnimationState
    {
        Idle,
        Walk,
    }

    public AnimationState state;

    public float walkAnimationInterval = 0.2f;

    public float time = 0;

    public int frame = 0;


    private Vector3 previousPosition;
    // Use this for initialization
    protected virtual void Start()
    {
        previousPosition = this.transform.position;

        if (aiPath == null)
        {
            aiPath = GetComponentInChildren<AIPath>();
        }
    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        var velocity = aiPath.velocity;

        if (velocity.sqrMagnitude > aiPath.maxSpeed * aiPath.maxSpeed * 0.1f)
        {
            direction = Utils.VelocityToDirection(velocity);
            state = AnimationState.Walk;

            time += Time.deltaTime;
            int whileCount = 0;
            while (time > walkAnimationInterval)
            {
                time -= walkAnimationInterval;
                frame++;
                if (whileCount++ > 10)
                {
                    Debug.LogWarning("walkAnimationInterval이 현재 Time.deltaTime보다 값이 작아 무한 루프가 발생했습니다!");
                    time = 0;
                }
            }
        }
        else
        {
            state = AnimationState.Idle;

            frame = 0;
            time = 0;
        }

        switch (state)
        {
            case AnimationState.Idle:
                IdleAnimation();
                break;
            case AnimationState.Walk:
                WalkAnimaition();
                break;
        }
    }

    protected virtual void IdleAnimation()
    {
        for (int i = 0; i < animationDatas.Length; i++)
        {
            var spriteRenderer = animationDatas[i].spriteRenderer;
            var movableSpriteData = animationDatas[i].movableSpriteData;
            switch (direction)
            {
                case FourDirection.Up:
                    spriteRenderer.sprite = movableSpriteData.Idle.Back[0];
                    break;
                case FourDirection.Down:
                    spriteRenderer.sprite = movableSpriteData.Idle.Front[0];
                    break;
                case FourDirection.Right:
                    spriteRenderer.sprite = movableSpriteData.Idle.Right[0];
                    break;
                case FourDirection.Left:
                    spriteRenderer.sprite = movableSpriteData.Idle.Left[0];
                    break;
            }
        }
    }
    protected virtual void WalkAnimaition()
    {
        for (int i = 0; i < animationDatas.Length; i++)
        {
            var spriteRenderer = animationDatas[i].spriteRenderer;
            var movableSpriteData = animationDatas[i].movableSpriteData;
            switch (direction)
            {
                case FourDirection.Up:
                    if (movableSpriteData.Walk.Back.Length > 0)
                        spriteRenderer.sprite = movableSpriteData.Walk.Back[frame % movableSpriteData.Walk.Back.Length];
                    else goto default;
                    break;
                case FourDirection.Down:
                    if (movableSpriteData.Walk.Front.Length > 0)
                        spriteRenderer.sprite = movableSpriteData.Walk.Front[frame % movableSpriteData.Walk.Front.Length];
                    else goto default;
                    break;
                case FourDirection.Right:
                    if (movableSpriteData.Walk.Right.Length > 0)
                        spriteRenderer.sprite = movableSpriteData.Walk.Right[frame % movableSpriteData.Walk.Right.Length];
                    else goto default;
                    break;
                case FourDirection.Left:
                    if (movableSpriteData.Walk.Left.Length > 0)
                        spriteRenderer.sprite = movableSpriteData.Walk.Left[frame % movableSpriteData.Walk.Left.Length];
                    else goto default;
                    break;
                default:
                    IdleAnimation();
                    break;
            }
        }
    }
}
