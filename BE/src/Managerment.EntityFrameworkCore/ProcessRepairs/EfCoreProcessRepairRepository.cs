using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.ProcessRepairs
{
    public class EfCoreProcessRepairRepository : EfCoreRepository<ManagermentDbContext, ProcessRepair, Guid>, IProcessRepairRepository
    {
        public EfCoreProcessRepairRepository(
         IDbContextProvider<ManagermentDbContext> dbContextProvider)
         : base(dbContextProvider)
        {
        }
    }
}
