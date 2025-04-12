using Domain.Contracts;
using Persistence.Repositories;
using Persistence;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Extentions
{
    public static class InfraStructureServiceExtention
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection Services , IConfiguration configuration)
        {
             Services.AddScoped<IDbInitializer, DbInitializer>();
             Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddDbContext<StoreContext>((options) =>
            {
                options.UseSqlServer( configuration.GetConnectionString("DefaultConnection"));
            });
            //return
            return Services;
        }
    }
}
