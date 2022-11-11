using Fibonacci.SecondApp.Models;
using Fibonacci.SecondApp.Services.Abstract;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fibonacci.SecondApp.Services.Implementation
{
    public class NumbersSynchronizationService : INumbersSynchronizationService
    {
        private readonly IMessageBrokerService _messageBrokerService;
        public NumbersSynchronizationService(IMessageBrokerService messageBrokerService)
        {
            _messageBrokerService = messageBrokerService;
        }

        public async Task SynchronizeNumber(FibonacciNumberModel model)
        {
            string topic = model.CalculationId.ToString();
            var jsonModel = JsonSerializer.Serialize(model);
            await _messageBrokerService.PushMessage(topic, jsonModel);
        }
    }
}
