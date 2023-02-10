using Localization.Resources.AbpUi;
using Managerment.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Managerment;

[DependsOn(
    typeof(ManagermentApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule)
    )]
public class ManagermentHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ManagermentResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
