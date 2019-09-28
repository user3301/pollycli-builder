namespace Models
{
    public class MonthlyPaySlip
    {
        public string FullName { get; set; }
        public uint GrossIncome { get; set; }
        public uint IncomeTax { get; set; }
        public uint NetIncome { get; set; }
        public uint Super { get; set; }
        public string PayPeriod { get; set; }

        public override string ToString()
        {
            return FullName + "," + PayPeriod + "," + GrossIncome + "," + IncomeTax + "," + NetIncome + "," + Super;
        }
    }
}
