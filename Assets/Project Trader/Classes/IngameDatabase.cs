using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTrader.Datas;
using UnityEngine;

namespace ProjectTrader.Datas
{

    public static class IngameDatabase
    {
        public class ReadOnlyContainer<T>
        {
            readonly T[] datas;

            public ReadOnlyContainer(T[] datas)
            {
                this.datas = datas;
            }

            public T this[int index]
            {
                get => datas[index];
            }

            public static implicit operator ReadOnlyContainer<T>(T[] array)
            {
                return new ReadOnlyContainer<T>(array);
            }
        }
        public static ReadOnlyContainer<ItemData> ItemDatas { get; private set; }
        public static ReadOnlyContainer<EmployeeData> EmployeeDatas { get; private set; }

        //public static ReadOnlyContainer<MaterialData> MaterialDatas { get; private set; }

        static IngameDatabase()
        {
            Initialize();
        }

        public static void Initialize()
        {
            LoadItemData();
            LoadEmployeeData();
        }

        static void LoadItemData()
        {
            CsvData csvData;

            csvData = CsvData.LoadFromResources("Datas/ItemData");
            ItemData[] datas = new ItemData[csvData.RowLength];
            for (int i = 1; i < datas.Length; i++)
            {
                ItemData data = new ItemData();
                int intResult;
                float floatResult;

                // 재료 데이터 배열 초기화
                int[] materialCodes;
                {
                    var raw = csvData[i, "MATERIAL_CODE"];
                    var rawSplit = raw.Split('/');
                    materialCodes = new int[rawSplit.Length];
                    for (int x = 0; x < rawSplit.Length; x++)
                    {
                        materialCodes[x] = int.TryParse(rawSplit[x], out var result) ? result : default;
                    }
                }
                int[] materialNeeds;
                {
                    var raw = csvData[i, "MATERIAL_NEEDS"];
                    var rawSplit = raw.Split('/');
                    materialNeeds = new int[rawSplit.Length];
                    for (int x = 0; x < rawSplit.Length; x++)
                    {
                        materialNeeds[x] = int.TryParse(rawSplit[x], out var result) ? result : default;
                    }
                }
                data.Code = int.TryParse(csvData[i, "CODE"], out intResult) ? intResult : default;
                data.Name = csvData[i, "NAME"];
                data.SellPrice = int.TryParse(csvData[i, "SELL_PRICE"], out intResult) ? intResult : default;
                data.Type = Enum.TryParse<ItemData.ItemType>(csvData[i, "TYPE"], out var itemTypeResult) ? itemTypeResult : default;
                data.Tier = int.TryParse(csvData[i, "TIER"], out intResult) ? intResult : default;
                data.CraftCost = int.TryParse(csvData[i, "CRAFT_COST"], out intResult) ? intResult : default;
                data.CraftDelay = float.TryParse(csvData[i, "CRAFT_DELAY"], out floatResult) ? floatResult : default;
                data.BuyType = Enum.TryParse<ItemData.BuyFlags>(csvData[i, "BUY_TYPE"], out var buyFlagsResult) ? buyFlagsResult : default;
                data.ShopPrice = (
                    int.TryParse(csvData[i, "GUILD_SHOP_PRICE_MIN"], out intResult) ? intResult : default,
                    int.TryParse(csvData[i, "GUILD_SHOP_PRICE_MAX"], out intResult) ? intResult : default
                    );
                data.WitchShopPrice = (
                    int.TryParse(csvData[i, "WITCH_PRICE_MIN"], out intResult) ? intResult : default,
                    int.TryParse(csvData[i, "WITCH_PRICE_MAX"], out intResult) ? intResult : default
                    );
                data.MaterialCodes = materialCodes;
                data.MaterialNeeds = materialNeeds;
                data.Awareness = int.TryParse(csvData[1, "AWARENESS"], out intResult) ? intResult : default;

                datas[i] = data;
            }
            ItemDatas = datas;
        }

        static void LoadEmployeeData()
        {
            CsvData csvData;

            csvData = CsvData.LoadFromResources("Datas/EmployeeData");
            EmployeeData[] datas = new EmployeeData[csvData.RowLength];
            for (int i = 1; i < datas.Length; i++)
            {
                EmployeeData data = new EmployeeData();
                int intResult;
                //float floatResult;

                data.Code = int.TryParse(csvData[i, "CODE"], out intResult) ? intResult : default;
                data.Name = csvData[i, "NAME"];
                data.Gender = Enum.TryParse<EmployeeData.GenderType>(csvData[i, "CODE"], out var genderResult) ? genderResult : default;
                data.Charisma = int.TryParse(csvData[i, "CHA"], out intResult) ? intResult : default;
                data.Intelligent = int.TryParse(csvData[i, "INT"], out intResult) ? intResult : default;
                data.Dexterity = int.TryParse(csvData[i, "DEX"], out intResult) ? intResult : default;
                data.Cost = int.TryParse(csvData[i, "COST"], out intResult) ? intResult : default;
                data.Tier = int.TryParse(csvData[i, "TIER"], out intResult) ? intResult : default;
                data.HairSpriteCode = int.TryParse(csvData[i, "HAIR"], out intResult) ? intResult : default;
                data.SkinSpriteCode = int.TryParse(csvData[i, "SKIN"], out intResult) ? intResult : default;
                data.HatSpriteCode = int.TryParse(csvData[i, "HAT"], out intResult) ? intResult : default;
                data.CostumeSpriteCode = int.TryParse(csvData[i, "COSTUME"], out intResult) ? intResult : default;

                datas[i] = data;
            }
            EmployeeDatas = datas;
        }

        //public static void Set<T>(T[] datas)
        //{
        //    switch (datas)
        //    {
        //        case ItemData[] itemDatas:
        //            ItemDatas = itemDatas;
        //            break;
        //            //case MaterialData[] materialDatas:
        //            //    MaterialDatas = materialDatas;
        //            //    break;

        //    }
        //}
    }
}
