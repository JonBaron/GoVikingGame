

namespace GameEngine.PlayerItem
{
    public class Resources
    {
        public int food;
        public int stone;
        public int wood;
        public int gold;
        public int workers;
        public int maxWorkers; // Housing

        public int foodProduction;
        public int stoneProduction;
        public int woodProduction;
        public int goldProduction;

        public Resources()
        {
            food = 1000;
            stone = 1000;
            wood = 1000;
            gold = 100;
            workers = 4;
            maxWorkers = 5;
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
