using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Configuration
{
    static class Mvc
    {
        public static void ConfigureMvc(this Startup startup, IServiceCollection services)
        {
            // Make controllers require authorization by default.
            services.AddMvc(options =>
            {
                var defaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(defaultPolicy));
            });
        }
    }
}
