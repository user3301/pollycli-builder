using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using TaxCalculationService;
using Xunit;
namespace Test
{
    public class TaxCalculationServiceTests
    {
        public ITaxBracketHandler<TaxPlan> TaxBracketHandler { get; set; }
        public ITaxCalculationHandler<EmployeeDetails, MonthlyPaySlip> TaxCalculationHandler { get; set; }

        public TaxCalculationServiceTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<AppSettings>(configuration)
                .AddSingleton<ITaxBracketHandler<TaxPlan>, TaxBracketHandler>()
                .AddTransient<ITaxCalculationHandler<EmployeeDetails, MonthlyPaySlip>, TaxCalculationHandler>()
                .BuildServiceProvider();

            TaxBracketHandler = serviceProvider.GetService<ITaxBracketHandler<TaxPlan>>();
            TaxCalculationHandler = serviceProvider.GetService<ITaxCalculationHandler<EmployeeDetails, MonthlyPaySlip>>();

        }
        public static IEnumerable<object[]> GetSuccessData()
        {
            yield return new object[]
            {
                new EmployeeDetails { FirstName = "David", LastName = "Rudd", AnnualSalary = 60_050U, SuperRate = 9M, PayPeriod = "01 March - 31 March"},
                new EmployeeDetails { FirstName = "Ryan", LastName = "Chen", AnnualSalary = 120_000U, SuperRate = 10M, PayPeriod = "01 March - 01 March"}
            };
        }

        public static IEnumerable<object[]> GetFailedData()
        {
            return new[]
            {
                // super is negative
                new object[]{"James", "Gosling", 85_000U, -1M, "01 March - 31 March", typeof(OverflowException) },
            };
        }

        [Theory(Timeout = 10_000)]
        [MemberData(nameof(GetFailedData))]
        public void GetMonthlyPaySlip_Exception(string firstName, string lastName, uint annualSalary, decimal super, string payPeriod, Type exception)
        {
            var employeeDetails = new EmployeeDetails
            {
                FirstName = firstName,
                LastName = lastName,
                AnnualSalary = annualSalary,
                SuperRate = super,
                PayPeriod = payPeriod
            };

            TaxCalculationHandler.SetEmployeeDetails(employeeDetails);

            Assert.Throws(exception, () => TaxCalculationHandler.GeneratePaySlip());
        }
    }
}
