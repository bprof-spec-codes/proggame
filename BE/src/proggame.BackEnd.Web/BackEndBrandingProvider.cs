using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace proggame.BackEnd.Web;

[Dependency(ReplaceServices = true)]
public class BackEndBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BackEnd";
}
