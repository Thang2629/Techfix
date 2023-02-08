using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace Managerment;

[DependsOn(
    typeof(ManagermentDomainSharedModule),
    typeof(AbpObjectExtendingModule),
     typeof(AbpDddApplicationContractsModule)
)]
public class ManagermentApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ManagermentDtoExtensions.Configure();
    }
}
