using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectTrader.Datas
{
    [Serializable]
    public class EmployeeData
    {
        public enum GenderType
        {
            Male,
            Female
        }

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
        public GenderType Gender
        {
            get => this.gender;
            set => this.gender = value;
        }
        public int Charisma
        {
            get => this.charisma;
            set => this.charisma = value;
        }
        public int Intelligent
        {
            get => this.intelligent;
            set => this.intelligent = value;
        }
        public int Dexterity
        {
            get => this.dexterity;
            set => this.dexterity = value;
        }
        public int Cost
        {
            get => this.cost;
            set => this.cost = value;
        }
        public int Tier
        {
            get => this.tier;
            set => this.tier = value;
        }
        public int HairSpriteCode
        {
            get => this.hairSpriteCode;
            set => this.hairSpriteCode = value;
        }
        public int SkinSpriteCode
        {
            get => this.skinSpriteCode;
            set => this.skinSpriteCode = value;
        }
        public int HatSpriteCode
        {
            get => this.hatSpriteCode;
            set => this.hatSpriteCode = value;
        }
        public int CostumeSpriteCode
        {
            get => this.costumeSpriteCode;
            set => this.costumeSpriteCode = value;
        }


        #region 인스펙터 변수
        [SerializeField] int code;
        [SerializeField] string name;
        [SerializeField] GenderType gender;
        [SerializeField] int charisma;
        [SerializeField] int intelligent;
        [SerializeField] int dexterity;
        [SerializeField] int cost;
        [SerializeField] int tier;
        [SerializeField] int hairSpriteCode;
        [SerializeField] int skinSpriteCode;
        [SerializeField] int hatSpriteCode;
        [SerializeField] int costumeSpriteCode;
        #endregion
    }
}
