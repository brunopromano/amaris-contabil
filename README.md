dotnet ef migrations add MigrationInicial --project .\AmarisContabil.Infrastructure\AmarisContabil.Infrastructure.csproj --startup-project .\AmarisContabil.WebApi\AmarisContabil.WebApi.csproj

dotnet ef database update --project  .\AmarisContabil.WebApi\AmarisContabil.WebApi.csproj

*** Comando para instalar o EF Core (global)***
TODO: dotnet tool add ef (CONFIRMAR!!!!)

https://dotnetthoughts.net/using-ef-core-in-a-separate-class-library/