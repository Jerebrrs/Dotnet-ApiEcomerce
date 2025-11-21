using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

        foreach (var implementation in types)
        {
            var interfaceType = implementation.GetInterfaces()
                .FirstOrDefault(i => i.Name == "I" + implementation.Name);

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implementation);
            }
        }
    }
}
