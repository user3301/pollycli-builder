namespace Models
{
    public class TaxPlan
    {
        public decimal SurCharge { get; set; }
        public uint TaxBase { get; set; }
        public uint LowerBound { get; set; }
        public uint UpperBound { get; set; }
    }
}
