using System;
using System.Linq;
using System.Web.Mvc;
using GameEngine;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;
using GoVikingGame.Models;
using GoVikingGameWeb;

namespace GoVikingGame.Controllers
{
    public class ClientController : Controller
    {



        private Game RunningGame
        {
            get
            {
                Game game = HttpContext.Application[MvcApplication.GameInstanse] as GameEngine.Game;
                return game;
            }

        }

        private  Vik PlayerVik
        {
            get
            {
                Vik playerVik = (RunningGame.Viks.Where(vik => (vik.Id.Equals(Session.SessionID)))).First();
                return playerVik;

            }
        }


        public ActionResult Build(TileType.Kind kind, int TileId)
        {
            TileType tileToBuild = RunningGame.TileTypes.Where(e => e.kind.Equals(kind)).First();

            bool buildStarted = PlayerVik.StartBuilding(tileToBuild, TileId);

            var buildresponse = new ClientResponseModels.BuildModel();
            buildresponse.Ok = buildStarted;
            if (buildStarted)
            {
                buildresponse.ImageFile = tileToBuild.ImageFile;
                buildresponse.BuildTimeTicks = tileToBuild.BuildingTime;
                buildresponse.TileId = TileId;
                buildresponse.Kind = kind;
            }

            return View(buildresponse);
        }


        public ActionResult Train(WarriorType.Kind kind)
        {
            WarriorType warriortoTrain = RunningGame.WarriorTypes.Where(e => e.kind.Equals(kind)).First();

            bool trainingStarted = PlayerVik.StartTraining(warriortoTrain);

            var response = new ClientResponseModels.CreateModel();
            response.Ok = trainingStarted;
            response.TrainingTicks = warriortoTrain.TrainingTime;
            return View(response);
        }

        public ActionResult Message(string message)
        {
            var response = new ClientResponseModels.MessageModel();
            response.response = "server klokka " + DateTime.Now + " melding var '" + message + "'";
            return View(response);
        }


        public ActionResult Resources()
        {
            var resources = new ClientResponseModels.Resources {nextTick = RunningGame.NextTick};

            Vik playerVik = PlayerVik;
            resources.food = playerVik.resources.food;
            resources.wood = playerVik.resources.wood;
            resources.stone = playerVik.resources.stone;
            resources.gold = playerVik.resources.gold;
            resources.workers = playerVik.resources.workers;
            resources.maxWorkers = playerVik.resources.maxWorkers;

            resources.foodProduction = playerVik.resources.foodProduction;
            resources.woodProduction = playerVik.resources.woodProduction;
            resources.stoneProduction = playerVik.resources.stoneProduction;
            resources.goldProduction = playerVik.resources.goldProduction;

            return View(resources);

        }

    }
}
