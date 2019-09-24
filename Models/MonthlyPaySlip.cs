namespace Models
{
    public class MonthlyPaySlip
    {
        public string FullName { get; set; }
        public int GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public int NetIncome { get; set; }
        public int Super { get; set; }
        public string PayPeriod { get; set; }
    }
}
