using Fibonacci.SecondApp.Services.Abstract;
using System.Collections.Concurrent;

namespace Fibonacci.SecondApp.Services.Implementation
{
    public class CacheService<TKey, TValue> : ICacheService<TKey, TValue> where TKey : notnull
    {
        private static readonly ConcurrentDictionary<TKey, TValue> fibonacciNumbers = new ConcurrentDictionary<TKey, TValue>();

        private readonly ILogger logger;
        public CacheService(ILogger<CacheService<TKey, TValue>> logger)
        {
            this.logger = logger;

        }
        public void Set(TKey key, TValue item)
        {
            fibonacciNumbers[key] = item;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            if (!fibonacciNumbers.ContainsKey(key))
                return false;
            value = fibonacciNumbers[key];
            return true;
        }
    }
}
