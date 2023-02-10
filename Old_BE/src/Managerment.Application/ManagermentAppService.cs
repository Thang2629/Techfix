using System;
using System.Collections.Generic;
using System.Text;
using Managerment.Localization;
using Volo.Abp.Application.Services;

namespace Managerment;

/* Inherit your application services from this class.
 */
public abstract class ManagermentAppService : ApplicationService
{
    protected ManagermentAppService()
    {
        LocalizationResource = typeof(ManagermentResource);
    }
}
