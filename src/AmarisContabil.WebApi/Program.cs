using AmarisContabil.Application;
using AmarisContabil.Infrastructure;
using AmarisContabil.Infrastructure.Interfaces;
using AmarisContabil.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        assembly => assembly.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
});

builder.Services.AddScoped<ILancamentoPersistencia, LancamentoPersistencia>();
builder.Services.AddScoped<ILancamentoService, LancamentoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

var app = builder.Build();

// Executa as migrações em tempo de execução
DatabaseManagementService.MigrationInitialization(app);

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors(x => x.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin()
);

app.MapControllers();

app.Run();
