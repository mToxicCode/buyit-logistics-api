FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToxiCode.BuyIt.Logistics.Api/ToxiCode.BuyIt.Logistics.Api.csproj", "ToxiCode.BuyIt.Logistics.Api/"]
RUN dotnet restore "ToxiCode.BuyIt.Logistics.Api/ToxiCode.BuyIt.Logistics.Api.csproj"
COPY . .
WORKDIR "/src/ToxiCode.BuyIt.Logistics.Api"
RUN dotnet build "ToxiCode.BuyIt.Logistics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToxiCode.BuyIt.Logistics.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToxiCode.BuyIt.Logistics.Api.dll"]
