using Managerment.GoodsReceipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Managerment.GoodsReceiptProducts
{
    [RemoteService(IsEnabled = false)]
    public class GoodReceiptsProductAppService : ManagermentAppService
    {
        private readonly IGoodsReceiptProductRepository _goodReceiptProductRepository;
        private readonly GoodsReceiptProductManager _goodsReceiptProductManager;

        public GoodReceiptsProductAppService(
            IGoodsReceiptProductRepository goodReceiptProductRepository,
            GoodsReceiptProductManager goodsReceiptManager)
        {
            _goodReceiptProductRepository = goodReceiptProductRepository;
            _goodsReceiptProductManager = goodsReceiptManager;
        }

        public async Task CreateAsync(GoodsReceiptProductDto goodsReceiptProductDto , Guid good_receipt_id)
        {
            var product_receipt = _goodsReceiptProductManager.CreateAsync(good_receipt_id, goodsReceiptProductDto.Product_id
                                                                     , goodsReceiptProductDto.Import_price, goodsReceiptProductDto.Total);

            await _goodReceiptProductRepository.InsertAsync(product_receipt);
        }
    }
}
