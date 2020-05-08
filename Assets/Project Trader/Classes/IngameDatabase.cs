using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTrader.Datas;

namespace ProjectTrader
{
    
    public static class IngameDatabase
    {
        public class ReadOnlyContainer<T>
        {
            T[] datas;

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
