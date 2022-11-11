using EasyNetQ;
using EasyNetQ.Topology;
using Fibonacci.SecondApp.Services.Abstract;

namespace Fibonacci.SecondApp.Services.Implementation
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly string messageBrokerPath;
        private readonly ILogger<MessageBrokerService> logger;
        public MessageBrokerService(IConfiguration configuration, ILogger<MessageBrokerService> logger)
        {
            messageBrokerPath = configuration.GetSection("RabbitMQ").Value!;
            this.logger = logger;
        }
        public async Task PushMessage<T>(string queueName, T message)
        {
            try
            {
                using (var bus = RabbitHutch.CreateBus(messageBrokerPath, c => c.EnableLegacyTypeNaming()).Advanced)
                {
                    var queue = await bus.QueueDeclareAsync(queueName);
                    var exchange = await bus.ExchangeDeclareAsync(queueName, ExchangeType.Direct);
                    bus.Bind(exchange, queue, "#");

                    var formattedMessage = new Message<T>(message);

                    bus.Publish<T>(exchange, "#", false, formattedMessage);
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
