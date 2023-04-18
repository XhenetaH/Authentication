using Authentication.Business._01_Common;
using Authentication.Infrastructure.Repositories;

namespace Authentication.Server.Extensions
{
    public static class RegisterDependencyInjectionExtension
    {
        public static void RegisterBusinessLayerDependencies(this IServiceCollection services)
        {
            var serviceInterfaceType = typeof(IService);

            var types = serviceInterfaceType
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                services.AddTransient(type.Service, type.Implementation);
            }
        }

        public static void RegisterDataAccessLayerDependencies(this IServiceCollection services)
        {
            var repositoryInterfaceType = typeof(IGenericRepository<>);

            var types = repositoryInterfaceType
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name != "UnitOfWork")
                .Where(x => !x.Name.Contains("GenericRepository"))
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                services.AddTransient(type.Service, type.Implementation);
            }


        }
    }
}