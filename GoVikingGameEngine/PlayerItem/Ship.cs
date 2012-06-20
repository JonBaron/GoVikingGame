using System.Collections.Generic;

namespace GameEngine.PlayerItem
{
    public class Ship
    {
        public GameTypes.Ship ship;
        public string AttackPlayerId;
        public int? AttackTicksLeft;
        public bool IsAttacking;
        public List<Warrior> UnitsAboard;

        public int foodLoot;
        public int woodLoot;
        public int stoneLoot;
        public int goldLoot;

        public void InitPLayerAttack(string targetId, List<Warrior> warriors)
        {
            AttackTicksLeft = 3;
            AttackPlayerId = targetId;
        }
        
    }
}
