namespace WeatherPrediction.Controllers
{
    public interface IRequestBodyParser
    {
        string Parse(object body);
    }
}