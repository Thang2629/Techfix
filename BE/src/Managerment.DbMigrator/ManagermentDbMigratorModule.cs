using Managerment.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Managerment.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ManagermentEntityFrameworkCoreModule),
    typeof(ManagermentApplicationContractsModule)
    )]
public class ManagermentDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
