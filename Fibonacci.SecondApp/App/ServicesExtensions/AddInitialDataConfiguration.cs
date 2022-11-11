namespace Fibonacci.SecondApp.App.ServicesExtensions
{
    public static partial class ApplicationExtensions
    {
        public static void InitializeDataConfiguration(this IApplicationBuilder app)
        {            
            var dataSeeder = app.ApplicationServices.GetRequiredService<DataSeeder>();
            dataSeeder.InitializeFirstFibonacciNumbers();
        }
    }
}
