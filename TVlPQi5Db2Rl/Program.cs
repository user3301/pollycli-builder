using FileIOService.Implementations;
using FileIOService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculationService;

namespace TVlPQi5Db2Rl
{
    class Program
    {
        private static readonly AutoResetEvent waitHandle = new AutoResetEvent(false);

        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //set up DI
            var serviceProvider = new ServiceCollection()
                .Configure<AppSettings>(configuration)
                .AddSingleton<IFileIOService<string, List<EmployeeDetails>>, CSVFileService>()
                .AddSingleton<ITaxBracketHandler<TaxPlan>, TaxBracketHandler>()
                .AddTransient<ITaxCalculationHandler<EmployeeDetails, MonthlyPaySlip>, TaxCalculationHandler>()
                .BuildServiceProvider();

            var CsvFileProcessor = serviceProvider.GetService<IFileIOService<string, List<EmployeeDetails>>>();
            var taxHandler = serviceProvider.GetService<ITaxBracketHandler<TaxPlan>>();
            var taxCalculationHandler = serviceProvider.GetService<ITaxCalculationHandler<EmployeeDetails, MonthlyPaySlip>>();
            Console.WriteLine("Welcome To Tax Calculator!");


            _ = Task.Run(async () =>
                  {
                      while (true)
                      {
                          try
                          {
                              Console.WriteLine("please specify your csv file path in full or type 'exit' to stop:");
                              string line = Console.ReadLine();
                              if (line == "exit")
                              {
                                  Console.WriteLine("Bye!");
                                  break;
                              }
                              Console.WriteLine("Your csv input:");
                              var employees = await CsvFileProcessor.ReadAsync(line);
                              employees.ForEach(e => Console.WriteLine(e.ToString()));
                              Console.WriteLine("Tax calculation outcome:");
                              foreach (var employee in employees)
                              {
                                  taxCalculationHandler.SetEmployeeDetails(employee);
                                  var payslip = taxCalculationHandler.GeneratePaySlip();
                                  Console.WriteLine(payslip.ToString());
                              }
                          }
                          catch (Exception ex)
                          {
                              Console.WriteLine(ex.Message);
                              if (ex.InnerException != null)
                              {
                                  Console.WriteLine("Inner Exception: ");
                                  Console.WriteLine(ex.InnerException.Message);
                              }
                              Console.WriteLine("Stack Trace: ");
                              Console.WriteLine(ex.StackTrace);
                          }
                      }
                  }

            );

            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Exit");

                waitHandle.Set();
            };


            waitHandle.WaitOne();
        }
    }
}
