namespace Fibonacci.SecondApp.Services.Abstract
{
    public interface ICacheService<TKey, TValue> where TKey : notnull
    {
        bool TryGetValue(TKey key, out TValue value);
        void Set(TKey key, TValue item);
    }
}
