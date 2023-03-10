using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.GoodsReceiptProducts
{
    public class EfCoreGoodsReceiptProductRepository : EfCoreRepository<ManagermentDbContext, GoodsReceiptProduct, Guid>, IGoodsReceiptProductRepository
    {
        public EfCoreGoodsReceiptProductRepository(IDbContextProvider<ManagermentDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }
    }
}
