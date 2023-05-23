namespace PayrollSolution.RestApi.Exceptions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEnvironmentSpecificExceptionHandler(this WebApplication app)
        {
            //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-7.0#produce-a-problemdetails-payload-for-exceptions

            app.UseExceptionHandler();
            app.UseStatusCodePages();

            if (!app.Environment.IsProduction() && !app.Environment.IsStaging())
            {
                //Make problem details include stack trace for troubleshooting
                return app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}
