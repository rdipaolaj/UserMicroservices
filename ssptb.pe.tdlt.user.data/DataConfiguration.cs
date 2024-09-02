﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ssptb.pe.tdlt.user.common.Secrets;
using ssptb.pe.tdlt.user.common.Settings;
using ssptb.pe.tdlt.user.data.Validations.Users;
using ssptb.pe.tdlt.user.data.Validations.Users.Service;
using ssptb.pe.tdlt.user.secretsmanager.Services;

namespace ssptb.pe.tdlt.user.data;
public static class DataConfiguration
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Configura la cadena de conexión obtenida de los secretos de AWS o directamente de la configuración
        var serviceProvider = services.BuildServiceProvider();
        var postgresSettings = serviceProvider.GetService<IOptions<PostgresDbSettings>>()?.Value;

        if (postgresSettings == null)
        {
            throw new InvalidOperationException("PostgresDbSettings not configured properly.");
        }

        var connectionString = $"Host={postgresSettings.Host};Port={postgresSettings.Port};Username={postgresSettings.Engine};Password={postgresSettings.Password};Database={postgresSettings.Dbname};";

        // Configura el DbContext con la cadena de conexión de PostgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString)
        );

        return services;
    }

    public static IServiceCollection AddDataServicesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IUserValidationService, UserValidationService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
