using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GameEngine.GameTypes;

namespace GameEngine.PlayerItem
{
    public class Vik
    {
        public string Id;

        public string VillageName;
        public string PlayerName; // TODO: public Play player;

        public Resources resources;
        
        public List<Ship> ships; // TODO: build ships
        public List<Warrior> warriors;
        public List<PlayerTile> buildings;
        public Map map;

        public Vik(string id, string playername, string villagename)
        {

            Id = id;
            PlayerName = playername;
            VillageName = villagename;

            resources = new Resources();

            ships = new List<Ship>();
            warriors = new List<Warrior>();
            buildings = new List<PlayerTile>();

        }

        public void Tick()
        {
            TrainWariors();
            resources.Produce();
        }

        public void MoveShips()
        {
            foreach (Ship ship in ships)
            {
                if (ship.AttackTicksLeft > 0)
                {
                    ship.AttackTicksLeft--;
                    if (ship.AttackTicksLeft == 0) // attacking!
                    {
                        ship.IsAttacking = true;
                    }

                }
            }
        }

        // TODO: enable ship build in shipyard
        private void BuildShip()
        {
            Ship defaultShip = new Ship();
            defaultShip.ship = new GameTypes.Ship("Long serpent", 60, 400);
            ships.Add(defaultShip);
        }

 

        public void TrainWariors()
        {

            // build in barracks. One Barrack can build one unit
            var barracks = from building in buildings where building.TileType.kind == TileType.Kind.Barracks ||  building.TileType.kind == TileType.Kind.Barracks_2 select building;
            foreach (var barrack in barracks)
            {
                foreach (Warrior warrior in warriors)
                {
                    if ((warrior.type.kind == GameTypes.WarriorType.Kind.Swordman || warrior.type.kind == GameTypes.WarriorType.Kind.Axeman) && warrior.TicksLeftToCompletion > 0)
                    {
                        warrior.TicksLeftToCompletion--;
                    }
                }
                
            }


            // build in archeries
            var archeries = from building in buildings where building.TileType.kind == TileType.Kind.Archery select building;
            foreach (var archery in archeries)
            {
                foreach (Warrior warrior in warriors)
                {
                    if (warrior.type.kind == GameTypes.WarriorType.Kind.Archer && warrior.TicksLeftToCompletion > 0)
                    {
                        warrior.TicksLeftToCompletion--;
                    }
                }

            }


            // build in long house
            var longhouse = from building in buildings where building.TileType.kind==TileType.Kind.The_Long_House select building;
            foreach (var b in longhouse)
            {
                var workersToTrain = from w in warriors
                                 where w.TicksLeftToCompletion > 0 && w.type.kind == WarriorType.Kind.Worker
                                 select w;

                foreach (Warrior warrior in workersToTrain)
                {
                        warrior.TicksLeftToCompletion--;

                    if (warrior.TicksLeftToCompletion==0)
                    {
                        resources.workers++;
                    }

                }

            }


        }

        public bool StartBuilding(TileType tileToBuild, int tileId)
        {


            foreach (List<PlayerTile> tiles in map)
            {
                foreach (PlayerTile playerTile in tiles)
                {
                    if (playerTile.Id == tileId && playerTile.TileType.kind == tileToBuild.kind)
                    {
                        Debug.WriteLine("Already build here.");
                        return false;
                    }
                }
            }



            if ( tileToBuild.FoodCost <= resources.food &&
                 tileToBuild.StoneCost <= resources.stone &&
                 tileToBuild.WoodCost <= resources.wood &&
                 tileToBuild.GoldCost <= resources.gold &&
                 tileToBuild.WorkerCost <= resources.workers)
            {

                resources.food -= tileToBuild.FoodCost;
                resources.stone -= tileToBuild.StoneCost;
                resources.wood -= tileToBuild.WoodCost;
                resources.gold -= tileToBuild.GoldCost;
                resources.workers -= tileToBuild.WorkerCost;

                buildings.Add(new PlayerTile(tileToBuild, tileId));

                return true;

            }

            return false;

        }

        public bool StartTraining(WarriorType warriortoTrain)
        {
            if (warriortoTrain.FoodCost <= resources.food &&
                 warriortoTrain.StoneCost <= resources.stone &&
                 warriortoTrain.WoodCost <= resources.wood &&
                 warriortoTrain.GoldCost <= resources.gold 
                )
            {

                if (warriortoTrain.kind==WarriorType.Kind.Worker)
                {
                    int workers = (from w in warriors where w.type.kind == WarriorType.Kind.Worker select w).Count();
                    if (workers>resources.maxWorkers)
                    {
                        Debug.WriteLine("Not enough houses to create worker.");
                        return false;
                    }
                }


                resources.food -= warriortoTrain.FoodCost;
                resources.stone -= warriortoTrain.StoneCost;
                resources.wood -= warriortoTrain.WoodCost;
                resources.gold -= warriortoTrain.GoldCost;


                warriors.Add(new Warrior(warriortoTrain));

                return true;

            }

            return false;
        }
    }
}
