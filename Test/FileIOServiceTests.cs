using FileIOService.Implementations;
using FileIOService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class FileIOServiceTests
    {

        public IFileIOService CSVFileService { get; set; }
        public FileIOServiceTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IFileIOService, CSVFileService>()
                .BuildServiceProvider();
            CSVFileService = serviceProvider.GetService<IFileIOService>();
        }


        [Fact(Timeout = 1_000)]
        public async Task ReadAsync_Invalid_FilePath_Exception()
        {
            var invalidPath = @"..\..\..\..\TestData\invalid.csv";
            await Assert.ThrowsAsync<FileNotFoundException>(async () => await CSVFileService.ReadAsync(invalidPath).ConfigureAwait(false));
        }

        [Fact(Timeout = 1_000)]
        public async Task First_Name_Null_Exception()
        {
            var path = @"..\..\..\..\TestData\first_name_null.csv";
            var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await CSVFileService.ReadAsync(path).ConfigureAwait(false));
            Assert.Equal("First name cannot be null or empty.", exception.Message);
        }

        [Fact(Timeout = 1_000)]
        public async Task Last_Name_Empty_Exception()
        {
            var path = @"..\..\..\..\TestData\last_name_empty.csv";
            var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await CSVFileService.ReadAsync(path).ConfigureAwait(false));
            Assert.Equal("Last name cannot be null or empty.", exception.Message);
        }

        [Fact(Timeout = 1_000)]
        public async Task Salary_Negative_Exception()
        {
            var path = @"..\..\..\..\TestData\salary_negative.csv";
            var exception = await Assert.ThrowsAsync<InvalidDataException>(async () => await CSVFileService.ReadAsync(path).ConfigureAwait(false));
            Assert.Equal("Value was either too large or too small for a UInt32.", exception.Message);
        }

        [Fact(Timeout = 1_000)]
        public async Task ReadAsync_Success()
        {
            var path = @"..\..\..\..\TestData\test_input.csv";
            var employeeDetails = await CSVFileService.ReadAsync(path).ConfigureAwait(false);
            Assert.NotNull(employeeDetails);
            Assert.Equal(2, employeeDetails.Count);
            Assert.Equal("David", employeeDetails[0].FirstName);
            Assert.Equal("Rudd", employeeDetails[0].LastName);
            Assert.Equal(60_050M, employeeDetails[0].AnnualSalary);
            Assert.Equal(0.09M, employeeDetails[0].SuperRate);
            Assert.Equal("01 March - 31 March", employeeDetails[0].PayPeriod);

            Assert.Equal("Ryan", employeeDetails[1].FirstName);
            Assert.Equal("Chen", employeeDetails[1].LastName);
            Assert.Equal(120_000M, employeeDetails[1].AnnualSalary);
            Assert.Equal(0.1M, employeeDetails[1].SuperRate);
            Assert.Equal("01 March - 31 March", employeeDetails[1].PayPeriod);
        }
    }
}
