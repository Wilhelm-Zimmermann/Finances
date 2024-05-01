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
                options.UseNpgsql(connectionString, config =>
                {
                    config.MigrationsAssembly("FinanceController.Domain.Api");
                });
            });

            // REPOSITORIES
            builder.Services.AddScoped<IBillTypeRepository, BillTypeRepository>();
            builder.Services.AddScoped<IBillRepository, BillRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            // HANDLERS
            builder.Services.AddScoped<BillTypeHandler, BillTypeHandler>();
            builder.Services.AddScoped<BillHandler, BillHandler>();
        }
    }
}
