using System;

namespace WeatherApp.Utilities
{
    public static class DynamicConverterExtensions
    {
        public static DateTimeOffset TryConvertToDateTimeOffset(this string value)
        {
            if(value != null)
            {
                return DateTimeOffset.FromUnixTimeSeconds(long.Parse(value));
            }
            return DateTimeOffset.MinValue;
        }
    }
}
