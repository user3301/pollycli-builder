using FileIOProcessor.Implementations;
using FileIOProcessor.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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

            var employees = await CsvFileProcessor.ReadAsync(@"..\..\..\..\TestData\test_input.csv");

            foreach (var employee in employees)
            {
                var handler = new TaxCalculationHandler(employee);
                var payslip = handler.GenerateMonthlyPaySlip();
                System.Console.WriteLine(payslip.ToString());
            }
        }
    }
}
