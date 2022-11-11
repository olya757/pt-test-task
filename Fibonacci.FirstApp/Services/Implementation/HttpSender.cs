using Fibonacci.FirstApp.Services.Abstract;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Fibonacci.FirstApp.Services.Implementation
{
    public class HttpSender : IHttpSender
    {
        private readonly ILogger<HttpSender> _logger;
        public HttpSender(ILogger<HttpSender> logger)
        {
            _logger = logger;
        }
        public async Task SendMessageAsync<T>(string uri, T message)
        {
            var bodyJson = JsonSerializer.Serialize(message);
            var content = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);

            var responce = await httpClient.PostAsync("", content);
            if (!responce.IsSuccessStatusCode)
            {
                _logger.LogError($"Message sending failed {message}");
                throw new Exception($"Message sending failed {message}");
            }
        }
    }
}
