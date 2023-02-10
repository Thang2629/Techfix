using Managerment.Shares;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Managerment.GoodsReceipts
{
    public interface IGoodsReceiptAppService : IApplicationService
    {
        Task CreateAsync(CreateGoodsReceiptDto createGoodsReceiptDto);
        Task UpdateAsync(UpdateGoodsReceiptDto updateGoodsReceiptDto);
        Task<GoodsReceiptDetailDto> GoodsReceiptDetail(Guid goods_receipt_id);
        Task<ReponseListDataDto<GoodsReceiptDto>> GetListAsync(int page, int perPage, string filter, DateTime? fromDate, DateTime? toDate,
           Guid? supplier_id);
    }
}
