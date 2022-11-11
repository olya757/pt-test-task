namespace Fibonacci.SecondApp.Services.Abstract
{
    public interface IMessageBrokerService
    {
        Task PushMessage<T>(string queueName, T message);
    }
}
