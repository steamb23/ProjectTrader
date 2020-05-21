using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.SpriteDatas
{
    [CreateAssetMenu(fileName = "VistorSpriteData", menuName = "스프라이트 데이터/손님 스프라이트 데이터")]
    public class VisitorSpriteData : MovableSpriteData
    {
        public SpriteData Grab;
    }
}
