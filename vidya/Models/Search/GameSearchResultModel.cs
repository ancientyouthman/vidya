using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static vidya.Models.DataModels;

namespace vidya.Models.Search
{
    public class GameSearchResultModel
    {
      public int TotalResults { get; set; }
        public IList<Game> Results { get; set; }
    }

}