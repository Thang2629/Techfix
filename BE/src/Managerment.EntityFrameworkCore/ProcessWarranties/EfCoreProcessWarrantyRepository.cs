using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.ProcessWarranties
{
    public class EfCoreProcessWarrantyRepository : EfCoreRepository<ManagermentDbContext, ProcessWarranty, Guid>, IProcessWarrantyRepository
    {
        public EfCoreProcessWarrantyRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
