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
            _employeeDetails = employeeDetails;
            _taxPlan = TaxStrategyFactory.GetInstance(employeeDetails);
        }

        public uint GetGrossIncome() => RoundDown(_employeeDetails.AnnualSalary / PAY_PERIOD);

        public uint GetIncomeTax() => RoundUp((_taxPlan.TaxBase + (_employeeDetails.AnnualSalary - _taxPlan.LowerBound + 1) * _taxPlan.SurCharge)) / PAY_PERIOD;

        public uint GetNetIncome() => GetGrossIncome() - GetIncomeTax();

        public uint GetSuper() => RoundDown(GetGrossIncome() * _employeeDetails.SuperRate);

        public MonthlyPaySlip GenerateMonthlyPaySlip()
        {
            return new MonthlyPaySlip
            {
                FullName = _employeeDetails.FirstName + " " + _employeeDetails.LastName,
                GrossIncome = GetGrossIncome(),
                IncomeTax = GetIncomeTax(),
                NetIncome = GetNetIncome(),
                Super = GetSuper(),
                PayPeriod = _employeeDetails.PayPeriod
            };
        }


        uint RoundDown(in decimal number) => (uint)checked(Math.Floor(number));
        uint RoundUp(in decimal number) => (uint)checked(Math.Ceiling(number));
    }
}
