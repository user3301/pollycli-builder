using Models;

namespace TaxCalculationService.Implementations
{
    public class FirstLevelTaxStrategy : TaxStrategyBase
    {
        readonly (uint plus, decimal charge, uint low, uint high) _taxBracket;
        public FirstLevelTaxStrategy(EmployeeDetails employeeDetails) : base(employeeDetails)
        {
            _taxBracket = TaxBracket.FirstLevelRange;
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
