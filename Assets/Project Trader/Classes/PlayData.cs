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
        public int money { set; get; }
        public int level { set; get; }
        public int maxTired { set; get; }
        public int tired { set; get; }
        public float famous { set; get; }
        public string shopName { set; get; }
        public int date { set; get; }
        public PlayData() {}
        public PlayData(int m, int l, int t,float f, string sN)
        {
            money = m;
            level = l;
            famous = f;
            maxTired = t;
            tired = t;
            shopName = sN;
        }
    }
}