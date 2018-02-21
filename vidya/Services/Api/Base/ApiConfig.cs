using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace vidya.Services
{
    public class ApiConfig
    {
        public readonly string Host;
        public readonly string ApiKey;

        public ApiConfig()
        {
            Host = ConfigurationManager.AppSettings["ApiHost"];
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
        }
    }
}