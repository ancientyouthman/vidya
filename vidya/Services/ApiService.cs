using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vidya.Models.Search;
using static vidya.Models.DataModels;

namespace vidya.Services
{
    public class ApiService
    {
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };
    public GameSearchResultModel Search(GameSearchModel searchModel)
        {
            var client = new RestClient("https://www.giantbomb.com/api/");

            var request = new RestRequest("search/", Method.GET);
            request.AddParameter("api_key", "978863b5a440adde891be8f6cc51f9ed26fe5a3a");
            request.AddParameter("format", "json");
            request.AddParameter("resources", "game");
            request.AddParameter("page", searchModel.Page);
            request.AddParameter("query", searchModel.Name);

            IRestResponse response = client.Execute(request);

            dynamic searchResults = JsonConvert.DeserializeObject(response.Content);

            var games = JsonConvert.DeserializeObject<IEnumerable<Game>>(searchResults.results.ToString(), _jsonSettings);
            var totalResults = searchResults.number_of_total_results;

            //


            return new GameSearchResultModel
            {
                Results = games,
                TotalResults = totalResults,
            };
        }


        public Game GetGame(long id)
        {
            var client = new RestClient("https://www.giantbomb.com/api/");

            var request = new RestRequest("games/", Method.GET);
            request.AddParameter("api_key", "978863b5a440adde891be8f6cc51f9ed26fe5a3a");
            request.AddParameter("format", "json");
            request.AddParameter("filter", "id:" + id);

            IRestResponse response = client.Execute(request);

            dynamic results = JsonConvert.DeserializeObject(response.Content);

            var result = JsonConvert.DeserializeObject<IEnumerable<Game>>(results.results.ToString())[0];

            var ret = new Game
            {

                Name = result.Name,
                Deck = result.Deck,
                Description = result.Description,
                Platforms = result.Platforms,
                Id = result.Id
            };
            return ret;

        }

        public IEnumerable<Game> GetGames(IEnumerable<int> ids)
        {
            var client = new RestClient("https://www.giantbomb.com/api/");

            var request = new RestRequest("games/", Method.GET);
            request.AddParameter("api_key", "978863b5a440adde891be8f6cc51f9ed26fe5a3a");
            request.AddParameter("format", "json");
            request.AddParameter("filter", "id:" + string.Join("|", ids.Select(x => x.ToString())));

            IRestResponse response = client.Execute(request);

            dynamic results = JsonConvert.DeserializeObject(response.Content);

            var result = JsonConvert.DeserializeObject<IEnumerable<Game>>(results.results.ToString());


            return result;

        }

    }
}
