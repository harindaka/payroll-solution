namespace PayrollSolution.Application.Configuration
{
     public class ApiClientConfig
    {
        public required string BaseAddress { get; init; }
        public required ApiClientProxyConfig Proxy { get; init; }
    }

    public sealed class ApiClientProxyConfig
    {
        public required bool Enabled { get; init; } = false;
        public required bool BypassOnLocal { get; init; } = false;
        public required string Url { get; init; }
        public required bool SslValidationEnabled { get; init; } = true;
    }
}
