using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using vidya.Models;
using vidya.Models.Base;
using vidya.Models.Search;

namespace vidya.Services
{
    public abstract class ApiService
    {
        protected readonly ApiConfig _config = new ApiConfig();
        protected RestClient _restClient;

        public ApiService()
        {
            _restClient = new RestClient(_config.Host);
        }

        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };


        protected IRestRequest Request(string resource, Method method, int? page = null, string query = null, string filter = null)
        {
            var request = new RestRequest(resource, method)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("api_key", _config.ApiKey);
            request.AddParameter("format", "json"); // make this a config 
            request.AddParameter("resources", "game"); // does this need to be a param 

            if (page != null) request.AddParameter("page", page);
            if (!string.IsNullOrEmpty(filter)) request.AddParameter("filter", filter);
            if (!string.IsNullOrEmpty(query)) request.AddParameter("query", query);

            // do we need it ??? 
            //if (body != null)
            //{
            //    var json = JsonConvert.SerializeObject(body, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //    request.AddParameter("application/json", json, ParameterType.RequestBody);
            //}

            return request;
        }

        protected T Response<T>(IRestRequest request) where T : new()
        {
           var response = _restClient.Execute(request);
          var result = JsonConvert.DeserializeObject<T>(response.Content);

            //    var result = JsonConvert.DeserializeObject<List<ResponseModel<T>>>(content.results.ToString());


            //if (string.IsNullOrEmpty(response.Data.error))
            //{
            //    // do something ?? 
            //}

            return result;

        }


        //public GameSearchResultModel Search(GameSearchModel searchModel)
        //{
        //    var client = new RestClient("https://www.giantbomb.com/api/");

        //    var request = new RestRequest("search/", Method.GET);
        //    request.AddParameter("api_key", "978863b5a440adde891be8f6cc51f9ed26fe5a3a");
        //    request.AddParameter("format", "json");
        //    request.AddParameter("resources", "game");
        //    request.AddParameter("page", searchModel.Page);
        //    request.AddParameter("query", searchModel.Name);

        //    IRestResponse response = client.Execute(request);

        //    dynamic searchResults = JsonConvert.DeserializeObject(response.Content);

        //    var games = JsonConvert.DeserializeObject<IEnumerable<Game>>(searchResults.results.ToString(), _jsonSettings);
        //    var totalResults = searchResults.number_of_total_results;

        //    //


        //    return new GameSearchResultModel
        //    {
        //        Results = games,
        //        TotalResults = totalResults,
        //    };
        //}


        //public GameModel GetGame(long id)
        //{
        //    var client = new RestClient("https://www.giantbomb.com/api/");

        //    var request = new RestRequest("games/", Method.GET);
        //    request.AddParameter("api_key", "978863b5a440adde891be8f6cc51f9ed26fe5a3a");
        //    request.AddParameter("format", "json");
        //    request.AddParameter("filter", "id:" + id);

        //    IRestResponse response = client.Execute(request);

        //    dynamic results = JsonConvert.DeserializeObject(response.Content);

        //    var result = JsonConvert.DeserializeObject<IEnumerable<GameModel>>(results.results.ToString())[0];

        //    var ret = new GameModel
        //    {

        //        Name = result.Name,
        //        Deck = result.Deck,
        //        Description = result.Description,
        //        Platforms = result.Platforms,
        //        Id = result.Id
        //    };
        //    return ret;

        //}

        public IEnumerable<GameModel> GetGames(IEnumerable<int> ids)
        {
            var client = new RestClient("https://www.giantbomb.com/api/");

            var request = new RestRequest("games/", Method.GET);
            request.AddParameter("api_key", "978863b5a440adde891be8f6cc51f9ed26fe5a3a");
            request.AddParameter("format", "json");
            request.AddParameter("filter", "id:" + string.Join("|", ids.Select(x => x.ToString())));

            IRestResponse response = client.Execute(request);

            dynamic results = JsonConvert.DeserializeObject(response.Content);

            var result = JsonConvert.DeserializeObject<IEnumerable<GameModel>>(results.results.ToString());


            return result;

        }

    }
}
