using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Configuration
{
    static class Mvc
    {
        public static void ConfigureMvc(this Startup startup, IServiceCollection services)
        {
            services.AddMvc();
        }
    }
}
