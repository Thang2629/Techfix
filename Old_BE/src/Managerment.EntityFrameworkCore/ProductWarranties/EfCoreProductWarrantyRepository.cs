using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.ProductWarranties
{
    public class EfCoreProductWarrantyRepository : EfCoreRepository<ManagermentDbContext, ProductWarranty, Guid>, IProductWarrantyRepository
    {
        public EfCoreProductWarrantyRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
