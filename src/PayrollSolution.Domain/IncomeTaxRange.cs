namespace PayrollSolution.Domain
{
    public class IncomeTaxRange : ValueObjectBase
    {
        private IncomeTaxRange(decimal inclusiveUpperBound, decimal taxRate)
        {
            InclusiveUpperBound = inclusiveUpperBound;
            TaxRate = taxRate;
        }

        public decimal InclusiveUpperBound { get; private init; }

        public decimal TaxRate { get; private init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InclusiveUpperBound;
            yield return TaxRate;
        }

        public static DomainValidationErrorOr<IncomeTaxRange> Create(decimal upTo, decimal taxRate)
        {
            if (upTo <= 0)
            {
                return DomainValidationError.InvalidUpperBoundForTaxRange;
            }

            if (taxRate < 0 || taxRate > 1)
            {
                return DomainValidationError.TaxRateNotWithinValidRange;
            }

            return new IncomeTaxRange(upTo, taxRate);
        }
    }
}
