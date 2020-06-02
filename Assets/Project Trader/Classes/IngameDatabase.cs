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
        //public static ReadOnlyContainer<MaterialData> MaterialDatas { get; private set; }

        static IngameDatabase()
        {
            Initialize();
        }

        public static void Initialize()
        {
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
            CsvData csvData;
            #region 아이템 데이터 초기화
            {
                csvData = CsvData.LoadFromResources("Datas/ItemData");
                ItemData[] datas = new ItemData[csvData.RowLength];
                for (int i = 1; i < datas.Length; i++)
                {
                    // 재료 데이터 배열 초기화
                    int[] materialCodes;
                    {
                        var raw = csvData[i, "MATERIAL_CODE"];
                        var rawSplit = raw.Split('/');
                        materialCodes = new int[rawSplit.Length];
                        for (int x = 0; x < rawSplit.Length; x++)
                        {
                            materialCodes[x] = Convert.ToInt32(rawSplit[x]);
                        }
                    }
                    int[] materialNeeds;
                    {
                        var raw = csvData[i, "MATERIAL_NEEDS"];
                        var rawSplit = raw.Split('/');
                        materialNeeds = new int[rawSplit.Length];
                        for (int x = 0; x < rawSplit.Length; x++)
                        {
                            materialNeeds[x] = Convert.ToInt32(rawSplit[x]);
                        }
                    }

                    ItemData data = new ItemData();
                    data.Code = Convert.ToInt32(csvData[i, "CODE"]);
                    data.Name = Convert.ToString(csvData[i, "NAME"]);
                    data.SellPrice = Convert.ToInt32(csvData[i, "SELL_PRICE"]);
                    data.MaterialCodes = materialCodes;
                    data.MaterialNeeds = materialNeeds;

                    datas[i] = data;
                }
                Set(datas);
            }
            #endregion

            //#region 재료 데이터 초기화
            //{
            //    csvData = CsvData.LoadFromResources("Datas/MaterialData");
            //    MaterialData[] datas = new MaterialData[csvData.RowLength];
            //    for (int i = 1; i < datas.Length; i++)
            //    {
            //        MaterialData data = new MaterialData();
            //        data.Code = Convert.ToInt32(csvData[i, "CODE"]);
            //        data.Name = Convert.ToString(csvData[i, "NAME"]);

            //        datas[i] = data;
            //    }
            //    Set(datas);
            //}
            //#endregion
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
        }

        [Obsolete("게임 초기화 단계에서만 호출해야합니다.")]
        public static void Set<T>(T[] datas)
        {
            switch (datas)
            {
                case ItemData[] itemDatas:
                    ItemDatas = itemDatas;
                    break;
                //case MaterialData[] materialDatas:
                //    MaterialDatas = materialDatas;
                //    break;

            }
        }
    }
}
