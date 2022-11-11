using Fibonacci.SecondApp.Services.Abstract;

namespace Fibonacci.SecondApp.App
{
    public class DataSeeder
    {
        private readonly ICacheService<int,long> _cacheService;
        public DataSeeder(ICacheService<int,long> cacheService)
        {
            _cacheService = cacheService;
        }

        public void InitializeFirstFibonacciNumbers()
        {
            _cacheService.Set(0, 0);
            _cacheService.Set(1, 1);
            _cacheService.Set(2, 1);
        }
    }
}
