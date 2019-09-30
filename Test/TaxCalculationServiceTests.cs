using Models;
using System;
using System.Collections.Generic;
using TaxCalculationService;
using Xunit;

namespace Test
{
    public class TaxCalculationServiceTests
    {
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
        [MemberData(nameof(GetSuccessData))]
        public void GetMonthlyPaySlip_Success(EmployeeDetails employeeDetails1, EmployeeDetails employeeDetails2)
        {
            var taxHandler1 = new TaxCalculationHandler(employeeDetails1);
            var grossIncome1 = taxHandler1.GetGrossIncome();
            var incomeTax1 = taxHandler1.GetIncomeTax();
            var netIncome1 = taxHandler1.GetNetIncome();
            var super1 = taxHandler1.GetSuper();

            Assert.Equal(5_004U, grossIncome1);
            Assert.Equal(922U, incomeTax1);
            Assert.Equal(4_082U, netIncome1);
            Assert.Equal(450U, super1);

            var taxHandler2 = new TaxCalculationHandler(employeeDetails2);
            var grossIncome2 = taxHandler2.GetGrossIncome();
            var incomeTax2 = taxHandler2.GetIncomeTax();
            var netIncome2 = taxHandler2.GetNetIncome();
            var super2 = taxHandler2.GetSuper();

            Assert.Equal(10_000U, grossIncome2);
            Assert.Equal(2_669U, incomeTax2);
            Assert.Equal(7_331U, netIncome2);
            Assert.Equal(1_000U, super2);
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

            var taxHandler = new TaxCalculationHandler(employeeDetails);

            Assert.Throws(exception, () => taxHandler.GenerateMonthlyPaySlip());
        }
    }
}
