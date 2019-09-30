using Models;
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
                new object[] { 1U, TaxBracketTable.FirstLevel},
                new object[] { 18_201U, TaxBracketTable.SecondLevel },
                new object[] { 37_100U , TaxBracketTable.ThirdLevel},
                new object[] { 87_123U, TaxBracketTable.FourthLevel},
                new object[] { 180_123U ,TaxBracketTable.FifthLevel}
            };
        }

        [Theory(Timeout = 10_000)]
        [MemberData(nameof(GetSuccessData))]
        public void GetTaxPlan_Success(uint salary, TaxPlan expectedTaxPlan)
        {
            var actual = TaxStrategyFactory.GetInstance(new EmployeeDetails { AnnualSalary = salary });
            Assert.Equal(expectedTaxPlan.TaxBase, actual.TaxBase);
            Assert.Equal(expectedTaxPlan.SurCharge, actual.SurCharge);
            Assert.Equal(expectedTaxPlan.LowerBound, actual.LowerBound);
            Assert.Equal(expectedTaxPlan.UpperBound, actual.UpperBound);
        }
    }
}
