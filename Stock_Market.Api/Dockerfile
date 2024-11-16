FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stock_Market.Api/Stock_Market.Api.csproj", "Stock_Market.Api/"]
COPY ["BuildingBlocks/Framework.Core/Framework.Core.csproj", "BuildingBlocks/Framework.Core/"]
COPY ["BuildingBlocks/Framework.WebApi.Core/Framework.WebApi.Core.csproj", "BuildingBlocks/Framework.WebApi.Core/"]
RUN dotnet restore "Stock_Market.Api/Stock_Market.Api.csproj"
COPY . .
WORKDIR "/src/Stock_Market.Api"
RUN dotnet build "Stock_Market.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Stock_Market.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stock_Market.Api.dll"]
