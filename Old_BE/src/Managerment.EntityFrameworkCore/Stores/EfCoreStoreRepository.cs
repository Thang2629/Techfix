using Managerment.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.Stores
{
    public class EfCoreStoreRepository : EfCoreRepository<ManagermentDbContext, Store, Guid>, IStoreRepository
    {
        public EfCoreStoreRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
        public async Task<List<Store>> GetListAsync()
        {
            var dbSet = await GetDbSetAsync();

            return await dbSet
                .OrderBy(x => x.CreationTime)
                .ToListAsync();          
        }
    }
}
