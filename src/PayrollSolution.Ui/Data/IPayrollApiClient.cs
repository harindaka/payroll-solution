using PayrollSolution.Application.PayslipGeneration;
using Refit;

namespace PayrollSolution.Ui.Data
{
    public interface IPayrollApiClient
    {
        [Post("/payslips")]
        Task<IApiResponse<PayslipDto>> GeneratePayslip(GeneratePayslipCommand request);
    }
}
