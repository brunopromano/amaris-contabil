FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /repo

COPY src/ ./src/
COPY tests/ ./tests/

RUN dotnet restore src/AmarisContabil.sln

RUN dotnet publish src/AmarisContabil.WebApi/AmarisContabil.WebApi.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "AmarisContabil.WebApi.dll"]
