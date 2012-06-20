using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.PlayerItem;


namespace GameEngine
{
    public class Game
    {

        public List<PlayerItem.Vik> Viks; // player villages .. i en "Vik"
        public List<GameTypes.Warrior> WarriorTypes;
        public List<GameTypes.Tile> TileTypes;

        public Game()
        {
            
            WarriorTypes = GameInititializer.InitWarriorTypes();
            TileTypes = GameInititializer.InitBuildingTypes();
            
            Viks = new List<PlayerItem.Vik>();

        }

        public PlayerItem.Vik AddPlayer(string id, string nickName, string villageName, Map map)
        {
            PlayerItem.Vik vik = new PlayerItem.Vik(id, nickName, villageName);

            vik.Map = map; 

            Viks.Add(vik);
            return vik;

        }

        public void Tick()
        {

            foreach (PlayerItem.Vik vik in Viks)
            {
                vik.Tick();
                vik.MoveShips();
            }

            CalculateBattlesForArrivedShips();

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
    }
}
