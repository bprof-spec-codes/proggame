using System.Threading.Tasks;

namespace proggame.BackEnd.Data;

public interface IBackEndDbSchemaMigrator
{
    Task MigrateAsync();
}
