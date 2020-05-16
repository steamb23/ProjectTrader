using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Collections;
using System.Collections.ObjectModel;

namespace ProjectTrader.SpriteDatas
{
    [CreateAssetMenu(fileName = "VisitorSpriteData", menuName = "스프라이트 데이터/손님 스프라이트 데이터")]
    public class VisitorSpriteData : ScriptableObject
    {
        [Serializable]
        public class VisitorSpriteDictionary : SerializableDictionary<string, ActionData> { }

        [SerializeField]
        VisitorSpriteDictionary dictionary;

        public VisitorSpriteDictionary Dictionary => dictionary;

        [Serializable]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235")]
        public struct Data
        {
            public Sprite[] Front;
            public Sprite[] Back;
            public Sprite[] Left;
            public Sprite[] Right;
        }

        [Serializable]
        public struct ActionData
        {
            public Data Idle;
            public Data Walk;
            public Data Grab;
        }
    }
}