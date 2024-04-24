using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvBet.TestTask.Persistence
{
    internal class AtmDataService : IDataService
    {
        public (List<int>, List<int>) GetDataFromFile(string filePath, CancellationToken stoppingToken)
        {
            var data = new List<int>(2048);
            var denominations = new List<int>(30);

            string line;

            using var reader = new StreamReader(filePath);
            while ((line = reader.ReadLine()) != null)
            {
                var (denomination, notesNumber) = ParseString(line);

                denominations.Add(denomination);
                data.AddRange(GenerateBanknoteList(denomination, notesNumber));
            }

            return (data, denominations);
        }

        private static List<int> GenerateBanknoteList(int denomination, int count) => Enumerable.Repeat(denomination, count).ToList();

        private static (int, int) ParseString(string line)
        {
            var splitedLine = line.Split('-');

            var denomination = int.Parse(splitedLine[0]);
            var notesNumber = int.Parse(splitedLine[1]);

            return (denomination, notesNumber);
        }
    }
}
