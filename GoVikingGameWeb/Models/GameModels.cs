using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;

namespace GoVikingGameWeb.Models
{
    public class GameModel
    {

        public string PlayerId;
        public int GameViksCount;
        public Map UserSessionMap;
        public Resources Resources;
        public List<Tile> TileTypes;
        public List<GameEngine.GameTypes.Warrior> WarriorTypes;
        public int MapRenderWidth;


    }
}