#region Services

using System.Net;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ToxiCode.BuyIt.Logistics.Api;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Extensions;
using ToxiCode.BuyIt.Logistics.Api.GrpcControllers;
using ToxiCode.BuyIt.Logistics.Api.Infrustructure.Extensions;

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


    kestrelServerOptions.Listen(address, port.Value, listenOptions => { listenOptions.Protocols = protocols; });
}

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddDatabaseInfrastructure(builder.Configuration);
services.AddControllers();
services.AddAuthorization();
services.AddSwaggerGen();
services.AddGrpcSwagger();
services.AddMediatR(typeof(Program));
services
    .AddHttpContextAccessor()
    .AddSingleton<HttpCancellationTokenAccessor>()
    .AddSingleton<ItemsGrpcController>()
    .AddSingleton<OrdersGrpcController>()
    .AddKafka(builder.Configuration);

#endregion

#region App

var app = builder.Build();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapGrpcService<ItemsGrpcController>();
app.MapGrpcService<OrdersGrpcController>();
app.Migrate();
app.Run();

#endregion