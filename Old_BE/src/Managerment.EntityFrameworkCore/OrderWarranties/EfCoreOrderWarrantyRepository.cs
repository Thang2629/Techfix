using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.OrderWarranties
{
    public class EfCoreOrderWarrantyRepository : EfCoreRepository<ManagermentDbContext, OrderWarranty, Guid>, IOrderWarrantyRepository
    {
        public EfCoreOrderWarrantyRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
