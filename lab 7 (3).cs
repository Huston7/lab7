using System;
using System.Collections.Generic;

public class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();

    public delegate TResult Func<TKey, TResult>(TKey key);

    private class CacheItem
    {
        public TResult Result { get; set; }
        public DateTime Expiration { get; set; }
    }

    public TResult GetOrAdd(TKey key, Func<TKey, TResult> func, TimeSpan cacheDuration)
    {
        if (cache.TryGetValue(key, out CacheItem cacheItem) && cacheItem.Expiration > DateTime.Now)
        {
            return cacheItem.Result;
        }
        else
        {
            TResult result = func(key);
            cache[key] = new CacheItem
            {
                Result = result,
                Expiration = DateTime.Now + cacheDuration
            };
            return result;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        FunctionCache<string, int> cache = new FunctionCache<string, int>();

        Func<string, int> expensiveFunction = (key) =>
        {
            // Simulate an expensive computation
            Console.WriteLine($"Computing the result for key '{key}'");
            return key.Length * 2;
        };

        string key1 = "abc";
        string key2 = "def";

        // Call the expensive function with caching
        int result1 = cache.GetOrAdd(key1, expensiveFunction, TimeSpan.FromMinutes(1));
        Console.WriteLine($"Result for key '{key1}': {result1}");

        // Call the expensive function with caching again, should return the cached result
        int result1Cached = cache.GetOrAdd(key1, expensiveFunction, TimeSpan.FromMinutes(1));
        Console.WriteLine($"Result for key '{key1}' (cached): {result1Cached}");

        // Call the expensive function with a different key
        int result2 = cache.GetOrAdd(key2, expensiveFunction, TimeSpan.FromMinutes(1));
        Console.WriteLine($"Result for key '{key2}': {result2}");
    }
}
