using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectTrader
{

    [Serializable]
    public class PlayData
    {
        //재화,가게 등급,인지도,가게이름
        public int money;
        public int level;
        public float famous;
        public string shopName;
        public PlayData() { }
        public PlayData(int m, int l, float f, string sN)
        {
            money = m;
            level = l;
            famous = f;
            shopName = sN;
        }

        public void UseMoney(int mon)
        {
            if (money + mon > 0)
                money += mon;
        }
        public void UseLevel(int lev)
        {
            level += lev;
        }
        public void UseFamous(int fam)
        {
            if (famous + fam > 0)
                famous += fam;
            else
                famous = 0;
        }
        public void SetShopName(string name)
        {
            shopName = name;
        }
        public void SetMoney(int mon)
        {
            money = mon;
        }
        public void SetLevel(int lev)
        {
            level = lev;
        }
        public void SetFamous(float fam)
        {
            famous = fam;
        }

        public void PrintPlayData()
        {
            Debug.Log(money);
            Debug.Log(famous);
            Debug.Log(level);
            Debug.Log(shopName);
        }
    }
}
