FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OrderConfirmationApi/OrderConfirmationApi.csproj", "OrderConfirmationApi/"]
RUN dotnet restore "OrderConfirmationApi/OrderConfirmationApi.csproj"
COPY . .
WORKDIR "/src/OrderConfirmationApi"
RUN dotnet build "OrderConfirmationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderConfirmationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderConfirmationApi.dll"]
