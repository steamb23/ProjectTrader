using Pathfinding;
using ProjectTrader;
using ProjectTrader.SpriteDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class VisitorAnimation : MoveableAnimation
{

    private VisitorSpriteData visitorSpriteData;

    protected override void Start()
    {
        base.Start();
        visitorSpriteData = moveableSpriteData as VisitorSpriteData;
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
}
