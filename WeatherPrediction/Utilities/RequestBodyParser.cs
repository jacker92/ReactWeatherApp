using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPrediction.Controllers
{
    public class RequestBodyParser : IRequestBodyParser
    {
        public string Parse(object body)
        {
            return JObject.Parse(body?.ToString())?["searchString"]?.ToString();
        }
    }
}
