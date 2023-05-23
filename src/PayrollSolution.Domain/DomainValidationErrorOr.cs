using OneOf;

namespace PayrollSolution.Domain;

public sealed class DomainValidationErrorOr<TResult> : OneOfBase<DomainValidationError, TResult>
{
    private DomainValidationErrorOr(OneOf<DomainValidationError, TResult> input) : base(input)
    {
    }

    public static implicit operator DomainValidationErrorOr<TResult>(TResult _) => new(_);
    public static implicit operator DomainValidationErrorOr<TResult>(DomainValidationError _) => new(_);

    public bool IsSuccess => IsT1;
    public TResult Result => AsT1;
}