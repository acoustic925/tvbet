using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TvBet.TestTask.Persistence;
using TvBet.TestTask.Worker;

namespace TvBet.TestTask
{
    internal static class DependencyInjection
    {
        public static IServiceCollection AddAtm(this IServiceCollection services)
        {
            services.AddTransient<IWorker, AtmWorker>();
            services.AddTransient<IDataService, AtmDataService>();

            return services;
        }
    }
}
