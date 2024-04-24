using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvBet.TestTask.Persistence
{
    internal interface IDataService
    {
        (List<int> Banknotes, List<int> Denomintations) GetDataFromFile(string filePath, CancellationToken stoppingToken);
    }
}
