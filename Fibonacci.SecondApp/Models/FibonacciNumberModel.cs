namespace Fibonacci.SecondApp.Models
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
