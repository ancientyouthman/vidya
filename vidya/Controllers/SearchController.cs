using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidya.Models.Search;
using vidya.Services;

namespace vidya.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApiService _apiService = new ApiService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public ActionResult SearchGames(GameSearchModel model)
        {
            model.Results = _apiService.Search(model);

            return Json(model);

        }


    }
}