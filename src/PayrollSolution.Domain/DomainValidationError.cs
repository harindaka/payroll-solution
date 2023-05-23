namespace PayrollSolution.Domain
{
    public class DomainValidationError : EnumerationBase
    {
        public DomainValidationError(int id, string name)
            : base(id, name)
        {
        }

        public static DomainValidationError DefaultGuidIdsNotAllowed =>
            new(100, "Default guids are not allowed as identifiers");

        public static DomainValidationError InvalidFirstName =>
            new(1000, "Specified first name is invalid");
        public static DomainValidationError InvalidLastName =>
            new(1001, "Specified last name is invalid");

        public static DomainValidationError InvalidUpperBoundForTaxRange =>
            new(1002, "Specified upper bound is not valid for a tax range");
        public static DomainValidationError TaxRateNotWithinValidRange =>
            new(1003, "Specified tax rate is out of valid range 0 - 1");
        public static DomainValidationError TaxRangeCollision =>
            new(1004, "Specified tax ranges are not mutually exclusive");

        public static DomainValidationError YearOutOfRange =>
            new(1005, "Specified year is out of range");

        public static DomainValidationError InvalidAnnualSalary =>
            new(1006, "Specified annual salary is invalid");
        public static DomainValidationError InvalidSuperRate =>
            new(1007, "Specified super rate is invalid");

    }
}
