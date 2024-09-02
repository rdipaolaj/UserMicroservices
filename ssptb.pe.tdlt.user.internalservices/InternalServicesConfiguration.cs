using Microsoft.Extensions.DependencyInjection;
using ssptb.pe.tdlt.user.internalservices.Helpers;

namespace ssptb.pe.tdlt.user.internalservices;
public static class InternalServicesConfiguration
{
    public static IServiceCollection AddInternalServicesConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, Argon2PasswordHasher>();
        //services.AddTransient<IUserDataService, UserDataService>();

        return services;
    }
}
