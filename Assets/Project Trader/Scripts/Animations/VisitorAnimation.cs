using Pathfinding;
using ProjectTrader;
using ProjectTrader.SpriteDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class VisitorAnimation : MonoBehaviour
{

    public VisitorSpriteData visitorSpriteData;
    public SpriteRenderer spriteRenderer;
    public AIPath aiPath;
    public FourDirection direction;


    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = this.transform.position;

        if (aiPath== null)
        {
            aiPath = GetComponentInChildren<AIPath>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void LateUpdate()
    {
        var velocity = aiPath.velocity;

        if (velocity.sqrMagnitude > aiPath.maxSpeed * aiPath.maxSpeed * 0.1f)
        {
            direction = Utils.VelocityToDirection(velocity);
        }

        switch (direction)
        {
            case FourDirection.Up:
                spriteRenderer.sprite = visitorSpriteData.Idle.Back[0];
                break;
            case FourDirection.Down:
                spriteRenderer.sprite = visitorSpriteData.Idle.Front[0];
                break;
            case FourDirection.Right:
                spriteRenderer.sprite = visitorSpriteData.Idle.Right[0];
                break;
            case FourDirection.Left:
                spriteRenderer.sprite = visitorSpriteData.Idle.Left[0];
                break;
        }
    }
}
