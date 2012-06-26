using System.Collections.Generic;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;

namespace GoVikingGameWeb.Models
{
    public class GameViewModel
    {
        public string PlayerId;
        public int GameViksCount;
        public Map UserSessionMap;
        public Resources Resources;
        public List<TileType> TileTypes;
        public List<WarriorType> WarriorTypes;
        public int MapRenderWidth;
    }
}