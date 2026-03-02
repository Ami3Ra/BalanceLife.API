using BalanceLife.Domain.Contracts;
using BalanceLife.Persistence.Data.DbContexts;
using BalanceLife.Persistence.IdentityData.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace BalanceLIfe.API.Extensions
{
    public static class WebApplicationRegister
    {
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BalanceDbContext>();

            var pendingdMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingdMigrations.Any())
            {
                dbContext.Database.Migrate();
            }
            return app;
        }

        public static async Task<WebApplication> MigrateIdentityDataBaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BalanceIdentityDbContext>();

            var pendingdMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (pendingdMigrations.Any())
            {
                dbContext.Database.Migrate();
            }
            return app;
        }


        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
           await  using var scope = app.Services.CreateAsyncScope();

           var dataIntializer = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Default");

           await dataIntializer.IntializeAsync();

            return app;
        }

        public static async Task<WebApplication> SeedIdentityDataAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredKeyedService<IDataIntializer>("Identity");

            await dataIntializer.IntializeAsync();

            return app;
        }
    }
}
