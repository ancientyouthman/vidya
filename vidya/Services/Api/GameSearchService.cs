using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidya.Models;
using vidya.Models.Base;
using vidya.Models.Search;


namespace vidya.Services.Api
{
    public class GameSearchService : ApiService
    {
        public GameSearchResultModel Search(GameSearchModel searchModel)
        {
            var request = Request("/search", Method.GET, searchModel.Page, searchModel.Name, null);
            var response = Response<GameSearchResultModel>(request);
            return response;
        }

        public GameModel GetGame (int id)
        {
            var request = Request("/games", Method.GET, null, null, "id:" + id);
            var response = Response< GameSearchResultModel > (request);
            return response.results.FirstOrDefault();
        }

    }
}