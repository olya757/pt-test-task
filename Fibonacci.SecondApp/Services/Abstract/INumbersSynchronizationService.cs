using Fibonacci.SecondApp.Models;

namespace Fibonacci.SecondApp.Services.Abstract
{
    public interface INumbersSynchronizationService
    {
        Task SynchronizeNumber(FibonacciNumberModel model);
    }
}
