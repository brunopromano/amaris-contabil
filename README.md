# Amaris Api

Projeto criado em .Net 6

## Executando Localmente

### Requisitos

- [.Net 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)  
- [SQL Server Express 2019](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [dotnet-ef](https://www.nuget.org/packages/dotnet-ef/) (CLI do Entity Framework Core)

### Executando a Api e Migrations

*Importante*: antes de iniciar a compilação verifique a string de conexão no arquivo src/AmarisContabil.WebApi\appsettings.json, em DefaultConnection.  

1) Abra um terminal e aponte para a pasta `src`:  
`$ cd src`  
2) Restaure os pacotes da solução com:  
`$ dotnet restore`  
3) Compile a solução:  
`$ dotnet build`  
4) Suba a Api com:
`$ dotnet run --project ./AmarisContabil.WebApi/AmarisContabil.WebApi.csproj`  
5) Verifique a documentação da Api em 'http://localhost:7056/swagger/index.html'  

Obs: As migrations já foram criadas na pasta src/AmarisContabil.Infrastructure/Migrationse sobem automaticamente. Caso alguma mudança na modelagem de dados aconteça e seja necessário criar uma nova migration manualmente:  
`$ dotnet ef migrations add NomeDaMigration --project .\AmarisContabil.Infrastructure\AmarisContabil.Infrastructure.csproj --startup-project .\AmarisContabil.WebApi\AmarisContabil.WebApi.csproj`  

Após criada é precisa subir a migration:
`$ dotnet ef database update --project .\AmarisContabil.WebApi\AmarisContabil.WebApi.csproj`

## Executando com Docker

### Requisitos

[Docker](https://docs.docker.com/desktop/install/windows-install/)

### Criando e executando o container

A solução já possui os arquivos necessários para criar o container da Api e do banco de dados (a string de conexão atual já está configurada para a execução da aplicação em container).  

Para criar e subir o container basta apenas:  

1) Abrir um terminal e apontar para a pasta src:  
`$cd src`
2) Subir os containeres com:  
`$ docker compose up -d --build`
3) Verifique a documentação da Api em 'http://localhost:7056/swagger/index.html'
