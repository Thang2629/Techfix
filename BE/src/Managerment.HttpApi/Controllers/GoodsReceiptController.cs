using Managerment.GoodsReceipts;
using Managerment.Shares;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Managerment.Controllers
{
    [Route("api/good-receipt")]
    public class GoodsReceiptController : ManagermentController
    {
        private readonly IGoodsReceiptAppService _goodsReceiptAppService;
        public GoodsReceiptController(IGoodsReceiptAppService goodsReceiptAppService)
        {
            _goodsReceiptAppService = goodsReceiptAppService;
        }

        [HttpPost]
        public async Task CreateAsync([FromBody]CreateGoodsReceiptDto createGoodsReceiptDto)
        {
             await _goodsReceiptAppService.CreateAsync(createGoodsReceiptDto);
        }
        [HttpPut]
        public async Task UpdateAsync([FromBody] UpdateGoodsReceiptDto updateGoodsReceiptDto)
        {
            await _goodsReceiptAppService.UpdateAsync(updateGoodsReceiptDto);
        }
        [HttpGet("id")]
        public async Task<GoodsReceiptDetailDto> GoodsReceiptDetail(Guid goods_receipt_id)
        {
            return await _goodsReceiptAppService.GoodsReceiptDetail(goods_receipt_id);
        }
        [HttpGet("goods-receipts")]
        public async Task<ReponseListDataDto<GoodsReceiptDto>> GetListAsync(int page, int perPage, string filter, DateTime? fromDate, DateTime? toDate, Guid? supplier_id)
        {
            return await _goodsReceiptAppService.GetListAsync(page, perPage, filter, fromDate, toDate, supplier_id);
        }
    }
}
