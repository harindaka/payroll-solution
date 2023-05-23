using PayrollSolution.Domain;

namespace PayrollSolution.Application.Common.Validation
{
    public static class DomainResultExtensions
    {
        public static List<ValidationErrorDto> ToValidationFailure<TResult>(this DomainValidationErrorOr<TResult> domainResult)
        {
            return new List<ValidationErrorDto>
            {
                new ValidationErrorDto {
                    ErrorCode = $"D{domainResult.AsT0.Id}",
                    ErrorMessage = domainResult.AsT0.Name
                }
            };
        }
    }
}
