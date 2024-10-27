﻿using Asp.Versioning;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using sptb.pe.tdlt.user.command.Mapster;
using ssptb.pe.tdlt.user.api.Configuration;
using ssptb.pe.tdlt.user.api.Configuration.Security;
using ssptb.pe.tdlt.user.common.Secrets;
using ssptb.pe.tdlt.user.common.Settings;
using ssptb.pe.tdlt.user.data.HealthCheck;
using ssptb.pe.tdlt.user.secretsmanager.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ssptb.pe.tdlt.user.api;

public static class ProgramExtesions
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string[] domains = builder.Configuration.GetSection("CorsDomains").Get<string[]>();

        services.AddControllersWithViews();

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .WithOrigins(domains)
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });

        services.AddOptions();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }

    public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
        builder.Services.Configure<SecretManagerSettings>(builder.Configuration.GetSection("SecretManagerSettings"));

        return services;
    }

    public static IServiceCollection AddSecretsConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        var secretManagerService = (ISecretManagerService)serviceProvider.GetService(typeof(ISecretManagerService));

        PostgresDbSecrets secretsPostgres = secretManagerService.GetPostgresDbSecrets().GetAwaiter().GetResult();
        RedisSecrets redisSecrets = secretManagerService.GetRedisSecrets().GetAwaiter().GetResult();

        services.Configure<PostgresDbSettings>(options =>
        {
            options.Username = secretsPostgres.Username;
            options.Password = secretsPostgres.Password;
            options.Engine = secretsPostgres.Engine;
            options.Host = secretsPostgres.Host;
            options.Port = secretsPostgres.Port;
            options.Dbname = secretsPostgres.Dbname;
            options.DbInstanceIdentifier = secretsPostgres.DbInstanceIdentifier;
        });

        services.Configure<RedisKeySettings>(options =>
        {
            options.PrivateKey = redisSecrets.PrivateKey;
        });

        return services;
    }

    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig mapsterConfiguration = MapsterConfiguration.Configuration();

        services.AddSingleton(mapsterConfiguration);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    public static WebApplication ConfigurationSwagger(this WebApplication app)
    {
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }

        });

        return app;
    }

    public static IServiceCollection AddDatabaseHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<PostgresHealthCheck>("PostgreSQL");

        return services;
    }

    public static WebApplication AddSecurityHeaders(this WebApplication app)
    {
        app.UseMiddleware<SecurityHeadersMiddleware>(new SecurityHeadersBuilder().AddDefaultSecurePolicy().Build());
        return app;
    }

    public static IServiceCollection AddAntiForgeryToken(this IServiceCollection services)
    {
        services.AddSingleton<IAntiforgeryAdditionalDataProvider, CustomAntiforgeryDataProvider>();

        services.AddAntiforgery(options =>
        {
            options.HeaderName = "X-CSRF-TOKEN";
        });

        return services;
    }
}
