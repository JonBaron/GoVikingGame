using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;
using Warrior = GameEngine.GameTypes.Warrior;

namespace GameEngine
{
    public class GameInititializer
    {

        public static List<Warrior> InitWarriorTypes()
        {
            List<Warrior> types = new List<GameTypes.Warrior>();

            // TODO: Rip units from aoe2
            types.Add(new Warrior() { kind = Warrior.Kind.Worker, Description = "Workers is used for farming, lumber cutting and other work. They are lousy fighers, and usually dies if attacked.", FoodCost = 100, AttackBonus = 0, Strength = 1 });
            types.Add(new Warrior() { kind = Warrior.Kind.Axeman, Description = "Agressive but poorly equitet warrior.", FoodCost = 100, AttackBonus = 1.25, Strength = 3});
            types.Add(new Warrior() { kind = Warrior.Kind.Archer, Description = "With their bow and arrow they are good defenders of the village", FoodCost = 100, WoodCost = 50, DefenceBonus = 1.25, Strength = 3 });
            types.Add(new Warrior() { kind = Warrior.Kind.Swordman, Description = "With swords and armor these warriors are good allround warriors.",FoodCost = 200, GoldCost = 50, AttackBonus = 1.25, Strength = 5 });

            return types;

        }

        public static List<Tile> InitBuildingTypes()
        {
            List<Tile> gameTileTypes = new List<Tile>();
            
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Ocean});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Mountain });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.The_Ship });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.The_Long_House, CreateAble = new Warrior.Kind[] { Warrior.Kind.Worker }});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Hill,           Buildable = new Tile.Kind[] { Tile.Kind.Tower}});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Grass,          Buildable = new Tile.Kind[] { Tile.Kind.Barracks, Tile.Kind.Archery, Tile.Kind.Farm, Tile.Kind.Tower  } });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Forrest,        Buildable = new Tile.Kind[] { Tile.Kind.Lumber_camp }});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Gold,           Buildable = new Tile.Kind[] { Tile.Kind.Gold_Mine } });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Stone,          Buildable = new Tile.Kind[] { Tile.Kind.Quarry }  });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Farm,           WoodCost  = 200, WorkerCost = 2, FoodProduction = 10, Description = "Farmers create 10 food per tick."} );
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Lumber_camp,    StoneCost = 200, WoodProduction = 10, WorkerCost = 2 , Description = "Workers create lumber for construction."});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Quarry,         WoodCost  = 300, StoneProduction = 10, WorkerCost = 3, Description = "Works creae stone blocks for buildings."});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Gold_Mine,      StoneCost = 200, WoodCost = 200, WorkerCost = 4, GoldProduction = 10 });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Barracks,       StoneCost = 200, WoodCost = 200, Buildable = new Tile.Kind[] { Tile.Kind.Barracks_2}, CreateAble = new Warrior.Kind[] { Warrior.Kind.Swordman}, Description = "A building where warriors are trained."});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Barracks_2,     StoneCost = 500, WoodCost = 500, CreateAble = new Warrior.Kind[] { Warrior.Kind.Axeman, Warrior.Kind.Swordman } });
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Archery,        StoneCost = 200, WoodCost = 200, CreateAble = new Warrior.Kind[] { Warrior.Kind.Archer}, Description = "Archers are trained and equited here."});
            gameTileTypes.Add(new Tile() { kind = Tile.Kind.Tower,          StoneCost = 500, WoodCost = 200, Description = "Increases the defence of the village."});

            return gameTileTypes;
        }


        public static Map CreateMap(int[] binMap, List<Tile> gameTileTypes)
        {


            int size = (int)Math.Sqrt(binMap.Length);

            Map map = new Map();


            int i=0;
            for (int y = 0;y < size; y++)
            {
//                map[y] = new List<Tile>();
                map.Add(new List<PlayerTile>());
                for (int x = 0; x < size; x++)
                {

                    Tile t = null;

                    int binId = binMap[i];
                    switch (binId)
                    {
                        case 0:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Ocean));
                            break;
                        case 1:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Grass));
                            break;
                        case 3:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Forrest));
                            break;
                        case 4:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Hill));
                            break;
                        case 5:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Mountain));
                            break;
                        case 7:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Stone));
                            break;
                        case 8:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Gold));
                            break;
                        case 100:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.The_Long_House));
                            break;
                        case 101:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Farm));
                            break;
                        case 102:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Gold_Mine));
                            break;
                        case 103:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Quarry));
                            break;
                        case 104:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Barracks));
                            break;
                        case 105:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Archery));
                            break;
                        case 106:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.Lumber_camp));
                            break;
                        case 50:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == Tile.Kind.The_Ship));
                            break;

                        default:
                            throw new Exception(String.Format("tile #{0} not created!", binId));
                            

                    }


                    if (t==null)
                    {
                        throw new Exception("No tile with #"+binId+" i game!");
                    }


                    map[y].Add(new PlayerTile(t,i));

                    i++;
                }
            }


            return map;


        }


        public static int[] map1 =
                          {
                            5,5,5,5,5,5,5,5,5,5,5,5,5,5,
                            5,4,7,5,4,4,4,5,4,4,3,4,4,5,
                            5,8,1,1,1,1,3,1,1,1,3,1,4,5,
                            5,4,1,0,1,1,1,1,1,7,3,1,4,5,
                            5,0,0,0,1,1,3,1,1,1,3,3,4,5,
                            5,4,1,1,1,1,1,1,1,7,3,3,3,5,
                            5,4,3,1,1,1,1,1,1,1,3,1,3,5,
                            5,4,1,1,3,1,1,100,1,1,3,1,1,5,
                            5,4,3,1,3,1,1,1,1,1,3,1,3,5,
                            5,8,3,1,3,1,0,0,1,1,3,1,4,5,
                            5,4,1,1,3,0,0,0,1,1,3,3,4,5,
                            5,4,1,1,3,0,0,0,1,1,1,3,7,5,
                            5,0,0,0,0,0,0,0,0,0,1,0,0,5,
                            0,0,0,0,0,0,0,0,0,0,0,0,0,0
                          };

        public static int[] map2 =
                          {
                            5,5,5,5,4,5,5,5,5,5,5,5,5,5,5,
                            5,4,7,5,3,4,4,4,5,4,4,3,4,4,5,
                            5,8,1,1,1,1,1,3,1,1,1,3,1,4,5,
                            4,4,4,4,4,4,1,1,1,1,7,3,102,4,5,
                            4,5,1,1,106,1,1,3,1,1,1,3,3,4,5,
                            5,4,1,1,1,1,1,1,1,1,7,3,3,3,5,
                            5,4,3,1,1,1,1,1,101,1,104,3,1,3,5,
                            4,4,1,1,1,3,1,1,100,1,1,3,1,1,5,
                            5,4,3,1,1,3,1,1,101,102,1,3,1,3,5,
                            5,4,3,1,1,3,1,1,1,1,1,3,1,3,5,
                            5,8,3,1,1,3,1,50,0,1,1,3,1,4,5,
                            5,4,1,1,1,3,0,0,0,1,1,3,3,4,5,
                            5,4,1,1,1,3,0,0,0,1,1,1,3,7,5,
                            5,0,0,0,1,1,0,0,0,1,0,1,0,0,5,
                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                          };



        public static int[] map3 =
                          {
                            5,5,5,5,4,5,5,5,5,5,5,5,5,5,5,
                            5,4,7,5,3,4,4,4,5,4,4,3,4,4,5,
                            5,8,1,1,1,1,1,3,1,1,1,3,1,4,5,
                            5,4,1,1,106,1,1,1,1,1,7,3,105,4,5,
                            5,8,1,1,1,1,1,3,1,1,1,3,3,4,5,
                            5,4,1,1,1,1,1,1,1,105,7,3,3,3,5,
                            5,4,3,1,1,1,1,1,101,1,104,3,1,3,5,
                            5,4,1,1,1,3,1,1,100,1,1,3,1,1,5,
                            5,4,3,104,01,3,1,1,101,102,1,3,1,3,5,
                            5,4,3,1,1,0,1,1,1,1,1,3,1,3,5,
                            5,8,3,1,1,0,0,50,0,1,1,3,1,4,5,
                            5,4,1,1,1,3,0,0,0,1,1,3,3,4,5,
                            5,4,1,1,1,3,0,0,0,1,1,1,3,7,5,
                            5,0,0,0,0,0,0,0,0,0,0,1,0,0,5,
                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                          };


    }

  
}
