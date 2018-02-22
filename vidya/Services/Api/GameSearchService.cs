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
        private readonly DatabaseService _databaseService = new DatabaseService();

        public GameSearchResultModel Search(GameSearchModel searchModel)
        {
            var request = Request("/search", Method.GET, searchModel.Page, searchModel.Name, null);
            var response = Response<GameSearchResultModel>(request);
            var gameCollection = _databaseService.GetGameCollection();
            foreach (var game in response.results)
            {
                if (gameCollection.Select(x => x.Id).Contains(game.Id))
                {
                    game.Have = gameCollection.Where(x => x.Id == game.Id).FirstOrDefault().Have;
                    game.Want = gameCollection.Where(x => x.Id == game.Id).FirstOrDefault().Want;
                }
            }
            return response;
        }

        public GameModel GetGame(int id)
        {
            var request = Request("/games", Method.GET, null, null, "id:" + id);
            var response = Response<GameSearchResultModel>(request);
            var result = response.results.FirstOrDefault();
            var inCollection = _databaseService.GameIsInCollection(id);

            if (inCollection)
            {
                var gameInCollection = _databaseService.GetGameInCollection(id);

                result.Have = gameInCollection.Have;
                result.Want = gameInCollection.Want;
                foreach (var platform in result.Platforms)
                {
                    if (gameInCollection.Platforms.Contains(platform.Id)) platform.Owned = true;
                }
            }
            return result;
        }

    }
}