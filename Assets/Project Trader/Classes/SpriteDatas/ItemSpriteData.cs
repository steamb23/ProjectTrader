using UnityEngine;
using System.Collections;

namespace ProjectTrader.SpriteDatas
{
    [CreateAssetMenu(fileName = "ItemSpriteData", menuName = "스프라이트 데이터/아이템 스프라이트 데이터")]
    public class ItemSpriteData : ScriptableObject
    {
        [SerializeField]
        Sprite[] sprites;

        static ItemSpriteData instance;

        static ItemSpriteData Instance
        {
            get
            {
                if (instance == null)
                {
                    Load();
                }
                return instance;
            }
        }

        public static void Load(string path = "SpriteDatas/ItemSpriteData")
        {
            // 리소스에서 불러오기
            instance = Resources.Load<ItemSpriteData>(path);
            if (instance == null)
            {
                Debug.LogError($"'{path}'에서 ItemSpriteData를 불러오는 데에 실패했습니다...");
            }
        }

        public static Sprite GetItemSprite(int code)
        {
            if (Instance == null)
            {
                Debug.LogError("ItemSpriteData의 인스턴스를 불러올 수 없습니다.");
                return null;
            }
            return Instance.sprites[code - 1];
        }
    }
}