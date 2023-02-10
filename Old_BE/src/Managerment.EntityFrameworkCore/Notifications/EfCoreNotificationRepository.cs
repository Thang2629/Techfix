using Managerment.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Managerment.Notifications
{
    public class EfCoreNotificationRepository : EfCoreRepository<ManagermentDbContext, Notification, Guid>, INotificationRepository
    {
        public EfCoreNotificationRepository(
           IDbContextProvider<ManagermentDbContext> dbContextProvider)
           : base(dbContextProvider)
    {
    }
}
}
