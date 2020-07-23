using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherPrediction.Backend
{
    public static class DynamicConverterExtensions
    {
        public static long TryConvertToLong(this string value)
        {
            if(value != null)
            {
                return long.Parse(value.Replace("{", "").Replace("}", ""));
            }

            return long.MinValue;
        }

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
