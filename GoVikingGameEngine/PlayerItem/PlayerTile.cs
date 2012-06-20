using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//


namespace GameEngine.PlayerItem
{
    public class PlayerTile
    {
        public GameTypes.Tile Tile;
        public int Id;
        public int TicksLeftToCompleteion;
        public int TicksLeftToBurnedDown;


        public PlayerTile(GameTypes.Tile tile , int id)
        {
            Tile = tile;
            Id = id;
        }
    }
}
