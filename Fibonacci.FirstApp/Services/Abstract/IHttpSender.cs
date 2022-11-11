using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.FirstApp.Services.Abstract
{
    public interface IHttpSender
    {
        Task SendMessageAsync<T>(string uri, T message);
    }
}
