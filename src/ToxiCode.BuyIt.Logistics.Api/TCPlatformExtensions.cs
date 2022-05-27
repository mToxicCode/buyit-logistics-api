using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ToxiCode.BuyIt.Logistics.Api;

public static class TCPlatformExtensions
{
    
    public static WebApplicationBuilder ConfigurePorts(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(
            options =>
            {
                Listen(options, TCPlatformEnvironment.HttpPort, HttpProtocols.Http1);
                Listen(options, TCPlatformEnvironment.GrpcPort, HttpProtocols.Http2);
                Listen(options, TCPlatformEnvironment.DebugPort, HttpProtocols.Http1);
            });

        void Listen(KestrelServerOptions kestrelServerOptions, int? port, HttpProtocols protocols)
        {
            if (!port.HasValue)
                return;
    
            var address = TCPlatformEnvironment.IsRunningInContainer
                ? IPAddress.Any
                : IPAddress.Loopback;

            kestrelServerOptions.Listen(address, port.Value, listenOptions => { listenOptions.Protocols = protocols; });
        }

        return builder;
    }
}