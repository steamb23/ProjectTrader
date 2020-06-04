using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader.Datas
{
    [Serializable]
    struct Employee
    {
        /// <summary>
        /// 아이템 코드
        /// </summary>
        [UnityEngine.SerializeField]
        private int code;

        public int Code
        {
            get => code;
            set => code = value;
        }

        /// <summary>
        /// 데이터 가져오기
        /// </summary>
        public ItemData GetData() => IngameDatabase.ItemDatas[code];
    }
}
