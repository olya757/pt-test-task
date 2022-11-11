using Fibonacci.FirstApp.Services.Abstract;
using Fibonacci.FirstApp.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine(args.FirstOrDefault());
var serviceProvider = new ServiceCollection()
            .AddSingleton<ICacheService<int, long>, CacheService<int, long>>()
            .AddScoped<IHttpSender,HttpSender>()
            .AddScoped<IMessageBrokerService, MessageBrokerService>()
            .AddScoped<IHttpSyncronizationService, NumbersSynchronizationService>()
            .AddScoped<ICalculationService, CalculationService>()
            .AddLogging(options =>
            {
                options.AddConsole();
            })
            .BuildServiceProvider();

//initialize values for 0 and 1
var cacheService = serviceProvider.GetService<ICacheService<int, long>>();
cacheService.Set(0, 0);
cacheService.Set(1, 1);

var calculationService = serviceProvider.GetRequiredService<ICalculationService>();

int amountOfcalculations = 5; //you can set any positive value, then rebuild app and call docker compose up
CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
CancellationToken token = cancelTokenSource.Token;

for (int i = 0; i < amountOfcalculations; i++)
{
    calculationService.StartFibonacciCalculation(token);
}

//read key from console to stop execution
Console.WriteLine("Type any symbol to stop execution");
var param = Console.ReadLine();

cancelTokenSource.Cancel();
cancelTokenSource.Dispose();

return;
