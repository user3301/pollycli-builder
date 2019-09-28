using FileIOProcessor.Implementations;
using FileIOProcessor.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TVlPQi5Db2Rl
{
    class Program
    {
        static void Main(string[] args)
        {
            //set up DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileIOService, CSVFileService>()
                .BuildServiceProvider();




        }
    }
}
