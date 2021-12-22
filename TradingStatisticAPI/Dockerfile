FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TradingStatisticAPI/TradingStatisticAPI.csproj", "TradingStatisticAPI/"]
RUN dotnet restore "TradingStatisticAPI/TradingStatisticAPI.csproj"
COPY . .
WORKDIR "/src/TradingStatisticAPI"
RUN dotnet build "TradingStatisticAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TradingStatisticAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TradingStatisticAPI.dll"]
