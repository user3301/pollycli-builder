using FileIOProcessor.Implementations;
using FileIOProcessor.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            //set up DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileIOService, CSVFileService>()
                .BuildServiceProvider();

            var CsvFileProcessor = serviceProvider.GetService<IFileIOService>();
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
                                  var handler = new TaxCalculationHandler(employee);
                                  var payslip = handler.GenerateMonthlyPaySlip();
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

            // Handle Control+C or Control+Break
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Exit");

                // Allow the manin thread to continue and exit...
                waitHandle.Set();
            };

            // Wait
            waitHandle.WaitOne();
        }
    }
}
