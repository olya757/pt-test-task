using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.FirstApp.Services.Abstract
{
    public interface ICalculationService
    {
        Task StartFibonacciCalculation(CancellationToken cancellationToken);
    }
}
