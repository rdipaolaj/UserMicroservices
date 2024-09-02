using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using ssptb.pe.tdlt.user.commandhandler.Users;
using ssptb.pe.tdlt.user.infraestructure.Behaviors;
using ssptb.pe.tdlt.user.redis;
using ssptb.pe.tdlt.user.secretsmanager;
using ssptb.pe.tdlt.user.commandvalidator.Users;
using ssptb.pe.tdlt.user.internalservices;

namespace ssptb.pe.tdlt.user.infraestructure.Modules;
public static class MediatorModule
{
    public static IServiceCollection AddMediatRAssemblyConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommandHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(DeleteUserCommandHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(GetUserByIdQueryHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(GetUserByUsernameQueryHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(GetUsersQueryHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(UpdateUserCommandHandler));
            configuration.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

        return services;
    }
    public static IServiceCollection AddCustomServicesConfiguration(this IServiceCollection services)
    {
        services.AddInternalServicesConfiguration();
        services.AddSecretManagerConfiguration();
        services.AddRedisServiceConfiguration();

        return services;
    }
}