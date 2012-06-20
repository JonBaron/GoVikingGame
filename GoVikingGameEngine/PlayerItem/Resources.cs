using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.PlayerItem
{
    public class Resources
    {
        public int food;
        public int stone;
        public int wood;
        public int gold;
        public int workers;

        public int foodProduction;
        public int stoneProduction;
        public int woodProduction;
        public int goldProduction;

        public Resources()
        {
            food = 100;
            stone = 100;
            wood = 100;
            gold = 0;
            workers = 5;
        }

        public void Produce()
        {

            food += foodProduction;
            stone += stoneProduction;
            wood += woodProduction;
            gold += goldProduction;
        }

        public void UpdateProduction(int food, int stone, int wood, int gold)
        {
            foodProduction = +food;
            stoneProduction = +stone;
            woodProduction = +wood;
            goldProduction = +gold;
        }


    }
}
