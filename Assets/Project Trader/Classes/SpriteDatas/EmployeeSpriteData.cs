using UnityEngine;
using System.Collections;
using ProjectTrader.SpriteDatas;

namespace ProjectTrader.SpriteDatas
{
    [CreateAssetMenu(fileName = "EmployeeSpriteData", menuName = "스프라이트 데이터/직원 스프라이트 데이터")]
    public class EmployeeSpriteData : MovableSpriteData
    {
        public Sprite[] Cleaning;
    }
}