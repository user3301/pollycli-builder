namespace Models
{
    public class EmployeeDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint AnnualSalary { get; set; }
        private decimal superRate;
        public decimal SuperRate
        {
            get { return superRate; }
            set { superRate = value / 100; }
        }
        public string PayPeriod { get; set; }

        public override string ToString() => FirstName + "," + LastName + "," + AnnualSalary + "," + superRate + "," + PayPeriod;

    }
}
