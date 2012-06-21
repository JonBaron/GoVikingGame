
using GameEngine.GameTypes;

namespace GameEngine.PlayerItem
{
    public class Warrior
    {
        public Warrior(WarriorType type)
        {
            this.type = type;
        }

        public WarriorType type;
        public int TicksLeftToCompletion;
        public bool Away;
        public bool Alive;
    }
}
