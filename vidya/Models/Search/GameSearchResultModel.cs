﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vidya.Models.Search
{
    public class GameSearchResultModel
    {
        public IList<GameModel> results { get; set; }
        public string error { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int number_of_page_results { get; set; }
        public int number_of_total_results { get; set; }
        public int status_code { get; set; }
    }

}