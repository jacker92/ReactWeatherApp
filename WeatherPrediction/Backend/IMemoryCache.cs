﻿using System;
using System.Collections.Generic;

namespace WeatherPrediction.Backend
{
    public interface IMemoryCache<TItem>
    {
        IEnumerable<TItem> GetOrCreate(object key, Func<IEnumerable<TItem>> createItem);
    }
}