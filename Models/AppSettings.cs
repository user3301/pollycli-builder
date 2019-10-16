using System.Collections.Generic;

namespace Models
{
    public class AppSettings
    {
        public Dictionary<string, IList<TaxPlan>> TaxBracketsForDifferentYears { get; set; } = new Dictionary<string, IList<TaxPlan>>();
        public string CurrentYear { get; set; }
    }
}
