using ProjectTrader.SpriteDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.Datas
{
    [Serializable]
    public class ItemData
    {
        public enum ItemType
        {
            None = 0,
            Material = 1,
            Craftable = 2
        }

        [Flags]
        public enum BuyFlags : int
        {
            None = 0,
            Craft = 1,
            GuildShop = 2,
            WitchShop = 4
        }

        public Sprite GetSprite() => ItemSpriteData.GetItemSprite(code);

        /// <summary>
        /// 제작에 필요한 재료의 데이터를 가져옵니다.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        //public ItemData GetMaterial(int index) => IngameDatabase.MaterialDatas[materialCodes[index]];
        public ItemData GetMaterial(int index) => IngameDatabase.ItemDatas[materialCodes[index]];

        public int Code
        {
            get => this.code;
            set => this.code = value;
        }
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }
        public ItemType Type
        {
            get => this.type;
            set => this.type = value;
        }
        public int SellPrice
        {
            get => this.sellPrice;
            set => this.sellPrice = value;
        }
        public int Tier
        {
            get => this.tier;
            set => this.tier = value;
        }
        public int[] MaterialCodes
        {
            get => this.materialCodes;
            set => this.materialCodes = value;
        }
        public int[] MaterialNeeds
        {
            get => this.materialNeeds;
            set => this.materialNeeds = value;
        }
        public int CraftCost
        {
            get => this.craftCost;
            set => this.craftCost = value;
        }
        public BuyFlags BuyType
        {
            get => this.buyType;
            set => this.buyType = value;
        }
        public int ShopPrice
        {
            get => this.shopPrice;
            set => this.shopPrice = value;
        }
        public int WitchShopPrice
        {
            get => this.witchShopPrice;
            set => this.witchShopPrice = value;
        }
        public float CraftDelay
        {
            get => this.craftDelay;
            set => this.craftDelay = value;
        }

        #region 인스펙터 변수
        [SerializeField]
        int code;
        [SerializeField]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2235")]
        string name;
        [SerializeField]
        ItemType type;
        [SerializeField]
        int sellPrice;
        [SerializeField]
        int tier;
        [SerializeField]
        int[] materialCodes;
        [SerializeField]
        int[] materialNeeds;
        [SerializeField]
        int craftCost;
        [SerializeField]
        BuyFlags buyType;
        [SerializeField]
        int shopPrice;
        [SerializeField]
        int witchShopPrice;
        [SerializeField]
        float craftDelay;
        #endregion
    }
}
