using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Configuration
{
    static class Authentication
    {
        public static void ConfigureAuthentication(this Startup startup, IServiceCollection services)
        {
            //services.AddAuthentication("Bearer")
            //      .AddIdentityServerAuthentication(options =>
            //      {
            //          options.ApiName = startup.Authentication.ApiName;
            //          options.Authority = startup.Authentication.Authority;
            //          options.RequireHttpsMetadata = startup.Authentication.RequireHttps;
            //      });
        }
    }
}
