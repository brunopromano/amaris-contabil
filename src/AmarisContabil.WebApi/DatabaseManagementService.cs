using AmarisContabil.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmarisContabil.WebApi
{
    public static class DatabaseManagementService
    {
        
        public static void MigrationInitialization(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var serviceDb = serviceScope
                                    .ServiceProvider
                                    .GetService<DataContext>();
                
                serviceDb?.Database.Migrate();
            }
        }
    }
}