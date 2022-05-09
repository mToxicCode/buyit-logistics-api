FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG GITHUB_USERNAME
ARG GITHUB_TOKEN
ARG REPOSITORY_OWNER
WORKDIR /src
COPY . ./
RUN dotnet nuget add source --username ${GITHUB_USERNAME} --password ${GITHUB_TOKEN} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/${REPOSITORY_OWNER}/index.json"
RUN dotnet restore "src/ToxiCode.BuyIt.Logistics.Api/ToxiCode.BuyIt.Logistics.Api.csproj"
RUN dotnet build "src/ToxiCode.BuyIt.Logistics.Api/ToxiCode.BuyIt.Logistics.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ToxiCode.BuyIt.Logistics.Api/ToxiCode.BuyIt.Logistics.Api.csproj" -c Release -o /app/publish

FROM base AS final
ENV DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToxiCode.BuyIt.Logistics.Api.dll"]