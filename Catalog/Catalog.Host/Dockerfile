FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Catalog.Host/Catalog.Host.csproj", "Catalog.Host/"]
RUN dotnet restore "Catalog.Host/Catalog.Host.csproj"
COPY . .
WORKDIR "/src/Catalog.Host"
RUN dotnet build "Catalog.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Host.dll"]
