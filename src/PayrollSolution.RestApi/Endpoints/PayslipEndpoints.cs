using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayrollSolution.Application.PayslipGeneration;
using PayrollSolution.RestApi.Swagger;
using PayrollSolution.RestApi.Validation;

namespace PayrollSolution.RestApi.Endpoints;

internal static class PayslipEndpoints
{
    internal static TBuilder AddPayslipEndpoints<TBuilder>(this TBuilder builder) 
        where TBuilder: IEndpointRouteBuilder
    {
        builder.MapPost("/payslips", async ([FromBody] GeneratePayslipCommand request, ISender mediator, 
            CancellationToken cancellationToken) =>
            {
                var response = await mediator.Send(request, cancellationToken);

                return response.Match<IResult>(
                    validationErrors => validationErrors.ToBadRequest(),
                    result => TypedResults.Created($"/payslips/{result.Id}", result)
                );
            })
            .Produces<PayslipDto>()
            .ProducesCommonResponses();
                
        return builder;
    }
}