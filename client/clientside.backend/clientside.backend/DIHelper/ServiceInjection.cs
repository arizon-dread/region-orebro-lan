using System.Reflection;

namespace clientside.backend.DIHelper
{
    public static class ServiceInjection
    {
        private static readonly Type[] interfacesToExclude = new Type[] 
        {
            typeof(ILifetime), 
            typeof(ITransient), 
            typeof(IScoped), 
            typeof(ISingleton), 
            typeof(IDisposable)
        };
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return AddServices(services, string.Empty, null);
        }
        public static IServiceCollection AddServices(this IServiceCollection services, Assembly assembly)
        {
            return AddServices(services, string.Empty,assembly);
        }
        public static IServiceCollection AddServices(this IServiceCollection services, string prefix)
        {
            return AddServices(services, prefix, null);
        }
        // Adds services from the entry assembly that are tagged with the ILifetime interface or are decorated with LifetimeAttribute
        // If a prefix is provided, only types whose full names start with the prefix are registered
        public static IServiceCollection AddServices(this IServiceCollection services, string prefix, Assembly? assembly)
        {
            var addedTypes = new HashSet<Type>();
            services = ParseAttributes(services, prefix, assembly,ref addedTypes);
            services = ParseInterfaces(services, prefix, assembly,ref addedTypes);
            return services;
        }
        private static IServiceCollection ParseAttributes(IServiceCollection services, string prefix, Assembly a, ref HashSet<Type> added)
        {
            var assembly = a ??  Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().Where(t => t.IsClass && t.GetCustomAttribute<LifetimeAttribute>() != null);
            foreach (var type in types)
            {
                if (!string.IsNullOrEmpty(prefix) && type.FullName.StartsWith(prefix) == false) continue;
                if (!added.Add(type)) continue; // Skip if already added

                var attr = type.GetCustomAttribute<LifetimeAttribute>();
                var ifaces = type.GetInterfaces();
                var baseIface = ifaces.Where(i => !interfacesToExclude.Contains(i)).FirstOrDefault();
                if(Enum.TryParse<ServiceLifetime>($"{attr.Lifetime}", out var lifetime))
                {
                    Register(services, baseIface, type, lifetime, attr.Key ? type.Name : null);
                }
            }
            return services;
        }
        private static IServiceCollection ParseInterfaces(IServiceCollection services, string prefix, Assembly? a, ref HashSet<Type> added)
        {
            var assembly = a ?? Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().Where(t => typeof(ILifetime).IsAssignableFrom(t) && t.IsClass);
            foreach (var type in types)
            {
                if (!string.IsNullOrEmpty(prefix) && type.FullName.StartsWith(prefix) == false) continue;
                if(!added.Add(type)) continue; // Skip if already added
                var ifaces = type.GetInterfaces();
                var baseIface = ifaces.Where(i => !interfacesToExclude.Contains(i)).FirstOrDefault();
                // Incase some class implements multiple lifecycle interfaces, we give precedence to  Singleton first, then Scoped , then Transient
                if (ifaces.Any(i => i.Name == typeof(ISingleton).Name))
                {
                    Register(services, baseIface, type, ServiceLifetime.Singleton,null);
                    continue;
                }
                if (ifaces.Any(i => i.Name == typeof(IScoped).Name))
                {
                    Register(services, baseIface, type, ServiceLifetime.Scoped,null);
                    continue;
                }
                if (ifaces.Any(i => i.Name == typeof(ITransient).Name))
                {
                    Register(services, baseIface, type, ServiceLifetime.Transient,null);
                    continue;
                }
            }
            return services;
        }
        private static void Register(IServiceCollection services, Type? serviceType, Type implementationType, ServiceLifetime serviceLifetime, string key)
        {
            services.Add(new ServiceDescriptor(serviceType ?? implementationType,key, implementationType, serviceLifetime));
        }
    }
}
