using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Collections;

namespace ProjectTrader
{
    [CreateAssetMenu(fileName = "CharacterSpriteData", menuName = "캐릭터 스프라이트 데이터")]
    public class CharacterSpriteData : ScriptableObject
    {
        [Serializable]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235")]
        public struct Data
        {
            public Sprite[] front;
            public Sprite[] back;
            public Sprite[] left;
            public Sprite[] right;
        }

        [SerializeField]
        private Data idle;
        [SerializeField]
        private Data walk;
        [SerializeField]
        private Data grab;

        public Data Idle => idle;
        public Data Walk => walk;
        public Data Grab => grab;
    }

}