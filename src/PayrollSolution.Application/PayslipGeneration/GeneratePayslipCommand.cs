using MediatR;
using PayrollSolution.Application.Common.Validation;
using PayrollSolution.Domain;

namespace PayrollSolution.Application.PayslipGeneration;

/// <summary>
/// In a real world scenario generating a payslip would result in the payslip being persisted
/// and leave an audi trail. Thus this class is intended to result in a "CREATE" operation.
/// Thus its a command rather than a query although in this case a user action/input triggers it.
/// </summary>
public class GeneratePayslipCommand : IRequest<ValidationFailureOr<PayslipDto>>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required decimal AnnualSalary { get; set; }
    public required decimal SuperRate { get; set; }
    public required int PayPeriodYear { get; set; }
    public required Common.Models.Month PayPeriodMonth { get; set; }
}

public class GeneratePayslipCommandHandler :
        IRequestHandler<GeneratePayslipCommand, ValidationFailureOr<PayslipDto>>
{
    private readonly IIncomeTaxRangeSpecificationRepository _incomeTaxRangeSpecRepo;

    public GeneratePayslipCommandHandler(IIncomeTaxRangeSpecificationRepository incomeTaxRangeSpecRepo)
    {
        _incomeTaxRangeSpecRepo = incomeTaxRangeSpecRepo;
    }
                
    public async Task<ValidationFailureOr<PayslipDto>> Handle(GeneratePayslipCommand request, CancellationToken cancellationToken)
    {
        var payslipId = PayslipId.Create();

        var payPeriod = PayPeriod.Create(request.PayPeriodYear, (Month)request.PayPeriodMonth);
        if(!payPeriod.IsSuccess)
        {
            return payPeriod.ToValidationFailure();
        }

        var employeeName = EmployeeName.Create(request.FirstName, request.LastName);
        if(!employeeName.IsSuccess)
        {
            return employeeName.ToValidationFailure();
        }
        
        var salarySpec = EmployeeSalarySpecification.Create(employeeName.Result, request.AnnualSalary, request.SuperRate);
        if(!salarySpec.IsSuccess)
        {
            return salarySpec.ToValidationFailure();
        }

        var incomeTaxRangeSpec = await _incomeTaxRangeSpecRepo.RetrieveActive(cancellationToken) 
            ?? throw new InvalidOperationException("No active income tax range specification exists");

        var payslip = Payslip.Create(payslipId, payPeriod.Result, salarySpec.Result, incomeTaxRangeSpec);
        if(!payslip.IsSuccess)
        {
            return payslip.ToValidationFailure();
        }

        return new PayslipDto(        
            Id: payslip.Result.PayslipId.Guid,
            PayPeriodStartDate: payslip.Result.PayPeriodStartDate,
            PayPeriodEndDate: payslip.Result.PayPeriodEndDate,
            Name: payslip.Result.EmployeeSalarySpecification.Name.DisplayName,
            GrossIncome: payslip.Result.GrossIncome,
            IncomeTax: payslip.Result.IncomeTax,
            Super: payslip.Result.Super,
            NetIncome: payslip.Result.NetIncome
        );
    }
}