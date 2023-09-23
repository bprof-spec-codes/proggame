using proggame.BackEnd.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace proggame.BackEnd;

[DependsOn(
    typeof(BackEndEntityFrameworkCoreTestModule)
    )]
public class BackEndDomainTestModule : AbpModule
{

}
