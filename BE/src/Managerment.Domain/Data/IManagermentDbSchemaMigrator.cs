using System.Threading.Tasks;

namespace Managerment.Data;

public interface IManagermentDbSchemaMigrator
{
    Task MigrateAsync();
}
