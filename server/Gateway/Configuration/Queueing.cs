using Gateway.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Configuration
{
    internal static class Queueing
    {
        public static void ConfigureQueueing(this Startup startup, IServiceCollection services)
        {
            var service = new QueueingService(startup.Queueing);
            services.AddSingleton(service);
        }
    }
}
