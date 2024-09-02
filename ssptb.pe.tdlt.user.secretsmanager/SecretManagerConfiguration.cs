using Microsoft.Extensions.DependencyInjection;
using ssptb.pe.tdlt.user.secretsmanager.Services;

namespace ssptb.pe.tdlt.user.secretsmanager;
public static class SecretManagerConfiguration
{
    public static IServiceCollection AddSecretManagerConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<ISecretManagerService, SecretManagerService>();

        return services;
    }
}
