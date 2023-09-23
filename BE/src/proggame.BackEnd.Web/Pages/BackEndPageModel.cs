using proggame.BackEnd.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace proggame.BackEnd.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class BackEndPageModel : AbpPageModel
{
    protected BackEndPageModel()
    {
        LocalizationResourceType = typeof(BackEndResource);
    }
}
