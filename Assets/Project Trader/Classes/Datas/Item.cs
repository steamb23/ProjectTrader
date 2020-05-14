﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader.Datas
{
    /// <summary>
    /// 아이템을 참조하는 값을 나타냅니다.
    /// </summary>
    [Serializable]
    public struct Item
    {
        /// <summary>
        /// 아이템 코드
        /// </summary>
        [UnityEngine.SerializeField]
        private int code;

        /// <summary>
        /// 갯수
        /// </summary>
        [UnityEngine.SerializeField]
        public int count;

        public int Code
        {
            get => code;
            set => code = value;
        }

        public int Count
        {
            get => count;
            set => count = value;
        }

        /// <summary>
        /// 데이터 가져오기
        /// </summary>
        public ItemData GetData() => IngameDatabase.ItemDatas[code];
    }
}
