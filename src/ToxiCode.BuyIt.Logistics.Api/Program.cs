#region Services

using System.Net;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ToxiCode.BuyIt.Logistics.Api;
using ToxiCode.BuyIt.Logistics.Api.BusinessLayer.Services;
using ToxiCode.BuyIt.Logistics.Api.DataLayer.Extensions;
using ToxiCode.BuyIt.Logistics.Api.GrpcControllers;
using ToxiCode.BuyIt.Logistics.Api.Infrustructure.Extensions;

var builder = WebApplication
    .CreateBuilder(args);
builder.ConfigurePorts();
var services = builder.Services;

services.AddControllers();
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
await app.RunAsync();

#endregion