using OneOf;

namespace PayrollSolution.Application.Common.Validation;

//Marker interface to be used in generic constraints for validation failure derivatives
public interface IValidationFailure : IOneOf { }

public sealed class ValidationFailureOr<TResult>: OneOfBase<List<ValidationErrorDto>, TResult>, IValidationFailure
{
    private ValidationFailureOr(OneOf<List<ValidationErrorDto>, TResult> input) : base(input)
    {
    }
            
    public static implicit operator ValidationFailureOr<TResult>(TResult _) => new(_);
    public static implicit operator ValidationFailureOr<TResult>(List<ValidationErrorDto> _) => new(_);

    public bool IsValid => IsT1;
    public TResult Result => AsT1;
}