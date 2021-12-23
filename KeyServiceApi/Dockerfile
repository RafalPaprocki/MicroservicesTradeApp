FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["KeyServiceApi/KeyServiceApi.csproj", "KeyServiceApi/"]
RUN dotnet restore "KeyServiceApi/KeyServiceApi.csproj"
COPY . .
WORKDIR "/src/KeyServiceApi"
RUN dotnet build "KeyServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KeyServiceApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeyServiceApi.dll"]
