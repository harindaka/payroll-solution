namespace PayrollSolution.Application.PayslipGeneration;

public record PayslipDto(
        Guid Id,
        string Name,
        DateOnly PayPeriodStartDate,
        DateOnly PayPeriodEndDate,
        decimal GrossIncome, 
        decimal IncomeTax,
        decimal Super,
        decimal NetIncome
    );
