using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Managerment;

[Dependency(ReplaceServices = true)]
public class ManagermentBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Managerment";
}
