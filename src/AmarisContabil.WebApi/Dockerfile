FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5261

ENV ASPNETCORE_URLS=http://+:5261

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AmarisContabil.WebApi/AmarisContabil.WebApi.csproj", "AmarisContabil.WebApi/"]
RUN dotnet restore "AmarisContabil.WebApi\AmarisContabil.WebApi.csproj"
COPY . .
WORKDIR "/src/AmarisContabil.WebApi"
RUN dotnet build "AmarisContabil.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AmarisContabil.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AmarisContabil.WebApi.dll"]
