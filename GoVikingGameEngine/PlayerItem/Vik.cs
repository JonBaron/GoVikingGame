using System;
using System.Collections.Generic;
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
        public Map Map;

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
            BuildOnBuildings();
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

        public void BuildOnBuildings()
        {

            foreach (PlayerTile building in buildings)
            {
                if (building.TicksLeftToCompleteion>0)
                {
                    building.TicksLeftToCompleteion--;
                    if (building.TicksLeftToCompleteion==0) // it is complete
                    {
                        resources.UpdateProduction( building.TileType.FoodProduction, 
                                                    building.TileType.StoneProduction, 
                                                    building.TileType.WoodProduction, 
                                                    building.TileType.GoldProduction);

                        if (building.TileType.kind.Equals(TileType.Kind.House))
                        {
                            resources.maxWorkers += 5;
                        }

                    }
                }
                
            }

        }

        public void TrainWariors()
        {

            // build in barracks. One Barrack can build one unit
            var barrckcs = from building in buildings where building.TileType.Name.Equals("Barracks") select building;
            foreach (var barrack in barrckcs)
            {
                foreach (Warrior warrior in warriors)
                {
                    if (warrior.type.kind == GameTypes.WarriorType.Kind.Swordman  && warrior.TicksLeftToCompletion > 0)
                    {
                        warrior.TicksLeftToCompletion--;
                    }
                }
                
            }


            // build in archeries
            var archeries = from building in buildings where building.TileType.Name.Equals("Arhery") select building;
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




        }

        public bool StartBuilding(TileType tileToBuild, int tileId)
        {
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
                 warriortoTrain.GoldCost <= resources.gold)
            {

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
