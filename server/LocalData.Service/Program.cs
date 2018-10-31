using QueueHandler;
using RabbitMQ.Client;
using System;

namespace LocalData.Service
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Local Data Service";

            var queueing = ConfigurationManager.Queueing;
            var factory = new ConnectionFactory() { HostName = queueing.HostName };
            using (new LocalDataWorker(factory))
            {
                HoldApplicationOpen();
            }
        }

        private static void HoldApplicationOpen()
        {
            try
            {
                var input = string.Empty;
                do
                {
                    Console.WriteLine("Type exit to close the application: ");
                    input = Console.ReadLine().ToUpperInvariant();
                } while (input != "EXIT");
            }
            catch (Exception ex)
            {
                Environment.Exit(-1);
            }
            Environment.Exit(0);
        }
    }
}
