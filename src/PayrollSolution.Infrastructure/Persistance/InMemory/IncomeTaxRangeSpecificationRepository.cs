using PayrollSolution.Application.PayslipGeneration;
using PayrollSolution.Domain;

namespace PayrollSolution.Infrastructure.Persistance.InMemory
{
    public class IncomeTaxRangeSpecificationRepository : IIncomeTaxRangeSpecificationRepository
    {
        //Make an active income tax range spec always available
        //In a real world scenario for example, this would not be hard coded
        //Ideally it would be included in the seed data within the persistance layer
        //or setup separately via other CRUD methods included in this repo
        private static readonly IncomeTaxRangeSpecification IncomeTaxRangeSpec;

        static IncomeTaxRangeSpecificationRepository()
        {
            var incomeTaxRangeSpecId = IncomeTaxRangeSpecificationId.Create();

            var initialSlab = IncomeTaxRange.Create(14_000m, 0.105m);
            var secondSlab = IncomeTaxRange.Create(48_000m, 0.175m);
            var thirdSlab = IncomeTaxRange.Create(70_000m, 0.3m);
            var fourthSlab = IncomeTaxRange.Create(180_000m, 0.33m);
            var lastSlabRate = 0.39m;

            var incomeTaxRangeSpec = IncomeTaxRangeSpecification.CreateFromRanges(incomeTaxRangeSpecId, new List<IncomeTaxRange>
            {
                initialSlab.Result, secondSlab.Result, thirdSlab.Result, fourthSlab.Result
            }, lastSlabRate);

            IncomeTaxRangeSpec = incomeTaxRangeSpec.Result;
        }

        public Task<IncomeTaxRangeSpecification?> RetrieveActive(CancellationToken cancellationToken)
        {
            return Task.FromResult(IncomeTaxRangeSpec)!;
        }
    }
}
