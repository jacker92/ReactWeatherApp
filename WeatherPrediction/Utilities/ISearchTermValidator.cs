namespace WeatherPrediction.Controllers
{
    public interface ISearchTermValidator
    {
        bool IsValid(string searchTerm);
    }
}