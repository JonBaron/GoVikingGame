using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.GameTypes
{
    public class TileType
    {

        public enum Kind
        {
            Ocean,
            Hill,
            Grass,
            Forrest,
            Mountain,
            Gold,
            Stone,
            Farm,
            The_Long_House,
            The_Ship,
            Lumber_camp,
            Quarry,
            Gold_Mine,
            Barracks,
            Barracks_2,
            Archery,
            Tower
        }


        public string FriendlyName
        {
            get { return kind.ToString().Replace("_"," "); }
        }

        public string ImageFile
        {
            get { return Name + "_1.png"; }
        }

        public string Name
        {
            get { return kind.ToString(); }
        }

        public Kind kind;
        public int FoodCost;
        public int WoodCost;
        public int StoneCost;
        public int GoldCost;
        public int WorkerCost;

        public int FoodProduction;
        public int WoodProduction;
        public int StoneProduction;
        public int GoldProduction;

        public int BuildingTime;

        public string Description;

        public Kind[] Buildable = new Kind[0];
        public WarriorType.Kind[] CreateAble = new WarriorType.Kind[0];


        #region ToString helper for testing

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string description = String.Format("{0} {1}", Name, Buildable != null ? "(Buildable)" : "");
            sb.Append(description);

            string cost = String.Format("    Food: {0} Wood: {1} Stone: {2} Gold: {3}", FoodCost, WoodCost, StoneCost,
                                        GoldCost);
            sb.AppendLine(cost);

            if (Buildable != null)
            {
                sb.Append("Buildable: ");
                foreach (Kind require in Buildable)
                {
                    sb.Append(require.ToString() + ",");
                }
            }
            sb.AppendLine();

            return sb.ToString();

        }

        #endregion

    }
}
