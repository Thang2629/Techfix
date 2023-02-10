using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Managerment.Data;

/* This is used if database provider does't define
 * IManagermentDbSchemaMigrator implementation.
 */
public class NullManagermentDbSchemaMigrator : IManagermentDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
