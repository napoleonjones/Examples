﻿using System;
using System.IO;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ConfigurationPipelineExample.Data;
using ConfigurationPipelineExample.Services;

namespace ConfigurationPipelineExample
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static ServiceProvider ServiceProvider { get; set; }

        public static ITransactionService TransactionService { get; set; }        

        static void Main(string[] args)
        {
            ServiceProvider = ConfigureServices();

            var transactionService = ServiceProvider.GetService<ITransactionService>();

            Console.WriteLine($"Transaction Count:  {transactionService.GetCount()} \tTransaction Total:  ${transactionService.GetTotal()}");
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            services.AddSingleton(new LoggerFactory());

            services.AddLogging(config => config.AddConsole())
                .Configure<LoggerFilterOptions>(config => config.MinLevel = LogLevel.Information);

            

            Configuration = builder.Build();

            services.AddSingleton<IConfiguration>(Configuration);

            var options = new DbContextOptionsBuilder<ExampleDbContext>()
                 .UseInMemoryDatabase("ExampleDB")
                 .Options;

            services.AddSingleton<ExampleDbContext>(_ => new ExampleDbContext(options));
            services.AddScoped<ITransactionService, TransactionService>();

            return services.BuildServiceProvider();
        }
    }
}
