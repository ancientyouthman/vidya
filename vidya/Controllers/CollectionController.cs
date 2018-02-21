using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidya.Models.Search;
using vidya.Services;

namespace vidya.Controllers
{
    public class CollectionController : Controller
    {
      //  private readonly ApiService _apiService = new ApiService();
        private readonly DatabaseService _databaseService = new DatabaseService();


        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult AddToCollection(int id)
        {
            var userId = 1;
            var success = _databaseService.AddGameToCollection(userId, id);
            return Json("true");

        }

        [HttpPost]
        public ActionResult GameCollection()
        {
            var userId = 1;
            var model = _databaseService.GetGameCollection(userId);
            return Json(model);

        }
    }
}