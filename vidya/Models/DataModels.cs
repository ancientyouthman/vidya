using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidya.Models
{

    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Owned { get; set; }
    }

    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Deck { get; set; }
        public string Description { get; set; }
        public IList<Platform> Platforms { get; set; }
        public Image Image { get; set; }
        public DateTime? Original_Release_Date { get; set; }
        public bool? Have { get; set; }
        public bool? Want { get; set; }

    }
    public class Image
    {
        public string Small_Url { get; set; }
    }

    // mongo

    public class GameCollection
    {
        public ObjectId Id { get; set; }
        public int UserId { get; set; }
        public List<GameInCollection> Games { get; set; }
        public GameCollection()
        {
            this.Games = new List<GameInCollection>();
        }
    }

    public  class GameInCollection {
        public int Id { get; set; }
        public List<int> Platforms { get; set; }
        public bool Have { get; set; }
        public bool Want { get; set; }
        public bool Completed { get; set; }
        public GameInCollection()
        {
            this.Platforms = new List<int>();

        }
            
    }

}