using PayrollSolution.Application;
using PayrollSolution.Infrastructure;
using PayrollSolution.RestApi;
using PayrollSolution.RestApi.Endpoints;
using PayrollSolution.RestApi.Exceptions;
using PayrollSolution.RestApi.Swagger;

const string apiName = "Payroll API";
const string apiRoutePrefix = "/api";

var builder = WebApplication.CreateBuilder();
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

var services = builder.Services;
services.AddApplication();
services.AddInfrastructure();
services.AddRestApi(apiName);

var webApplication = builder.Build();
var app = webApplication.UseEnvironmentSpecificExceptionHandler();

app.UseHttpLogging();

app.UseSwaggerDocumentation();
app.UseCors();

//Create an endpoint group / prefix for API endpoints
var api = webApplication.MapGroup(apiRoutePrefix);

//Add api endpoints here
api.AddPayslipEndpoints();

await webApplication.RunAsync();
