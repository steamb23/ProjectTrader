using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader
{
    public enum FourDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    public static class Utils
    {

        public static FourDirection VelocityToDirection(Vector3 velocity)
        {
            var angle = Math.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            // 오른쪽
            if (angle < 45 && angle > -45)
            {
                return FourDirection.Right;
            }
            // 위
            else if (angle < 135 && angle > 45)
            {
                return FourDirection.Up;
            }
            // 아래
            else if (angle > -135 && angle < -45)
            {
                return FourDirection.Down;
            }
            // 왼쪽
            else
            {
                return FourDirection.Left;
            }
        }
    }
}
