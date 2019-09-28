namespace TaxCalculationService.Interfaces
{
    public interface ITaxStrategy
    {
        uint GetGrossIncome();
        uint GetIncomeTax();
        uint GetNetIncome();
        uint GetSuper();
    }
}
