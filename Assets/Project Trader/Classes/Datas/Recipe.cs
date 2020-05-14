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
    public class Recipe
    {
        public Recipe()
        {

        }

        public Recipe(ItemData productItemData)
        {
            ProductItemData = productItemData;
        }

        public Recipe(int productItemCode)
        {
            ProductItemCode = productItemCode;
        }

        public ItemData ProductItemData
        {
            get => this.productItemData;
            set
            {
                this.productItemData = value;
                materialDatas = new MaterialData[productItemData.MaterialCodes.Length];
                for(int i = 0; i < materialDatas.Length; i++)
                {
                    materialDatas[i] = value.GetMaterial(i);
                }
            }
        }

        public MaterialData[] MaterialDatas => materialDatas;

        public int ProductItemCode
        {
            get => ProductItemData.Code;
            set => ProductItemData = IngameDatabase.ItemDatas[value];
        }

        #region 인스펙터 변수
        [SerializeField]
        ItemData productItemData;
        [SerializeField]
        MaterialData[] materialDatas;
        #endregion
    }
}
