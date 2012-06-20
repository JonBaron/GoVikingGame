using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.GameTypes
{
    public class Warrior
    {
        public enum Kind
        {
            Worker,
            Swordman,
            Axeman,
            Archer
        }

        public string Name
        {
            get { return kind.ToString(); }
        }

        public object FriendlyName
        {
            get { return kind.ToString().Replace("_"," "); }   
        }

        public Kind kind;
        public int Strength;
        public int FoodCost;
        public int WoodCost;
        public int GoldCost;
        public double AttackBonus;
        public double DefenceBonus;
        public string Description;
    }
}
