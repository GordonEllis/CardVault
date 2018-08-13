using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Configuration
{
    static class Authorization
    {
        public static void ConfigureAuthorization(this Startup startup, IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("dealings", builder =>
                {
                    builder.RequireRole(Roles.Dealings);
                });
                options.AddPolicy("investments", builder =>
                {
                    builder.RequireRole(Roles.Investments);
                });
                options.AddPolicy("settlements", builder =>
                {
                    builder.RequireRole(Roles.Settlements);
                });
            });
        }
    }
}
