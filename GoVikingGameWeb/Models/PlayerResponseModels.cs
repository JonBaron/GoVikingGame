
using GameEngine.GameTypes;

namespace GoVikingGame.Models
{
    public class PlayerResponseModels
    {

        public class BuildModel
        {
            public TileType.Kind Kind;
            public int BuildTimeTicks;
            public bool Ok;
            public string Response;
        }

        public class CreateModel
        {
            public int TrainingTicks;
            public bool Ok;
            public string Response;
        }

        public class MessageModel
        {
            public string response;
        }

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

        }


        

    }
}