using System;
using System.Collections.Generic;
using GameEngine;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoVikingGameEngineTest
{
    [TestClass]
    public class GameInitTest
    {
        private Game game;

        public GameInitTest()
        {
            game = new Game();
        }

        [TestMethod]
        public void GameCreatedTest()
        {


            Assert.IsNotNull(game);
            Assert.IsNotNull(game.TileTypes);

            Console.WriteLine("PlayerTile types:");
            game.TileTypes.ForEach(e => Console.WriteLine(e.ToString()));


        }


        [TestMethod]
        public void MapTest()
        {
            Map defaultMap = GameInititializer.CreateMap(GameInititializer.defaultMap,  game.TileTypes);

            int y = 0;
            int x = 0;

            foreach (List<PlayerTile> tiles in defaultMap)
            {
                x = 0;
                foreach (PlayerTile tile in tiles)
                {
   
                    Assert.IsNotNull(tile);

                    Console.Write(tile.TileType.Name.PadRight(15).Substring(0,10) + " | ");
                    x++;
                }
                y++;
                Console.WriteLine();
            }

            

        }

        [TestMethod]
        public void AddPlayersTest()
        {
            game.AddPlayer("asdf", "Test1", "No name", GameInititializer.CreateMap(GameInititializer.defaultMap,game.TileTypes));
        }

        [TestMethod]
        public void TryATick()
        {
            game.Tick();
        }





    }
}
