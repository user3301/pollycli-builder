using Models;
using System;
using System.Collections.Generic;
using TaxCalculationService;
using Xunit;

namespace Test
{
    public class TestStrategyFactoryTests
    {
        public static IEnumerable<object[]> GetSuccessData()
        {
            return new[]
            {
                new object[] { 1M, TaxBracketTable.FirstLevel},
                new object[] { 18_201M, TaxBracketTable.SecondLevel },
                new object[] { 37_100M , TaxBracketTable.ThirdLevel},
                new object[] { 87_123M, TaxBracketTable.FourthLevel},
                new object[] { 180_123M ,TaxBracketTable.FifthLevel}
            };
        }

        [Theory(Timeout = 10_000)]
        [MemberData(nameof(GetSuccessData))]
        public void GetTaxPlan_Success(decimal salary, TaxPlan expectedTaxPlan)
        {
            var actual = TaxStrategyFactory.GetInstance(new EmployeeDetails { AnnualSalary = salary });
            Assert.Equal(expectedTaxPlan.TaxBase, actual.TaxBase);
            Assert.Equal(expectedTaxPlan.SurCharge, actual.SurCharge);
            Assert.Equal(expectedTaxPlan.LowerBound, actual.LowerBound);
            Assert.Equal(expectedTaxPlan.UpperBound, actual.UpperBound);
        }

        [Fact(Timeout = 10_000)]
        public void GetTaxPlan_Fail_With_Negative_Salary()
        {
            var salary = -1000M;
            var employDetails = new EmployeeDetails { AnnualSalary = salary };
            var exception = Assert.Throws<Exception>(() => TaxStrategyFactory.GetInstance(employDetails));
            Assert.Equal("Salary is not in a valid range.", exception.Message);
        }
    }
}
