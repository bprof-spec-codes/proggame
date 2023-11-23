using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace proggame.Data;

public class proggameEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public proggameEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the proggameDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<proggameDbContext>()
            .Database
            .MigrateAsync();
    }
}
