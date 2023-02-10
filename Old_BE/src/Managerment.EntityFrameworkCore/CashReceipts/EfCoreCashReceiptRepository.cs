using Managerment.CashReceiptOthers;
using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.CashReceipts
{
    public class EfCoreCashReceiptRepository : EfCoreRepository<ManagermentDbContext, CashReceipt, Guid>, ICashReceiptRepository
    {
        public EfCoreCashReceiptRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
