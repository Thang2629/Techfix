using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.CustomCodes
{
    public class EfCoreCustomCodeRepository : EfCoreRepository<ManagermentDbContext, CustomCode, Guid>, ICustomCodeRepository
    {
        public EfCoreCustomCodeRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
    }
}
