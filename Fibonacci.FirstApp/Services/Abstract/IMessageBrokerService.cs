using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.FirstApp.Services.Abstract
{
    public interface IMessageBrokerService
    {
        Task ConsumeQueue<T>(string queueName, Action<IMessage<T>, MessageReceivedInfo> handler);
    }
}
