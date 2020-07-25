using Newtonsoft.Json.Linq;

namespace WeatherPrediction.Utilities
{
    public class RequestBodyParser : IRequestBodyParser
    {
        public string Parse(object body)
        {
            if(string.IsNullOrWhiteSpace(body?.ToString()))
            {
                return string.Empty;
            }

            return JObject.Parse(body?.ToString())?["searchString"]?.ToString();
        }
    }
}
