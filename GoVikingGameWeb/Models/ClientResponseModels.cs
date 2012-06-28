

using System;
using GameEngine.GameTypes;

namespace GoVikingGame.Models
{
    public class ClientResponseModels
    {

        public class BuildModel
        {
            public string ImageFile;
            public int BuildTimeTicks;
            public bool Ok;
            public string Response;
            public int TileId;
            public TileType.Kind Kind;
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

            public int maxWorkers;

            public int foodProduction;
            public int stoneProduction;
            public int woodProduction;
            public int goldProduction;

            public DateTime nextTick;

        }


        

    }
}