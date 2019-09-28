using FileIOProcessor.Implementations;
using FileIOProcessor.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TaxCalculationService;

namespace TVlPQi5Db2Rl
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //set up DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileIOService, CSVFileService>()
                .BuildServiceProvider();

            var CsvFileProcessor = serviceProvider.GetService<IFileIOService>();

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var employees = await CsvFileProcessor.ReadAsync(@"..\..\..\test.csv");

            foreach (var employee in employees)
            {
                var handler = new TaxCalculationHandler(employee);
                var payslip = handler.GenerateMonthlyPaySlip();
                System.Console.WriteLine(payslip.ToString());
            }
        }
    }
}
