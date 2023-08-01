using Business.Concrete.Services;
using Business.Discrete;
using Data.Entity;
using System.Runtime.CompilerServices;

namespace Presentation.Configuration
{
    public static class Extensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            AddIocImports(services);
        }

        private static void AddIocImports(IServiceCollection services)
        {
            services.AddSingleton<IService<Customer>, CustomerService>();
            services.AddSingleton<IService<Client>, ClientService>();
        }
    }
}
