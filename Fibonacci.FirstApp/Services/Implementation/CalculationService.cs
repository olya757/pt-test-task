using EasyNetQ;
using Fibonacci.FirstApp.Models;
using Fibonacci.FirstApp.Services.Abstract;
using Microsoft.Extensions.Logging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fibonacci.FirstApp.Services.Implementation
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogger _logger;
        private readonly IMessageBrokerService _messageBrokerService;
        private readonly IHttpSyncronizationService _numbersSyncronizationService;
        private readonly ICacheService<int, long> _cacheService;
        public CalculationService(IHttpSyncronizationService numbersSyncronizationService, ICacheService<int, long> cacheService, IMessageBrokerService messageBrokerService, ILogger<CalculationService> logger)
        {
            _logger = logger;
            _numbersSyncronizationService = numbersSyncronizationService;
            _messageBrokerService = messageBrokerService;
            _cacheService = cacheService;
        }



        public async Task StartFibonacciCalculation(CancellationToken cancellationToken)
        {
            Guid calculationId = Guid.NewGuid();
            Action<IMessage<string>, MessageReceivedInfo> handler = (messageModel, rec) =>
            {
                FibonacciNumberModel message = JsonSerializer.Deserialize<FibonacciNumberModel>(messageModel.Body);
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Cansel task for {calculationId}", message.CalculationId);
                    return;
                }

                Console.WriteLine($"New number {message.Number} received for {message.CalculationId}");
                _logger.LogInformation("New number {number} received for {calculationId}", message.Number, message.CalculationId);

                _cacheService.Set(message.Index, message.Number);
                long previousNumber;
                if (_cacheService.TryGetValue(message.Index - 1, out previousNumber))
                {
                    var nextNumber = previousNumber + message.Number;
                    _cacheService.Set(message.Index + 1, nextNumber);
                    _numbersSyncronizationService.SynchronizeNumber(
                       new FibonacciNumberModel(
                           message.CalculationId,
                           message.Index + 1,
                           nextNumber));
                }
                else
                {
                    _logger.LogError("Previous number not found for {calculationId}", message.CalculationId);
                    return;
                }
            };

            var queueName = calculationId.ToString();
            await _messageBrokerService.ConsumeQueue(queueName, handler);

            await _numbersSyncronizationService.SynchronizeNumber(new FibonacciNumberModel(calculationId, 1, 1));
        }
    }
}
