using Models;

namespace TaxCalculationService
{
    public class TaxStrategyFactory
    {
        public static TaxPlan GetInstance(in EmployeeDetails employeeDetails)
        {
            switch (employeeDetails.AnnualSalary)
            {
                case decimal d when (d >= TaxBracketTable.FirstLevel.LowerBound && d <= TaxBracketTable.FirstLevel.UpperBound):
                    return TaxBracketTable.FirstLevel;
                case decimal d when (d >= TaxBracketTable.SecondLevel.LowerBound && d <= TaxBracketTable.SecondLevel.UpperBound):
                    return TaxBracketTable.SecondLevel;
                case decimal d when (d >= TaxBracketTable.ThirdLevel.LowerBound && d <= TaxBracketTable.ThirdLevel.UpperBound):
                    return TaxBracketTable.ThirdLevel;
                case decimal d when (d >= TaxBracketTable.FourthLevel.LowerBound && d <= TaxBracketTable.FourthLevel.UpperBound):
                    return TaxBracketTable.FourthLevel;
                case decimal d when (d >= TaxBracketTable.FifthLevel.LowerBound && d <= TaxBracketTable.FifthLevel.UpperBound):
                    return TaxBracketTable.FifthLevel;
                default:
                    throw new System.Exception("Salary is not in a valid range.");

            }
        }
    }
}
