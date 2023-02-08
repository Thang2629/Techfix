using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
namespace Managerment;

[DependsOn(
    typeof(ManagermentApplicationContractsModule)
)]
public class ManagermentHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        //context.Services.AddHttpClientProxies(
        //    typeof(ManagermentApplicationContractsModule).Assembly,
        //    RemoteServiceName
        //);

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ManagermentHttpApiClientModule>();
        });
    }
}
