namespace WeatherPrediction.Utilities
{
    public interface IRequestBodyParser
    {
        string Parse(object body);
    }
}