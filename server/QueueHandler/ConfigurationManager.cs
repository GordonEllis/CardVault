using Microsoft.Extensions.Configuration;
using System.IO;

namespace QueueHandler
{
    public static class ConfigurationManager
    {
        private static IConfigurationRoot _configuration;
        private static QueueingConfiguration _queueing;

        public static IConfigurationRoot Configuration => _configuration ?? (_configuration = new ConfigurationBuilder()
                                                              .SetBasePath(Directory.GetCurrentDirectory())
                                                              .AddJsonFile("appsettings.json")
                                                              .Build());

        public static QueueingConfiguration Queueing => _queueing ?? (_queueing = Configuration.GetSection(nameof(QueueingConfiguration))
                                                            .Get<QueueingConfiguration>());
    }
}
