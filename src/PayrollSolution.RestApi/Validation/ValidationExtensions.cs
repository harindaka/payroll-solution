using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PayrollSolution.Application.Common.Validation;

namespace PayrollSolution.RestApi.Validation;

public static class ValidationExtensions
{
    public static BadRequest<ValidationProblemDetails> ToBadRequest(this IEnumerable<ValidationErrorDto> validationErrors)
    {
        var errorsByProperty = validationErrors.GroupBy(e => e.PropertyName ?? string.Empty, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

        var problemDetails = new ValidationProblemDetails(errorsByProperty)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        return TypedResults.BadRequest(problemDetails);
    }
}