using Managerment.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.Users
{
    public class EfCoreUserRepository : EfCoreRepository<ManagermentDbContext, User, Guid>, IUserRepository
    {
        public EfCoreUserRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }
        public async Task<List<User>> GetListAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .OrderBy(x => x.CreationTime)
                .ToListAsync();
        }
    }
}
