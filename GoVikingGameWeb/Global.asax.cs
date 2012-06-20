using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GameEngine;

// ReSharper disable CheckNamespace
namespace GoVikingGameWeb
// ReSharper restore CheckNamespace
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public const string GameInstanse = "GameEngineServerInstanse";

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Game", action = "Default", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Go Vik'ing Game Engine
            //game = new GameEngine.Game();
            GameEngine.Game _game = new GameEngine.Game();
            Application.Add(GameInstanse, _game);
        }


        protected void Session_Start(object sender, EventArgs e)
        {


            GameEngine.Game gameEngine = Application[MvcApplication.GameInstanse] as GameEngine.Game;


            int[] map = null;
            if (gameEngine.Viks.Count() % 2 == 1)
            {
                map =  GameInititializer.map2;
            }
            else
            {
                map =  GameInititializer.map3;
            }

            gameEngine.AddPlayer(Session.SessionID, "test", "no name",GameInititializer.CreateMap(map, gameEngine.TileTypes));

        }
    }
}