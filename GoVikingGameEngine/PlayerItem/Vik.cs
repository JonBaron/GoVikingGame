using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            BuildBuildings();
            BuildWariors();
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

        public void BuildBuildings()
        {

            foreach (PlayerTile building in buildings)
            {
                if (building.TicksLeftToCompleteion>0)
                {
                    building.TicksLeftToCompleteion--;
                    if (building.TicksLeftToCompleteion==0) // it is complete
                    {
                        resources.UpdateProduction( building.Tile.FoodProduction, 
                                                    building.Tile.StoneProduction, 
                                                    building.Tile.WoodProduction, 
                                                    building.Tile.GoldProduction);
                    }
                }
                
            }

        }

        public void BuildWariors()
        {

            // build in barracks. One Barrack can build one unit
            var barrckcs = from building in buildings where building.Tile.Name.Equals("Barracks") select building;
            foreach (var barrack in barrckcs)
            {
                foreach (Warrior warrior in warriors)
                {
                    if (warrior.unit.kind == GameTypes.Warrior.Kind.Swordman  && warrior.TicksLeftToCompletion > 0)
                    {
                        warrior.TicksLeftToCompletion--;
                    }
                }
                
            }


            // build in archeries
            var archeries = from building in buildings where building.Tile.Name.Equals("Arhery") select building;
            foreach (var archery in archeries)
            {
                foreach (Warrior warrior in warriors)
                {
                    if (warrior.unit.kind == GameTypes.Warrior.Kind.Archer && warrior.TicksLeftToCompletion > 0)
                    {
                        warrior.TicksLeftToCompletion--;
                    }
                }

            }




        }
    }
}
