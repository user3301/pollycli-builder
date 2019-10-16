using Microsoft.Extensions.Options;
using Models;
using System;
using System.Linq;

namespace TaxCalculationService
{
    public class TaxBracketHandler : ITaxBracketHandler<TaxPlan>
    {
        readonly AppSettings _config;

        public TaxBracketHandler(IOptions<AppSettings> config)
        {
            _config = config.Value;
        }

        public TaxPlan GetTaxPlan(uint salary)
        {
            var ans = _config.TaxBracketsForDifferentYears[_config.CurrentYear].SingleOrDefault(x => salary >= x.LowerBound && salary <= x.UpperBound);
            return ans ?? throw new NullReferenceException();
        }
    }
}
