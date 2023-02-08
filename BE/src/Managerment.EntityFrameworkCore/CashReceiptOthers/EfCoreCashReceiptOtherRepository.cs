using Managerment.CashRecieptOthers;
using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.CashReceiptOthers
{
    public class EfCoreCashReceiptOtherRepository : EfCoreRepository<ManagermentDbContext, CashReceiptOther, Guid>, ICashReceiptOtherRepository
    {
        public EfCoreCashReceiptOtherRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
