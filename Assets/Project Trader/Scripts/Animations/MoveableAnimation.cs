using UnityEngine;
using System.Collections;
using ProjectTrader.SpriteDatas;
using Pathfinding;
using ProjectTrader;

public abstract class MoveableAnimation : MonoBehaviour
{
    public MovableSpriteData moveableSpriteData;
    public SpriteRenderer spriteRenderer;
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
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
                if(whileCount++ > 10)
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


        void IdleAnimation()
        {
            switch (direction)
            {
                case FourDirection.Up:
                    spriteRenderer.sprite = moveableSpriteData.Idle.Back[0];
                    break;
                case FourDirection.Down:
                    spriteRenderer.sprite = moveableSpriteData.Idle.Front[0];
                    break;
                case FourDirection.Right:
                    spriteRenderer.sprite = moveableSpriteData.Idle.Right[0];
                    break;
                case FourDirection.Left:
                    spriteRenderer.sprite = moveableSpriteData.Idle.Left[0];
                    break;
            }
        }
        void WalkAnimaition()
        {
            switch (direction)
            {
                case FourDirection.Up:
                    if (moveableSpriteData.Walk.Back.Length > 0)
                        spriteRenderer.sprite = moveableSpriteData.Walk.Back[frame % moveableSpriteData.Walk.Back.Length];
                    else goto default;
                    break;
                case FourDirection.Down:
                    if (moveableSpriteData.Walk.Front.Length > 0)
                        spriteRenderer.sprite = moveableSpriteData.Walk.Front[frame % moveableSpriteData.Walk.Front.Length];
                    else goto default;
                    break;
                case FourDirection.Right:
                    if (moveableSpriteData.Walk.Right.Length > 0)
                        spriteRenderer.sprite = moveableSpriteData.Walk.Right[frame % moveableSpriteData.Walk.Right.Length];
                    else goto default;
                    break;
                case FourDirection.Left:
                    if (moveableSpriteData.Walk.Left.Length > 0)
                        spriteRenderer.sprite = moveableSpriteData.Walk.Left[frame % moveableSpriteData.Walk.Left.Length];
                    else goto default;
                    break;
                default:
                    IdleAnimation();
                    break;
            }
        }
    }
}
