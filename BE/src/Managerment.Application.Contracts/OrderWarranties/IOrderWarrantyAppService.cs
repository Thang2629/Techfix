using Managerment.CustomerTypes;
using Managerment.ProcessWarranties;
using Managerment.Shares;
using Managerment.WarrantyProcesss;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Managerment.OrderWarranties
{
    public interface IOrderWarrantyAppService
    {
        Task<ReponseDataDto<ResultLstOrderWarrantyDto>> GetListAsync(int page, int perPage, string filter = null,
                                                                DateTime? fromDate = null, DateTime? toDate = null,
                                                                ECustomerTypeEnum? e_customer_type = null);
        Task<ReponseDataDto<ResultProductWarrantyDto>> GetListProcessWarranty(int page, int perPage, string filter = null,
                                                        Guid? user_id = null, EProductWarrantyType? eProductWarrantyType = null,
                                                        DateTime? fromDate = null, DateTime? toDate = null,
                                                        EWarrantyProcess? eWarrantyProcess = null);
        Task CreateAsync(CreateOrderWarrantyDto input);

        Task UpdateAsync(Guid id, CreateOrderWarrantyDto input);
    }
}
