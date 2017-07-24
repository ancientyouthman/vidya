using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidya.Models
{
    public class DataModels
    {
        public class Platform
        {
            public string Name { get; set; }
        }

        public class Game
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Deck { get; set; }
            public string Description { get; set; }
            public IList<Platform> Platforms { get; set; }
            public Image Image { get; set; }
            public DateTime? Original_Release_Date { get; set; }
        }
        public class Image
        {
            public string Small_Url { get; set; }
        }

        // mongo

        public class GameCollection
        {
            public ObjectId Id { get; set; }
            public int userId { get; set; }
            public List<int> Games { get; set; }
            public GameCollection()
            {
                this.Games = new List<int>();
            }
        }
    }
}