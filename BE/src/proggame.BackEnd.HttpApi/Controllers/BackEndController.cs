using proggame.BackEnd.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace proggame.BackEnd.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BackEndController : AbpControllerBase
{
    protected BackEndController()
    {
        LocalizationResource = typeof(BackEndResource);
    }
}
