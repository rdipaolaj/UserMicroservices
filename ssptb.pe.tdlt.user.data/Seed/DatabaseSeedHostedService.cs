using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ssptb.pe.tdlt.user.data.Seed;
public class DatabaseSeedHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseSeedHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
            await seeder.SeedAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
