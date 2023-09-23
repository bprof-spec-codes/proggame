using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace proggame.BackEnd.Data;

/* This is used if database provider does't define
 * IBackEndDbSchemaMigrator implementation.
 */
public class NullBackEndDbSchemaMigrator : IBackEndDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
