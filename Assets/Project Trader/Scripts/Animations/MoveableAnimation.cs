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
        }

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
}
