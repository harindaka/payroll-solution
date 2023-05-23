namespace PayrollSolution.Domain
{
    public sealed class IncomeTaxRangeSpecification : EntityBase<IncomeTaxRangeSpecificationId>
    {
        private IncomeTaxRangeSpecification(IncomeTaxRangeSpecificationId id, IReadOnlyList<IncomeTaxRange> taxRanges,
            decimal taxRateForRemainder)
        {
            IncomeTaxRangeSpecificationId = id;
            IncomeTaxRanges = taxRanges;
            TaxRateForRemainder = taxRateForRemainder;
        }

        public IncomeTaxRangeSpecificationId IncomeTaxRangeSpecificationId { get; private init; }
        protected override IncomeTaxRangeSpecificationId Id => IncomeTaxRangeSpecificationId;


        public IReadOnlyList<IncomeTaxRange> IncomeTaxRanges { get; private init; }
        public decimal TaxRateForRemainder { get; private init; }

        public static DomainValidationErrorOr<IncomeTaxRangeSpecification> CreateFromFlatRate(IncomeTaxRangeSpecificationId id,
            decimal flatTaxRate)
        {
            return CreateFromRanges(id, Enumerable.Empty<IncomeTaxRange>(), flatTaxRate);
        }

        public static DomainValidationErrorOr<IncomeTaxRangeSpecification> CreateFromRanges(IncomeTaxRangeSpecificationId id,
            IEnumerable<IncomeTaxRange> taxRanges, decimal taxRateForRemainder)
        {
            if (taxRateForRemainder < 0 || taxRateForRemainder > 1)
            {
                return DomainValidationError.TaxRateNotWithinValidRange;
            }

            var taxRangeList = taxRanges.ToList();

            if (taxRangeList.Any())
            {
                if (taxRangeList.Count > 1)
                {
                    var endIndex = taxRangeList.Count - 1;
                    for (var i = 0; i < endIndex; i++)
                    {
                        if (taxRangeList[i].InclusiveUpperBound >= taxRangeList[i + 1].InclusiveUpperBound)
                        {
                            return DomainValidationError.TaxRangeCollision;
                        }
                    }
                }
            }

            return new IncomeTaxRangeSpecification(id, taxRangeList.AsReadOnly(), taxRateForRemainder);
        }
    }
}
