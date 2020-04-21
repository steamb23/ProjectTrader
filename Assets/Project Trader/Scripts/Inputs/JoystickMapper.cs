using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Inputs
{
    static class JoystickMapper
    {
        static private float FarFromZero(float a, float b)
        {
            var aAbs = Mathf.Abs(a);
            var bAbs = Mathf.Abs(b);
            if (aAbs > bAbs)
                return a;
            else
                return b;
        }
        public static Vector2 GetAxisRaw()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            Vector2 virtualVector = VirtualJoystickController != null ? VirtualJoystickController.Velocity : Vector2.zero;

            return new Vector2(FarFromZero(horizontal, virtualVector.x), FarFromZero(vertical, virtualVector.y));
        }

        public static Vector2 GetAxis()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            Vector2 virtualVector = VirtualJoystickController != null ? VirtualJoystickController.Velocity : Vector2.zero;

            return new Vector2(FarFromZero(horizontal, virtualVector.x), FarFromZero(vertical, virtualVector.y));
        }

        static VirtualJoystickController virtualJoystickController;
        public static VirtualJoystickController VirtualJoystickController
        {
            get => virtualJoystickController != null ? virtualJoystickController : null;
        }

        public static void SetVirtualJoystickController(VirtualJoystickController virtualJoystickController)
        {
            JoystickMapper.virtualJoystickController = virtualJoystickController;
        }
    }
}
