using Fibonacci.FirstApp.App;
using Fibonacci.FirstApp.Models;
using Fibonacci.FirstApp.Services.Abstract;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Fibonacci.FirstApp.Services.Implementation
{
    public class NumbersSynchronizationService : IHttpSyncronizationService
    {
        private readonly ILogger<NumbersSynchronizationService> _logger;
        private readonly IHttpSender _httpSender;
        private readonly string secondAppUri;
        public NumbersSynchronizationService(ILogger<NumbersSynchronizationService> logger, IHttpSender sender)
        {
            _logger = logger;
            _httpSender = sender;
            secondAppUri = Settings.SecondAppURI;
        }

        public async Task SynchronizeNumber(FibonacciNumberModel model)
        {
            try
            {
                await _httpSender.SendMessageAsync(secondAppUri, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
