using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.ProductRepairs
{
    public class EfCoreProductRepairRepository : EfCoreRepository<ManagermentDbContext, ProductRepair, Guid>, IProductRepairRepository
    {
        public EfCoreProductRepairRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
