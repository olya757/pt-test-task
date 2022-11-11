using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci.FirstApp.App
{
    public static class Settings
    {
        public static string RabbitMQ => ConfigurationManager.AppSettings["RabbitMQ"] ?? "host=localhost";
        public static string SecondAppURI => ConfigurationManager.AppSettings["SecondAppURI"]!;
    }
}
