using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.GameTypes
{
    public class Ship
    {
        public string TypeName;
        public int WarriorCapacity;
        public int CargoCapacity;

        public Ship(string typeName, int warriorCapacity, int cargoCapacity)
        {
            TypeName = typeName;
            WarriorCapacity = warriorCapacity;
            CargoCapacity = cargoCapacity;
        }


    }
}
