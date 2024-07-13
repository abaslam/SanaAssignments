using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceAbstraction.Infrastructure.Caching
{
    public class InMemoryCache : ICache
    {
        private ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        public T? Get<T>(string key)
        {
            if(cache.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            return default;
        }

        public T Set<T>(string key, T value)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(key, nameof(key));
            ArgumentNullException.ThrowIfNull(value, nameof(value));

            this.cache[key] = value;

            return value;
        }
    }
}
