﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;
using GameEngine.GameTypes;
using GameEngine.PlayerItem;
using GoVikingGameWeb.Models;

namespace GoVikingGameWeb.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Default()
        {

            Game game = HttpContext.Application[MvcApplication.GameInstanse] as GameEngine.Game;

            var gameModel = new GameViewModel();
            gameModel.PlayerId = Session.SessionID;
            gameModel.TileTypes = game.TileTypes;
            gameModel.WarriorTypes = game.WarriorTypes;

            Vik playerVik = (game.Viks.Where(vik => (vik.Id.Equals(Session.SessionID)))).First();

            gameModel.UserSessionMap = playerVik.map;
            gameModel.Resources = playerVik.resources;

            gameModel.GameViksCount = game.Viks.Count;
            gameModel.MapRenderWidth = gameModel.UserSessionMap.Count()*128;


            return View(gameModel);
        }

    }
}
