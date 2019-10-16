using Models;
using System;

namespace TaxCalculationService
{
    public sealed class TaxCalculationHandler : ITaxCalculationHandler<EmployeeDetails, MonthlyPaySlip>
    {
        readonly ITaxBracketHandler<TaxPlan> _taxHandler;
        EmployeeDetails _employeeDetails;
        const int PAY_PERIOD = 12;
        public TaxCalculationHandler(ITaxBracketHandler<TaxPlan> taxHandler)
        {
            _taxHandler = taxHandler;
        }

        public void SetEmployeeDetails(EmployeeDetails details) => _employeeDetails = details;

        public uint GetGrossIncome() => Round(_employeeDetails.AnnualSalary / PAY_PERIOD);

        public uint GetIncomeTax()
        {
            var taxPlan = _taxHandler.GetTaxPlan(_employeeDetails.AnnualSalary);
            return Round((taxPlan.TaxBase + (_employeeDetails.AnnualSalary - taxPlan.LowerBound + 1) * taxPlan.SurCharge) / PAY_PERIOD);
        }

        public uint GetNetIncome() => checked(GetGrossIncome() - GetIncomeTax());

        public uint GetSuper() => Round(GetGrossIncome() * _employeeDetails.SuperRate);

        public MonthlyPaySlip GeneratePaySlip()
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
