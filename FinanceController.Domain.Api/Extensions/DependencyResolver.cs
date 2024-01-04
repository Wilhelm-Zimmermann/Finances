using FinanceController.Domain.Handlers;
using FinanceController.Domain.Infra.Contexts;
using FinanceController.Domain.Infra.Repositories;
using FinanceController.Domain.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FinanceController.Domain.Api.Extensions
{
    public static class DependencyResolver
    {
        public static void Resolve(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ApiDatabase");

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 2, 0)),
                    mySqlOptions => {
                        mySqlOptions.MigrationsAssembly("FinanceController.Domain.Api");
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    });
            });

            // REPOSITORIES
            builder.Services.AddScoped<IBillTypeRepository, BillTypeRepository>();
            builder.Services.AddScoped<IBillRepository, BillRepository>();

            // HANDLERS
            builder.Services.AddScoped<BillTypeHandler, BillTypeHandler>();
            builder.Services.AddScoped<BillHandler, BillHandler>();
        }
    }
}
