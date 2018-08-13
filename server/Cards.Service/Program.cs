using QueueHandler;
using RabbitMQ.Client;
using System;

namespace Cards.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cards Service";

            var queueing = ConfigurationManager.Queueing;
            var factory = new ConnectionFactory() { HostName = queueing.HostName };
            using (var cards = new CardsWorker(factory))
            {
                HoldApplicationOpen();
            }
        }

        static void HoldApplicationOpen()
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
                Console.WriteLine("Error Occurred:", ex);
                Environment.Exit(-1);
            }
            Environment.Exit(0);
        }
    }
}
