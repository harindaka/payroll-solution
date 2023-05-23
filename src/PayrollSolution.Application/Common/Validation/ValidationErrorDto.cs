namespace PayrollSolution.Application.Common.Validation;

public class ValidationErrorDto
{
     public string? PropertyName { get; set; }
     public string? ErrorCode { get; set; }
     public required string ErrorMessage { get; set; }
}