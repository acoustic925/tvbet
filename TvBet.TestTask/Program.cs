using Microsoft.Extensions.DependencyInjection;
using TvBet.TestTask.Worker;

namespace TvBet.TestTask
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var provider = ConfigureServiceProvider();

            try
            {
                var stoppingToken = CreateStoppingToken();

                var worker = provider.GetRequiredService<IWorker>();
                await worker.DoWorkAsync(stoppingToken);
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine("Stop process...");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static IServiceProvider ConfigureServiceProvider()
        {
            var servicer = new ServiceCollection();

            servicer.AddAtm();

            return servicer.BuildServiceProvider();
        }

        private static CancellationToken CreateStoppingToken()
        {
            var cts = new CancellationTokenSource();

            Console.CancelKeyPress += (obj, e) =>
            {
                Console.WriteLine("Cancelling...");
                cts.Cancel();
                e.Cancel = true;
            };

            return cts.Token;
        }
    }
}
