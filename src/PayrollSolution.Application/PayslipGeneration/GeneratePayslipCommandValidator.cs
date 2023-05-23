using FluentValidation;
using PayrollSolution.Domain;

namespace PayrollSolution.Application.PayslipGeneration;

public class GeneratePayslipCommandValidator: AbstractValidator<GeneratePayslipCommand>
{
    public GeneratePayslipCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.AnnualSalary).GreaterThan(0m);
        RuleFor(x => x.PayPeriodMonth).NotEmpty();
        RuleFor(x => x.PayPeriodYear).NotEmpty().InclusiveBetween(PayPeriod.MinYear, DateTime.MaxValue.Year);
        RuleFor(x => x.SuperRate).InclusiveBetween(0, 1);
    }
}