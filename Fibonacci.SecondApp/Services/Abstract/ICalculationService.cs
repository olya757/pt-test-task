using Fibonacci.SecondApp.Models;

namespace Fibonacci.SecondApp.Services.Abstract
{
    public interface ICalculationService
    {
        Task CalculateNextFibonacciNumber(FibonacciNumberModel model);
    }
}
