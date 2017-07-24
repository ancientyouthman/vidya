using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using MongoDB.Driver;
using vidya.Models;
using static vidya.Models.DataModels;

namespace vidya.Services
{
    public class DatabaseService
    {
        static readonly string connectionString = "mongodb://localhost:27017";
        private MongoClient _client;
        private IMongoDatabase _database;
        public DatabaseService()
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase("test");
        }




        public async Task<bool> AddGameToCollection(int userId, int gameId)
        {
            try
            {
                var collection = _database.GetCollection<GameCollection>("gameCollections");
                var filter = Builders<GameCollection>.Filter.Eq("userId", userId);

                var update = Builders<GameCollection>.Update
                 .Push<int>(x => x.Games, gameId);

                await collection.FindOneAndUpdateAsync(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Game> GetGameCollection(int userId = 1)
        {
            var collection = _database.GetCollection<GameCollection>("gameCollections");
            var filter = Builders<GameCollection>.Filter.Eq("userId", userId);

           var gameCollection =  collection.Find(filter).FirstOrDefault();

            var ids = gameCollection.Games.Select(X => X);
            var result = new ApiService().GetGames(ids);
            return result;
        }


    }
}