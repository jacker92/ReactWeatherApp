namespace WeatherPrediction.Utilities
{
    public class SearchTermValidator : ISearchTermValidator
    {
        public bool IsValid(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return false;
            }

            else if (searchTerm.Length > 255)
            {
                return false;
            }

            return true;
        }
    }
}
