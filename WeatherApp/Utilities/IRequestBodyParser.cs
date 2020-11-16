namespace WeatherApp.Utilities
{
    public interface IRequestBodyParser
    {
        string Parse(object body);
    }
}