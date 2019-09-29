using Models;
using System;

namespace TaxCalculationService
{
    public sealed class TaxCalculationHandler
    {
        readonly TaxPlan _taxPlan;
        readonly EmployeeDetails _employeeDetails;
        const int PAY_PERIOD = 12;
        public TaxCalculationHandler(EmployeeDetails employeeDetails)
        {
            _employeeDetails = employeeDetails ?? throw new ArgumentNullException(nameof(employeeDetails));
            _taxPlan = TaxStrategyFactory.GetInstance(employeeDetails);
        }

        public uint GetGrossIncome() => Round(_employeeDetails.AnnualSalary / PAY_PERIOD);

        public uint GetIncomeTax() => Round((_taxPlan.TaxBase + (_employeeDetails.AnnualSalary - _taxPlan.LowerBound + 1) * _taxPlan.SurCharge) / PAY_PERIOD);

        public uint GetNetIncome() => checked(GetGrossIncome() - GetIncomeTax());

        public uint GetSuper() => Round(GetGrossIncome() * _employeeDetails.SuperRate);

        public MonthlyPaySlip GenerateMonthlyPaySlip()
        {
            var fullName = $"{_employeeDetails.FirstName} {_employeeDetails.LastName}";
            var gross = GetGrossIncome();
            var tax = GetIncomeTax();
            var net = GetNetIncome();
            var super = GetSuper();
            var payPeriod = _employeeDetails.PayPeriod;
            return new MonthlyPaySlip
            {
                FullName = fullName,
                GrossIncome = gross,
                IncomeTax = tax,
                NetIncome = net,
                Super = super,
                PayPeriod = payPeriod
            };
        }


        uint Round(in decimal number) => (uint)checked(Math.Round(number, MidpointRounding.AwayFromZero));
    }
}
