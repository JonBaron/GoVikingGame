using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;


namespace GameEngine
{
    public class Game
    {

        public List<Vik> Viks; // player villages .. i en "Vik"
        public List<WarriorType> WarriorTypes;
        public List<TileType> TileTypes;
        private int _tickNumber = 0;
        public TimeSpan TickTime = new TimeSpan(0,0,10);
        private DateTime _nextTick = DateTime.Now; 

        public Game()
        {
            
            WarriorTypes = GameInititializer.InitWarriorTypes();
            TileTypes = GameInititializer.InitBuildingTypes();
            
            Viks = new List<Vik>();

        }

        public int TickNumber
        {
            get { return _tickNumber; }
        }

        public DateTime NextTick
        {
            get { return _nextTick; }
        }

        public Vik AddPlayer(string id, string nickName, string villageName, Map map)
        {
            Vik vik = new Vik(id, nickName, villageName);

            vik.map = map; 

            Viks.Add(vik);
            return vik;

        }

        public void Tick()
        {

            DateTime tickstart = DateTime.Now;


            foreach (Vik vik in Viks)
            {
                BuildOnBuildings(vik);
                vik.Tick();
                vik.MoveShips();
            }

            CalculateBattlesForArrivedShips();

            _tickNumber = TickNumber + 1;

            TimeSpan waitForIt = TickTime - (DateTime.Now - tickstart);
            Debug.WriteLine("Tick took:" +(DateTime.Now - tickstart));
            _nextTick = DateTime.Now + waitForIt;
            if (waitForIt.Ticks > 0)
            {
                Thread.Sleep(waitForIt);
            }

        }


        private void CalculateBattlesForArrivedShips()
        {

            foreach (PlayerItem.Vik vik in Viks)
            {
                var attackingShips = from ship in vik.ships where ship.IsAttacking select ship;

                foreach (PlayerItem.Ship attackingShip in attackingShips)
                {
                    PlayerItem.Vik defender = (from dvik in Viks where dvik.Id == attackingShip.AttackPlayerId select dvik).FirstOrDefault();
                    CalculateBattle(attackingShip, defender);
                }
            }
        }

        private void CalculateBattle(PlayerItem.Ship attackerShip, PlayerItem.Vik defenderVik)
        {
            List<PlayerItem.Warrior>.Enumerator defenderWarriors = defenderVik.warriors.GetEnumerator();

            foreach (PlayerItem.Warrior attacker in attackerShip.UnitsAboard)
            {
                PlayerItem.Warrior defender = defenderWarriors.Current;

                Random god = new Random();

                if (defenderWarriors.MoveNext())
                {

                    if (god.Next(2) == 0)
                    {
                        defender.Alive = false;
                    }
                    else
                    {
                        attacker.Alive = false;
                    }
                }


                if (attacker.Alive)
                {
                    
                    int foodLoot = god.Next(100);
                    if (defenderVik.resources.food<foodLoot)
                        foodLoot = defenderVik.resources.food;
                    defenderVik.resources.food -= foodLoot;
                    attackerShip.foodLoot += foodLoot;

                    int woodLoot = god.Next(100);
                    if (defenderVik.resources.wood<woodLoot)
                        woodLoot = defenderVik.resources.wood;
                    defenderVik.resources.wood -= woodLoot;
                    attackerShip.woodLoot += woodLoot;

                    int stoneLoot = god.Next(100);
                    if (defenderVik.resources.stone < stoneLoot)
                        stoneLoot = defenderVik.resources.stone;
                    defenderVik.resources.stone -= stoneLoot;
                    attackerShip.stoneLoot += stoneLoot;

                    int goldLoot = god.Next(100);
                    if (defenderVik.resources.gold < goldLoot)
                        goldLoot = defenderVik.resources.gold;
                    defenderVik.resources.gold -= goldLoot;
                    attackerShip.goldLoot += goldLoot;

                }


            }
        }


        public void BuildOnBuildings(Vik vik)
        {

            foreach (PlayerTile building in vik.buildings)
            {
                if (building.TicksLeftToCompleteion > 0)
                {
                    building.TicksLeftToCompleteion--;
                }
                else
                {
                    // BUILDING COMPLETE

                    foreach (List<PlayerTile> tiles in vik.map)
                    {
                        foreach (PlayerTile playerTile in tiles)
                        {
                            if (playerTile.Id == building.Id)
                            {
                                playerTile.TileType =
                                    (from t in TileTypes where t.kind == building.TileType.kind select t).First();
                                Debug.WriteLine("build new building :" + playerTile.TileType.ToString());
                                break;
                            }
                        }
                    }




                    vik.resources.UpdateProduction(building.TileType.FoodProduction,
                                               building.TileType.StoneProduction,
                                               building.TileType.WoodProduction,
                                               building.TileType.GoldProduction);

                    if (building.TileType.kind.Equals(TileType.Kind.House))
                    {
                        vik.resources.maxWorkers += 5;
                    }



                }

            }

        }
    }
}
