using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTrader.Datas
{
    [Serializable]
    public struct Employee
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


        #region 연산자

        public static explicit operator int(Employee item) => item.code;

        public static implicit operator Employee(int itemCode) => new Employee() { code = itemCode };

        public override bool Equals(object obj) => code.Equals(obj);

        public override int GetHashCode() => code.GetHashCode();

        public static bool operator ==(Employee left, Employee right) => left.code == right.code;

        public static bool operator !=(Employee left, Employee right) => left.code != right.code;
        #endregion
    }
}
