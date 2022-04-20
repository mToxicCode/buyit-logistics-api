#region Services

using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ToxiCode.BuyIt.Logistics.Api;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.WebHost.ConfigureKestrel(
    options =>
    {
        Listen(options, 5000, HttpProtocols.Http1);
        Listen(options, 5002, HttpProtocols.Http2);
    }); 

static void Listen(KestrelServerOptions kestrelServerOptions, int? port, HttpProtocols protocols)
{
    if (port == null)
        return;

    var address = IPAddress.Loopback;
        // ? IPAddress.Any
        // : IPAddress.Loopback;

        
        
    kestrelServerOptions.Listen(address, port.Value, listenOptions => { listenOptions.Protocols = protocols; });
}



var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddDatabaseInfrastructure(builder.Configuration);
services.AddSwaggerGen();
services.AddGrpcSwagger();
services
    .AddHttpContextAccessor()
    .AddSingleton<HttpCancellationTokenAccessor>();

#endregion

#region App

var app = builder.Build();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Migrate();
app.Run();

#endregion