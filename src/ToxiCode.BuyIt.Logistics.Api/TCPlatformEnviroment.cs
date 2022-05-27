namespace ToxiCode.BuyIt.Logistics.Api;

public static class TCPlatformEnvironment
{
    public static bool IsRunningInContainer
        => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

    public static int HttpPort => TryGetEnvironmentInt("ASPNETCORE_HTTP_PORT") ?? 5000;

    public static int GrpcPort => TryGetEnvironmentInt("ASPNETCORE_GRPC_PORT") ?? 5002;

    public static int DebugPort => TryGetEnvironmentInt("ASPNETCORE_DEBUG_PORT") ?? 5004;

    public static string HostUrl => Environment.GetEnvironmentVariable("HOST_URL") ?? string.Empty;

    public static bool IsProduction => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
    
    public static bool IsStaging => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Staging";
    
    public static bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
    
    private static int? TryGetEnvironmentInt(string name)
    {
        var env = Environment.GetEnvironmentVariable(name);
        return int.TryParse(env, out var parsed)
            ? parsed
            : null;
    }
}