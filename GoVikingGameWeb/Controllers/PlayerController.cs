using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;
using GoVikingGame.Models;
using GoVikingGameWeb;

namespace GoVikingGame.Controllers
{
    public class PlayerController : Controller
    {

        // GET: /Player/



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


        public ActionResult Build(TileType.Kind kind)
        {
            TileType tileToBuild = RunningGame.TileTypes.Where(e => e.kind.Equals(kind)).First();

            bool buildStarted = PlayerVik.Build(tileToBuild);

            var buildresponse = new PlayerResponseModels.BuildModel();
            buildresponse.Ok = buildStarted;
            buildresponse.Kind = kind;
            buildresponse.BuildTimeTicks = tileToBuild.BuildingTime;

            return View(buildresponse);
        }


        public ActionResult Train(WarriorType.Kind kind)
        {
            WarriorType warriortoTrain = RunningGame.WarriorTypes.Where(e => e.kind.Equals(kind)).First();

            bool trainingStarted = PlayerVik.Train(warriortoTrain);

            var response = new PlayerResponseModels.CreateModel();
            response.Ok = trainingStarted;
            response.TrainingTicks = warriortoTrain.TrainingTime;
            return View(response);
        }

        public ActionResult Message(string message)
        {
            var response = new PlayerResponseModels.MessageModel();
            response.response = "server klokka " + DateTime.Now + " melding var '" + message + "'";
            return View(response);
        }



        public ActionResult Resources()
        {
            var resources = new PlayerResponseModels.Resources();

            Vik playerVik = PlayerVik;
            resources.food = playerVik.resources.food;
            resources.wood = playerVik.resources.wood;
            resources.stone = playerVik.resources.stone;
            resources.gold = playerVik.resources.gold;
            resources.workers = playerVik.resources.workers;

            resources.foodProduction = playerVik.resources.foodProduction;
            resources.woodProduction = playerVik.resources.woodProduction;
            resources.stoneProduction = playerVik.resources.stoneProduction;
            resources.goldProduction = playerVik.resources.goldProduction;
            
            
            return View(resources);

        }





    }
}
