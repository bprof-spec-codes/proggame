using System;
using System.Collections.Generic;
using System.Text;
using proggame.BackEnd.Localization;
using Volo.Abp.Application.Services;

namespace proggame.BackEnd;

/* Inherit your application services from this class.
 */
public abstract class BackEndAppService : ApplicationService
{
    protected BackEndAppService()
    {
        LocalizationResource = typeof(BackEndResource);
    }
}
