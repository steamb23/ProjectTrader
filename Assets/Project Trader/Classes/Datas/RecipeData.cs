using ProjectTrader.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader
{
    [Serializable]
    public class RecipeData
    {
        public int MaterialCount => materialCodes.Length;
        public MaterialData GetMaterial(int index) => IngameDatabase.MaterialDatas[materialCodes[index]];
        public ItemData GetProductItem() => IngameDatabase.ItemDatas[productItemCode];

        public int[] MaterialCodes
        {
            get => materialCodes;
            set => materialCodes = value;
        }

        public int ProductItemCode
        {
            get => productItemCode;
            set => productItemCode = value;
        }

        #region 인스펙터 변수
        [SerializeField]
        int[] materialCodes;
        [SerializeField]
        int productItemCode;
        #endregion
    }
}
