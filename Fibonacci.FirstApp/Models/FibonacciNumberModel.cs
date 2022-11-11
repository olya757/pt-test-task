using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.FirstApp.Models
{
    public class FibonacciNumberModel
    {
        public Guid CalculationId { get; set; }
        public int Index { get; set; }
        public long Number { get; set; }
        public FibonacciNumberModel(Guid calculationId, int index, long number)
        {
            CalculationId = calculationId;
            Index = index;
            Number = number;
        }

        public FibonacciNumberModel() { }
    }
}
