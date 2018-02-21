using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidya.Models.Search;
using vidya.Services;
using vidya.Services.Api;

namespace vidya.Controllers
{
    public class GameSearchController : Controller
    {
        private readonly GameSearchService _searchService = new GameSearchService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchGames(GameSearchModel model)
        {
           var response = _searchService.Search(model);

            return Json(response);

        }
        [HttpPost]
        public ActionResult GetGame(int id)
        {
            var model = _searchService.GetGame(id);
            return Json(model);

        }

    }
}