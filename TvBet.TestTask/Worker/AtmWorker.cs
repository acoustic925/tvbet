using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvBet.TestTask.ATM;
using TvBet.TestTask.Persistence;

namespace TvBet.TestTask.Worker
{
    internal class AtmWorker : IWorker
    {
        private readonly IDataService _dataService;

        public AtmWorker(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Please enter file path: ");
            var filePath = Console.ReadLine();

            var (banknotes, denominations) = _dataService.GetDataFromFile(filePath, stoppingToken);
            var atm = Atm.CreateAtm(banknotes, denominations);

            while(!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Please enter an amount: ");
                var textAmount = Console.ReadLine();

                var amount = int.Parse(textAmount);

                var notes = atm.GetNotes(amount);

                if(!notes.Any())
                {
                    Console.WriteLine("ATM has no required amount.");
                }
                else
                {
                    Console.WriteLine(ConvertToPresentation(notes));
                }
            }
        }

        private string ConvertToPresentation(int[] notes)
        {
            var group = notes.GroupBy(n => n);

            var builder = new StringBuilder();

            foreach (var gr in group)
            {
                builder.Append($"{gr.Key} x {gr.Count()}; ");
            }

            return builder.ToString();
        }
    }
}
