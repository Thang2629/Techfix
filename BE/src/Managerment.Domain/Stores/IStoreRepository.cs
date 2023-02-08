﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Managerment.Stores
{
    public interface IStoreRepository : IRepository<Store, Guid>
    {
        Task<List<Store>> GetListAsync();
    }
}
