using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvBet.TestTask.Worker
{
    internal interface IWorker
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
