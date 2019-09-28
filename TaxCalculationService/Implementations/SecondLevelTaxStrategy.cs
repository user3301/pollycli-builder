using Models;

namespace TaxCalculationService.Implementations
{
    public class SecondLevelTaxStrategy : TaxStrategyBase
    {
        public SecondLevelTaxStrategy(EmployeeDetails employeeDetails) : base(employeeDetails)
        {
        }

        public override uint GetIncomeTax()
        {
            throw new System.NotImplementedException();
        }

        public override uint GetNetIncome()
        {
            throw new System.NotImplementedException();
        }
    }
}
