using Models;
using System;
using TaxCalculationService.Interfaces;

namespace TaxCalculationService
{
    public abstract class TaxStrategyBase : ITaxStrategy
    {
        readonly EmployeeDetails _employeeDetails;
        const uint PAYMENT_RANGE = 12;

        protected TaxStrategyBase(EmployeeDetails employeeDetails)
        {
            _employeeDetails = employeeDetails;
        }

        public abstract uint GetIncomeTax();
        public abstract uint GetNetIncome();

        public uint GetGrossIncome() => RoundDown(_employeeDetails.AnnualSalary / PAYMENT_RANGE);
        public uint GetSuper() => RoundDown(_employeeDetails.AnnualSalary * _employeeDetails.SuperRate);

        protected uint RoundDown(in decimal number) => checked((uint)Math.Floor(number));
        protected uint RoundUp(in decimal number) => checked((uint)Math.Ceiling(number));
    }
}
