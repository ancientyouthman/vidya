using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Core;
using MongoDB.Driver;
using vidya.Models;
using vidya.Services.Api;
using vidya.Models.Enums;

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




        public GameInCollection AddGameToCollection(int userId, int gameId, bool adding, ListType listType)
        {
            var collection = _database.GetCollection<GameCollection>("gameCollections");
            var gameCollection = GetGameCollection(userId);
            if (gameCollection == null) CreateGameCollection(userId);

            var game = new GameInCollection
            {
                Id = gameId,
                Have = listType == ListType.Collection && adding,
                Want = listType == ListType.Wantlist && adding
            };
            try
            {
                //


                //// update with positional operator
                //var update = Builders<GameCollection>.Update;
                //var listSetter = update.Set("Games.$.Have", game.Have).Set("Games.$.Want", game.Want);
                //collection.UpdateOne(collectionAndGameFilter, listSetter, new UpdateOptions { IsUpsert = true });

                var filter = Builders<GameCollection>.Filter;

                var collectionAndGameFilter = filter.And(
                filter.Eq(x => x.UserId, userId),
                filter.ElemMatch(x => x.Games, y => y.Id == gameId));
                var gameInCollection = collection.Find(collectionAndGameFilter).SingleOrDefault();

                if (gameInCollection == null)
                {
                    var collectionFilter = Builders<GameCollection>.Filter.Eq("UserId", userId);

                    var update = Builders<GameCollection>.Update
                     .Push<GameInCollection>("Games", game);

                    collection.UpdateOne(collectionFilter, update, new UpdateOptions { IsUpsert = true });
                }
                else if (!game.Have && !game.Want)
                {
                    var update = Builders<GameCollection>.Update.PullFilter(x => x.Games, x => x.Id == gameId);

                    collection.FindOneAndUpdate(collectionAndGameFilter, update);
                }
                else
                {
                    var update = Builders<GameCollection>.Update;
                    var listSetter = update.Set("Games.$.Have", game.Have).Set("Games.$.Want", game.Want);
                    collection.UpdateOne(collectionAndGameFilter, listSetter, new UpdateOptions { IsUpsert = true });
                }

                return GetGameInCollection(gameId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<GameModel> GetGameCollection(int userId = 5)
        {
            var gameCollection = GetCollection(userId);
            if (gameCollection == null) return null;
            var gameIds = gameCollection.Games.Select(x => x.Id);
            var result = new GameSearchService().GetGames(gameIds);
            foreach (var game in result)
            {
                if (gameCollection.Games.Select(x => x.Id).Contains(game.Id))
                {
                    game.Have = gameCollection.Games.Where(x => x.Id == game.Id).FirstOrDefault().Have;
                    game.Want = gameCollection.Games.Where(x => x.Id == game.Id).FirstOrDefault().Want;

                }
            }
            return result.OrderBy(x => x.Name);
        }

        public bool GameIsInCollection(int gameId, int userId = 5)
        {
            var gameCollection = GetCollection(userId);
            if (gameCollection == null) return false;
            var gameIds = gameCollection.Games.Select(X => X);
            return gameIds.Where(x => x.Id == gameId).Any();
        }

        public List<int> AddPlatformForGame(int gameId, int platformId, bool adding)
        {
            var userId = 5;
            var collection = _database.GetCollection<GameCollection>("gameCollections");
            var filter = Builders<GameCollection>.Filter;
            var collectionAndGameFilter = filter.And(
            filter.Eq(x => x.UserId, userId),
            filter.ElemMatch(x => x.Games, y => y.Id == gameId));


            var update = Builders<GameCollection>.Update;

            var ids = adding ? update.Push<int>("Games.$.Platforms", platformId) : update.Pull<int>("Games.$.Platforms", platformId);


            collection.UpdateOne(collectionAndGameFilter, ids, new UpdateOptions { IsUpsert = true });

            var gameinCollection = GetGameInCollection(gameId);

            return gameinCollection.Platforms;

        }

        public GameInCollection GetGameInCollection(int gameId, int userId = 5)
        {
            var gameCollection = GetCollection(userId);
            if (gameCollection == null) return null;
            if (gameCollection.Games.Where(x => x.Id == gameId).Any())
            {
                var game = gameCollection.Games.Where(x => x.Id == gameId).FirstOrDefault();
                return game;
            }
            return null;
        }

        private GameCollection GetCollection(int userId)
        {
            var collection = _database.GetCollection<GameCollection>("gameCollections");
            var filter = Builders<GameCollection>.Filter.Eq("UserId", userId);
            var gameCollection = collection.Find(filter).FirstOrDefault();
            return gameCollection;

        }

        private void CreateGameCollection(int userId)
        {
            var collection = _database.GetCollection<GameCollection>("gameCollections");
            collection.InsertOne(new GameCollection { UserId = userId });
        }


    }
}