namespace TaxCalculationService
{
    public interface ITaxBracketHandler<T> where T : class
    {
        T GetTaxPlan(uint salary);
    }
}
