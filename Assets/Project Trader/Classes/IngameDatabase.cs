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
        public static ReadOnlyContainer<MaterialData> MaterialDatas { get; private set; }

        static IngameDatabase()
        {
            Initialize();
        }

        public static void Initialize()
        {
            var itemCsvData = CsvData.LoadFromResources("Datas/ItemData");
            ItemData[] itemDatas = new ItemData[itemCsvData.RowLength];
            for(int i = 1; i < itemDatas.Length; i++)
            {
                ItemData itemData = new ItemData();
                itemData.Code = Convert.ToInt32(itemCsvData[i, "CODE"]);
                itemData.SellPrice = Convert.ToInt32(itemCsvData[i, "SELL_PRICE"]);

                itemDatas[i] = itemData;
            }
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
            Set(itemDatas);
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
            }
        }
    }
}
