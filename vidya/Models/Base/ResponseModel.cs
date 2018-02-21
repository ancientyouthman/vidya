using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace vidya.Models.Base
{
    public class ResponseModel<T> where T : new()
    {
        public ResponseModel()
        {
            results = new T();
        }
        public T results { get; set; }
        public string error { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int number_of_page_results { get; set; }
        public int number_of_total_results { get; set; }
        public int status_code { get; set; }

    }
}