using proggame.BackEnd.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace proggame.BackEnd.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BackEndEntityFrameworkCoreModule),
    typeof(BackEndApplicationContractsModule)
    )]
public class BackEndDbMigratorModule : AbpModule
{
}
