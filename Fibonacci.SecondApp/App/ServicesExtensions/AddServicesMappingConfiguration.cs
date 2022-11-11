using Fibonacci.SecondApp.Services.Abstract;
using Fibonacci.SecondApp.Services.Implementation;

namespace Fibonacci.SecondApp.App.ServicesExtensions
{
    public static partial class ApplicationExtensions
    {
        public static void AddServicesMappingConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMessageBrokerService, MessageBrokerService>();
            services.AddScoped<INumbersSynchronizationService, NumbersSynchronizationService>();
            services.AddSingleton<ICacheService<int,long>, CacheService<int,long>>();
            services.AddScoped<ICalculationService, CalculationService>();

            services.AddTransient<DataSeeder>();
        }
    }
}
