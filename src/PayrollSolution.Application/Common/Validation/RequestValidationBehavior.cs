using FluentValidation;
using FluentValidation.Results;
using MediatR;
using PayrollSolution.Application.Common.Models;

namespace PayrollSolution.Application.Common.Validation;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse: IValidationFailure //Pipeline behaviour applicable for only ValidationFailureOr<TResult> types
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationErrors = new List<ValidationErrorDto>();        
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var requestValidationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var requestValidationErrors = requestValidationResults
                .Where(r => r.Errors.Any())
                .SelectMany(ToValidationErrors)
                .ToList();

            validationErrors.AddRange(requestValidationErrors);
        }

        if (validationErrors.Any())
        {
            return (dynamic) validationErrors;
        }
        
        return await next();
    }

    private static IEnumerable<ValidationErrorDto> ToValidationErrors(ValidationResult validationResult)
    {
        return validationResult.Errors.Select(e => new ValidationErrorDto()
        {
            PropertyName = e.PropertyName,
            ErrorCode = e.ErrorCode,
            ErrorMessage = e.ErrorMessage
        });
    }
}