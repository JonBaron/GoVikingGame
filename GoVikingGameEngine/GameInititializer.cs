using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;

namespace GameEngine
{
    public class GameInititializer
    {

        public static List<WarriorType> InitWarriorTypes()
        {
            List<WarriorType> types = new List<GameTypes.WarriorType>();

            // TODO: Rip units from aoe2
            types.Add(new WarriorType() { kind = WarriorType.Kind.Worker,   TrainingTime = 3, Description = "Workers is used for farming, lumber cutting and other work. They are lousy fighers, and usually dies if attacked.", FoodCost = 100, AttackBonus = 0, Strength = 1 });
            types.Add(new WarriorType() { kind = WarriorType.Kind.Axeman,   TrainingTime = 3, Description = "Agressive but poorly equitet warrior.", FoodCost = 100, AttackBonus = 1.25, Strength = 3 });
            types.Add(new WarriorType() { kind = WarriorType.Kind.Archer,   TrainingTime = 3, Description = "With their bow and arrow they are good defenders of the village", FoodCost = 100, WoodCost = 50, DefenceBonus = 1.25, Strength = 3 });
            types.Add(new WarriorType() { kind = WarriorType.Kind.Swordman, TrainingTime = 3, Description = "With swords and armor these warriors are good allround warriors.", FoodCost = 200, GoldCost = 50, AttackBonus = 1.25, Strength = 5 });

            return types;

        }

        public static List<TileType> InitBuildingTypes()
        {
            List<TileType> gameTileTypes = new List<TileType>();
            
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Ocean       });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Mountain    });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.The_Ship    });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.The_Long_House, CreateAble = new WarriorType.Kind[] { WarriorType.Kind.Worker }});
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Hill,           Buildable = new TileType.Kind[] { TileType.Kind.Tower}});
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Grass,          Buildable = new TileType.Kind[] { TileType.Kind.Barracks, TileType.Kind.Archery, TileType.Kind.Farm, TileType.Kind.Tower , TileType.Kind.House } });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Forrest,        Buildable = new TileType.Kind[] { TileType.Kind.Lumber_camp }});
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Gold,           Buildable = new TileType.Kind[] { TileType.Kind.Gold_Mine } });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Stone,          Buildable = new TileType.Kind[] { TileType.Kind.Quarry }  });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.House,          BuildingTime = 1, WoodCost = 100, Description = "Housing for workers." });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Farm,           BuildingTime = 1, WoodCost  = 200, WorkerCost = 2, FoodProduction = 10, Description = "Farmers create 10 food per tick."} );
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Lumber_camp,    BuildingTime = 1, StoneCost = 200, WoodProduction = 10, WorkerCost = 2, Description = "Workers create lumber for construction." });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Quarry,         BuildingTime = 1, WoodCost = 300, StoneProduction = 10, WorkerCost = 3, Description = "Works creae stone blocks for buildings." });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Gold_Mine,      BuildingTime = 1, StoneCost = 200, WoodCost = 200, WorkerCost = 4, GoldProduction = 10 });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Barracks,       BuildingTime = 1, StoneCost = 200, WoodCost = 200, Buildable = new TileType.Kind[] { TileType.Kind.Barracks_2 }, CreateAble = new WarriorType.Kind[] { WarriorType.Kind.Swordman }, Description = "A building where warriors are trained." });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Barracks_2,     BuildingTime = 1, StoneCost = 500, WoodCost = 500, CreateAble = new WarriorType.Kind[] { WarriorType.Kind.Axeman, WarriorType.Kind.Swordman } });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Archery,        BuildingTime = 1, StoneCost = 200, WoodCost = 200, CreateAble = new WarriorType.Kind[] { WarriorType.Kind.Archer }, Description = "Archers are trained and equited here." });
            gameTileTypes.Add(new TileType() { kind = TileType.Kind.Tower,          BuildingTime = 1, StoneCost = 500, WoodCost = 100, Description = "Increases the defence of the village." });

            return gameTileTypes;
        }


        public static Map CreateMap(int[] binMap, List<TileType> gameTileTypes)
        {


            int size = (int)Math.Sqrt(binMap.Length);

            Map map = new Map();


            int i=0;
            for (int y = 0;y < size; y++)
            {
//                map[y] = new List<TileType>();
                map.Add(new List<PlayerTile>());
                for (int x = 0; x < size; x++)
                {

                    TileType t = null;

                    int binId = binMap[i];
                    switch (binId)
                    {
                        case 0:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Ocean));
                            break;
                        case 1:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Grass));
                            break;
                        case 3:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Forrest));
                            break;
                        case 4:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Hill));
                            break;
                        case 5:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Mountain));
                            break;
                        case 7:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Stone));
                            break;
                        case 8:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Gold));
                            break;
                        case 100:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.The_Long_House));
                            break;
                        case 101:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Farm));
                            break;
                        case 102:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Gold_Mine));
                            break;
                        case 103:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Quarry));
                            break;
                        case 104:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Barracks));
                            break;
                        case 105:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Archery));
                            break;
                        case 106:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.Lumber_camp));
                            break;
                        case 50:
                            t = (gameTileTypes.FirstOrDefault(ti => ti.kind == TileType.Kind.The_Ship));
                            break;

                        default:
                            throw new Exception(String.Format("TileType #{0} not created!", binId));
                            

                    }


                    if (t==null)
                    {
                        throw new Exception("No TileType with #"+binId+" i game!");
                    }


                    map[y].Add(new PlayerTile(t,i));

                    i++;
                }
            }


            return map;


        }

/*
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



        public static int[] defaultMap =
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
*/

        public static int[] defaultMap =
                          {
                            5,5,5,5,4,5,5,5,5,5,5,1,5,5,5,5,
                            5,4,7,5,3,4,4,4,5,4,4,3,3,4,4,5,
                            5,8,1,1,1,1,1,3,1,1,1,3,3,1,4,5,
                            5,4,1,1,1,1,1,1,1,1,7,3,3,1,4,5,
                            5,8,1,1,1,1,1,3,1,1,1,3,3,3,4,5,
                            5,4,1,1,1,1,1,1,1,1,7,3,3,3,3,5,
                            5,4,3,1,1,1,1,1,1,1,1,3,1,3,3,5,
                            5,4,1,3,1,1,3,1,1,100,1,1,3,1,1,5,
                            5,4,3,3,1,1,0,1,1,1,1,1,3,1,3,5,
                            5,4,3,3,1,1,0,1,1,1,1,1,3,1,3,5,
                            5,8,3,3,1,1,0,0,50,0,1,1,3,1,4,5,
                            5,4,1,3,1,1,3,0,0,0,1,1,3,3,4,5,
                            5,4,1,3,1,1,3,0,0,0,1,1,1,3,7,5,
                            5,0,0,0,0,0,0,0,0,0,0,0,1,0,0,5,
                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                          };

    }

  
}
