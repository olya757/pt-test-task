using EasyNetQ;
using EasyNetQ.Topology;
using Fibonacci.FirstApp.App;
using Fibonacci.FirstApp.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace Fibonacci.FirstApp.Services.Implementation
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly string messageBrokerPath;
        private readonly ILogger<MessageBrokerService> logger;
        public MessageBrokerService(ILogger<MessageBrokerService> logger)
        {
            messageBrokerPath = Settings.RabbitMQ;
            this.logger = logger;
        }
        public async Task ConsumeQueue<T>(string queueName, Action<IMessage<T>, MessageReceivedInfo> handler)
        {
            try
            {
                using (var bus = RabbitHutch.CreateBus(messageBrokerPath, c => c.EnableLegacyTypeNaming()).Advanced)
                {
                    var queue = await bus.QueueDeclareAsync(queueName);
                    var exchange = await bus.ExchangeDeclareAsync(queueName, ExchangeType.Direct);
                    bus.Bind(exchange, queue, "#");
                    bus.Consume(queue, handler);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
