using Fibonacci.SecondApp.Models;
using Fibonacci.SecondApp.Services.Abstract;

namespace Fibonacci.SecondApp.Services.Implementation
{
    public class CalculationService : ICalculationService
    {
        private readonly ICacheService<int,long> _cacheService;
        private readonly INumbersSynchronizationService _numbersSynchronizationService;
        public CalculationService(ICacheService<int,long> cacheService, INumbersSynchronizationService numbersSynchronizationService)
        {
            _cacheService = cacheService;
            _numbersSynchronizationService = numbersSynchronizationService;
        }

        public async Task CalculateNextFibonacciNumber(FibonacciNumberModel model)
        {
            if (model.Index < 0)
            {
                return;
            }

            _cacheService.Set(model.Index, model.Number);

            long previousNumber;
            if (!_cacheService.TryGetValue(model.Index - 1, out previousNumber))
            {
                throw new Exception("Previous value is not found");
            }

            long nextNumber = previousNumber + model.Number;
            _cacheService.Set(model.Index + 1, nextNumber);

            await _numbersSynchronizationService.SynchronizeNumber(
                new FibonacciNumberModel(
                    model.CalculationId,
                    model.Index + 1,
                    nextNumber)
            );
        }
    }
}
