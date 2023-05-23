namespace PayrollSolution.Domain
{
    public sealed class Payslip : EntityBase<PayslipId>
    {
        private Payslip(PayslipId id, DateOnly payPeriodStartDate, DateOnly payPeriodEndDate, EmployeeSalarySpecification salarySpec,
            IncomeTaxRangeSpecificationId incomeTaxRangeSpecificationId, decimal grossIncome, decimal incomeTax, decimal netIncome,
            decimal super)
        {
            PayslipId = id;
            EmployeeSalarySpecification = salarySpec;
            IncomeTaxRangeSpecificationId = incomeTaxRangeSpecificationId;
            PayPeriodStartDate = payPeriodStartDate;
            PayPeriodEndDate = payPeriodEndDate;
            GrossIncome = grossIncome;
            IncomeTax = incomeTax;
            NetIncome = netIncome;
            Super = super;
        }
        public PayslipId PayslipId { get; private init; }
        protected override PayslipId Id => PayslipId;

        public EmployeeSalarySpecification EmployeeSalarySpecification { get; private init; }

        public IncomeTaxRangeSpecificationId IncomeTaxRangeSpecificationId { get; private init; }

        public DateOnly PayPeriodStartDate { get; private init; }
        public DateOnly PayPeriodEndDate { get; private init; }
        public decimal GrossIncome { get; private init; }
        public decimal IncomeTax { get; private init; }
        public decimal NetIncome { get; private init; }
        public decimal Super { get; private init; }

        public static DomainValidationErrorOr<Payslip> Create(PayslipId id, PayPeriod payPeriod, EmployeeSalarySpecification salarySpec,
            IncomeTaxRangeSpecification incomeTaxRangeSpec)
        {
            //Use IEEE Standard 754, section 4 (Banker's Rounding) to reduce rounding bias when dealing with currency values
            decimal incomeTax = Math.Round(CalculateAnnualIncomeTax(salarySpec.AnnualSalary, incomeTaxRangeSpec) / 12, 2, MidpointRounding.ToEven);
            decimal grossIncome = Math.Round(salarySpec.AnnualSalary / 12, 2, MidpointRounding.ToEven);
            decimal netIncome = grossIncome - incomeTax;
            decimal super = Math.Round(grossIncome * salarySpec.SuperRate, 2, MidpointRounding.ToEven);

            return new Payslip(id, payPeriod.StartDate, payPeriod.EndDate, salarySpec, incomeTaxRangeSpec.IncomeTaxRangeSpecificationId,
                grossIncome, incomeTax, netIncome, super);
        }

        private static decimal CalculateAnnualIncomeTax(decimal annualSalary, IncomeTaxRangeSpecification taxRangeSpecification)
        {
            decimal totalTax = 0m;

            decimal prevRangeUpperBound = 0m;
            foreach (var taxRange in taxRangeSpecification.IncomeTaxRanges)
            {
                if (annualSalary > taxRange.InclusiveUpperBound)
                {
                    totalTax += (taxRange.InclusiveUpperBound - prevRangeUpperBound) * taxRange.TaxRate;
                }
                else
                {
                    totalTax += (annualSalary - prevRangeUpperBound) * taxRange.TaxRate;
                    return totalTax;
                }

                prevRangeUpperBound = taxRange.InclusiveUpperBound;
            }

            totalTax += (annualSalary - prevRangeUpperBound) * taxRangeSpecification.TaxRateForRemainder;

            return totalTax;
        }
    }
}
