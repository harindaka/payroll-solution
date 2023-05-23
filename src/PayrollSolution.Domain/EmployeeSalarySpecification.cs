namespace PayrollSolution.Domain
{
    /// <summary>
    /// Since employee salary information is entered manually during each request, rather than iterating an employee list
    /// from a data store for example, this class is designed as a value object owned by the Payslip aggregate 
    /// rather than a dedicated entity. In a real world scenario this would not be the case.
    /// </summary>
    public class EmployeeSalarySpecification : ValueObjectBase
    {
        private EmployeeSalarySpecification(EmployeeName name, decimal annualSalary, decimal superRate)
        {
            Name = name;
            AnnualSalary = annualSalary;
            SuperRate = superRate;
        }

        public EmployeeName Name { get; private init; }
        public decimal AnnualSalary { get; private init; }
        public decimal SuperRate { get; private init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return AnnualSalary;
            yield return SuperRate;
        }

        public static DomainValidationErrorOr<EmployeeSalarySpecification> Create(EmployeeName name, decimal annualSalary, decimal superRate)
        {
            if (annualSalary <= 0)
            {
                return DomainValidationError.InvalidAnnualSalary;
            }

            if (superRate < 0 || superRate > 1)
            {
                return DomainValidationError.InvalidSuperRate;
            }

            return new EmployeeSalarySpecification(name, annualSalary, superRate);
        }
    }
}
