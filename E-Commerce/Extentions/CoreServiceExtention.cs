using Services.Abstractions;
using Services;

namespace E_Commerce.Extentions
{
    public static class   CoreServiceExtention
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
             Services.AddScoped<IServiceManager, ServiceManager>();
             Services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);
            return Services;
        }
    }
}
