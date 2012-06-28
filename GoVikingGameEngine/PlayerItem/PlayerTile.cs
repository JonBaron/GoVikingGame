using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//


namespace GameEngine.PlayerItem
{
    public class PlayerTile
    {
        public GameTypes.TileType TileType;
        public int Id;
        public int TicksLeftToCompleteion;
        public int TicksLeftToBurnedDown;


        public PlayerTile(GameTypes.TileType tileType , int id)
        {
            TileType = tileType;
            Id = id;
            TicksLeftToCompleteion = tileType.BuildingTime;

        }
    }
}
