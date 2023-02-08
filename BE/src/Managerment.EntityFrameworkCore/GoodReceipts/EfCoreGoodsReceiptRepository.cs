using Managerment.CustomerTypes;
using Managerment.EntityFrameworkCore;
using Managerment.GoodsReceipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.GoodReceipts
{
    public class EfCoreGoodsReceiptRepository : EfCoreRepository<ManagermentDbContext, GoodsReceipt, Guid>, IGoodsReceiptRepository
    {
        public EfCoreGoodsReceiptRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
