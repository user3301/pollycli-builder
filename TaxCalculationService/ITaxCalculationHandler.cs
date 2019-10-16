namespace TaxCalculationService
{
    public interface ITaxCalculationHandler<TIn, TOut>
    {
        TOut GeneratePaySlip();

        void SetEmployeeDetails(TIn i);
    }
}
