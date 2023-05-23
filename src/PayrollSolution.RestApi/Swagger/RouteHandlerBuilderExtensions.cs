using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PayrollSolution.RestApi.Swagger;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder ProducesCommonResponses(this RouteHandlerBuilder builder)
    {
        builder.Produces<ValidationProblemDetails>((int) HttpStatusCode.BadRequest);

        builder.Produces<ProblemDetails>((int) HttpStatusCode.InternalServerError);

        return builder;
    }
}