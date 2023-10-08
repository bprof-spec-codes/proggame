using Volo.Abp.Modularity;

namespace proggame.BackEnd;

[DependsOn(
    typeof(BackEndApplicationModule),
    typeof(BackEndDomainTestModule)
    )]
public class BackEndApplicationTestModule : AbpModule
{

}
