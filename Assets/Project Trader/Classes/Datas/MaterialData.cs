using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.Datas
{
    [Serializable]
    public class MaterialData
    {
        public int Code
        {
            get => code;
            set => code = value;
        }

        #region 인스펙터 변수
        [SerializeField]
        int code;
        #endregion
    }
}
