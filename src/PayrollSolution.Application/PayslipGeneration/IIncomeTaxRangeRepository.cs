using PayrollSolution.Domain;

namespace PayrollSolution.Application.PayslipGeneration
{
    public interface IIncomeTaxRangeSpecificationRepository
    {
        /// <summary>
        /// Retrieves the currently active tax range spec
        /// </summary>
         Task<IncomeTaxRangeSpecification?> RetrieveActive(CancellationToken cancellationToken);
    }
}
