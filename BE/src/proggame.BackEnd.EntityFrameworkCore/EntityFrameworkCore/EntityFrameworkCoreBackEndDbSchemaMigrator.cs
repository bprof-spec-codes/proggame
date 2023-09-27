using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using proggame.BackEnd.Data;
using Volo.Abp.DependencyInjection;

namespace proggame.BackEnd.EntityFrameworkCore;

public class EntityFrameworkCoreBackEndDbSchemaMigrator
    : IBackEndDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBackEndDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BackEndDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BackEndDbContext>()
            .Database
            .MigrateAsync();
    }
}
