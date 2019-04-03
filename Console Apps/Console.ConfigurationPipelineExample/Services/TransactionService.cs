using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

using ConfigurationPipelineExample.Data;
using System.Threading.Tasks;
using System.Linq;

using ConfigurationPipelineExample.Data.Entities;

namespace ConfigurationPipelineExample.Services
{
    public class TransactionService : ITransactionService
    {
        private ExampleDbContext Context { get; }

        private ILogger<TransactionService> Logger { get; }

        public TransactionService(ILogger<TransactionService> logger, ExampleDbContext context)
        {
            Context = context;
            Logger = logger;
            LoadData();
        }

        public int GetCount() => Context.Transactions.Count();

        public decimal GetTotal()
        {
            Logger.LogInformation("Getting Total");
            return Context.Transactions.Sum(x => x.Amount);
        }

        internal void LoadData()
        {
            Logger.LogInformation("Loading Data...");

            var amount = 1.13M;
            for (var i = 1; i <= 51; i++)
            {
                amount *= 1.14M;
                Context.Transactions.Add(new Transaction { Id = i, Amount = Math.Round(amount, 2), AccountId = "A" });
            }

            Context.SaveChanges();
            Logger.LogInformation("Loading Complete");
        }
    }
}
