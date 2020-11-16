using Newtonsoft.Json.Linq;

namespace WeatherApp.Utilities
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
