using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidya.Models.Enums;
using vidya.Models.Search;
using vidya.Services;
using vidya.Services.Api;

namespace vidya.Controllers
{
    public class CollectionController : Controller
    {
      //  private readonly ApiService _apiService = new ApiService();
        private readonly DatabaseService _databaseService = new DatabaseService();
        private readonly GameSearchService _searchService = new GameSearchService();


        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddToCollection(int id, bool adding, ListType listType)
        {
            var userId = 5;
            var success = _databaseService.AddGameToCollection(userId, id, adding, listType);
            return Json(success);
        }

        [HttpPost]
        public ActionResult GameCollection()
        {
            var userId = 5;
            var model = _databaseService.GetGameCollection(userId);
            return Json(model);

        }
        [HttpPost]
        public ActionResult IsGameInCollection(int gameId)
        {
            var result = _databaseService.GameIsInCollection(gameId);
            return Json(result);
        }
        [HttpPost]
        public ActionResult AddPlatformForGame(int gameId, int platformId, bool adding)
        {
            var result = _databaseService.AddPlatformForGame(gameId, platformId, adding);
            return Json(result);
        }
    }
}