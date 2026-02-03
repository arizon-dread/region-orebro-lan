using System.Reflection;

namespace clientside.backend.DIHelper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationSectionAttribute : Attribute
    {
        public Type? Type { get; }
        public string Section { get; }
        public ConfigurationSectionAttribute(Type type)
        {
            Section = type.Name;
        }
        public ConfigurationSectionAttribute(string section)
        {
            Section = section;
        }

    }
    public interface IValidatable
    {
        void Validate();
    }
    public static class ConfigurationInjection
    {

        public static IServiceCollection AddConfigurations(this IServiceCollection services, ConfigurationManager configurationManager, Assembly assembly = null)
        {
            assembly = assembly ?? Assembly.GetEntryAssembly();
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<ConfigurationSectionAttribute>() != null);
            foreach (var t in types)
            {
                var attr = t.GetCustomAttribute<ConfigurationSectionAttribute>();
                var section = configurationManager.GetSection(attr.Section) ?? throw new InvalidOperationException($"Failed to load configuration section '{attr.Section}'");
                var settings = Activator.CreateInstance(t);
                section.Bind(settings);
                if (settings is IValidatable validatable)
                {
                    validatable.Validate();
                }
                services.AddSingleton(t, settings);
            }
            return services;
        }
        public static IServiceCollection AddConfiguration<T>(this IServiceCollection services, ConfigurationManager configurationManager) where T : class
        {
            return AddConfiguration<T>(services, configurationManager, typeof(T).Name);
        }
        public static IServiceCollection AddConfiguration<T>(this IServiceCollection services, ConfigurationManager configurationManager, string section) where T : class
        {
            var settings = configurationManager.GetSection(section).Get<T>() ?? throw new InvalidOperationException($"Failed to load configuration section '{section}'");
            if (settings is IValidatable validatable)
            {
                validatable.Validate();
            }
            services.AddSingleton(settings);
            return services;
        }
    }
}
