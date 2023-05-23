namespace PayrollSolution.RestApi.Swagger
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            var environment = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
            if (!environment.IsProduction())
            {
                return app.UseSwagger().UseSwaggerUI();
            }

            return app;
        }
    }
}
