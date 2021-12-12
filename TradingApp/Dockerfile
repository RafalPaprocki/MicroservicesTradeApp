FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TradingApp/TradingApp.csproj", "TradingApp/"]
RUN dotnet restore "TradingApp/TradingApp.csproj"
COPY . .
WORKDIR "/src/TradingApp"
RUN dotnet build "TradingApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TradingApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradingApp.dll"]
