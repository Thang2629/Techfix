using Managerment.CustomCodes;
using Managerment.GoodsReceiptProducts;
using Managerment.Products;
using Managerment.Shares;
using Managerment.Stores;
using Managerment.Suppliers;
using Managerment.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;

namespace Managerment.GoodsReceipts
{
    [RemoteService(IsEnabled = false)]
    public class GoodsReceiptAppService : ManagermentAppService, IGoodsReceiptAppService
    {
        private readonly IGoodsReceiptRepository _goodReceiptRepository;
        private readonly GoodsReceiptManager _goodsReceiptManager;
        private readonly CustomCodeAppService _customCodeAppService;
        private readonly GoodReceiptsProductAppService _goodReceiptsProductAppService;
        private readonly IStoreRepository _storeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IGoodsReceiptProductRepository _goodReceiptProductRepository;
        private readonly IProductRepository _productRepository;
        public GoodsReceiptAppService(
            IGoodsReceiptRepository goodReceiptRepository,
            GoodsReceiptManager goodsReceiptManager,
            CustomCodeAppService customCodeAppService,
            GoodReceiptsProductAppService goodReceiptsProductAppService,
            IStoreRepository storeRepository,
            IUserRepository userRepository,
            ISupplierRepository supplierRepository,
            IGoodsReceiptProductRepository goodReceiptProductRepository,
            IProductRepository productRepository)
        {
            _goodReceiptRepository = goodReceiptRepository;
            _goodsReceiptManager = goodsReceiptManager;
            _customCodeAppService = customCodeAppService;
            _goodReceiptsProductAppService = goodReceiptsProductAppService;
            _storeRepository = storeRepository;
            _userRepository = userRepository;
            _supplierRepository = supplierRepository;
            _goodReceiptProductRepository = goodReceiptProductRepository;
            _productRepository = productRepository;
        }

        public async Task CreateAsync(CreateGoodsReceiptDto createGoodsReceiptDto)
        {
            try
            {
                var productCode = await _customCodeAppService.GenerateCode("PN");

                var good_receipt = _goodsReceiptManager.CreateAsync(productCode, createGoodsReceiptDto.Receipt_date
                                                                    , createGoodsReceiptDto.Total_price, createGoodsReceiptDto.Discount
                                                                    , createGoodsReceiptDto.Total_money, createGoodsReceiptDto.Paid, createGoodsReceiptDto.Debt
                                                                    , createGoodsReceiptDto.Notes, createGoodsReceiptDto.Payment_method, createGoodsReceiptDto.Can_return, createGoodsReceiptDto.Id_store
                                                                    , createGoodsReceiptDto.Id_user, createGoodsReceiptDto.Id_supplier);

                await _goodReceiptRepository.InsertAsync(good_receipt, true);

                await _customCodeAppService.UpdateAsync("PN");

                foreach (var item in createGoodsReceiptDto.Product_receipt)
                {
                    GoodsReceiptProductDto goodsReceiptProductDto = new()
                    {
                        Product_id = item.Product_id,
                        Import_price = item.Import_price,
                        Total = item.Total,
                    };
                    await _goodReceiptsProductAppService.CreateAsync(goodsReceiptProductDto, good_receipt.Id);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }


        }

        public async Task UpdateAsync(UpdateGoodsReceiptDto updateGoodsReceiptDto)
        {
            var list_prodcut_receipt = await _goodReceiptProductRepository.GetListAsync(x => x.Goods_receipt_id == updateGoodsReceiptDto.Id);

            var list_product_receipt_ids = list_prodcut_receipt.Select(x=>x.Id).ToList();

            var list_product_receipt_ids_input = updateGoodsReceiptDto.Product_receipt.Select(x => x.Product_id);

            var product_delete_id = list_product_receipt_ids.Except(list_product_receipt_ids_input);

            await _goodReceiptProductRepository.DeleteManyAsync(product_delete_id);

            foreach (var product in updateGoodsReceiptDto.Product_receipt) {

                if(list_product_receipt_ids.Any(x=> x == product.Product_id))
                {
                    var goods_receipt_product = await _goodReceiptProductRepository.GetAsync(product.Product_id);

                    goods_receipt_product.Total = product.Total;
                    goods_receipt_product.Import_price = product.Import_price;

                    await _goodReceiptProductRepository.UpdateAsync(goods_receipt_product, true);
                }    
                else
                {
                    await _goodReceiptsProductAppService.CreateAsync(product, updateGoodsReceiptDto.Id);
                }    
            }

            var good_receipt = await _goodReceiptRepository.GetAsync(updateGoodsReceiptDto.Id);

            good_receipt.Receipt_date = updateGoodsReceiptDto.Receipt_date;
            good_receipt.Total_price = updateGoodsReceiptDto.Total_price;
            good_receipt.Discount = updateGoodsReceiptDto.Discount;
            good_receipt.Total_money = updateGoodsReceiptDto.Total_money;
            good_receipt.Paid = updateGoodsReceiptDto.Paid;
            good_receipt.Debt = updateGoodsReceiptDto.Debt;
            good_receipt.Notes = updateGoodsReceiptDto.Notes;
            good_receipt.Payment_method = updateGoodsReceiptDto.Payment_method;
            good_receipt.Id_store = updateGoodsReceiptDto.Id_store;
            good_receipt.Id_supplier = updateGoodsReceiptDto.Id_supplier;
            good_receipt.Id_user = updateGoodsReceiptDto.Id_user;

            await _goodReceiptRepository.UpdateAsync(good_receipt, true);
        }

        public async Task<GoodsReceiptDetailDto> GoodsReceiptDetail(Guid goods_receipt_id)
        {
            var goods_detail = await _goodReceiptRepository.GetAsync(goods_receipt_id);

            var product_good_receipt_lst =  await _goodReceiptProductRepository.GetListAsync(x=>x.Goods_receipt_id == goods_receipt_id);

            var product_id_lst = product_good_receipt_lst.Select(x=>x.Product_id).ToList();

            List<ProductDto> productDtos = new();

            decimal commodity_price = 0;

            foreach(var id in product_id_lst)
            {
                var product = await _productRepository.GetAsync(id);

                commodity_price += product.Pro_sell_in;

                var product_map = ObjectMapper.Map<Product,ProductDto>(product);

                productDtos.Add(product_map);
            }

            return new GoodsReceiptDetailDto()
            {
                Product_count = productDtos.Count,
                Commodity_price = commodity_price,
                Discount = goods_detail.Discount,
                Total_price = commodity_price - goods_detail.Discount,
                Total_debt = (commodity_price - goods_detail.Discount) - goods_detail.Total_price,
                Product_lst = productDtos
            };
        }

        public async Task<ReponseListDataDto<GoodsReceiptDto>> GetListAsync(int page, int perPage, string filter, DateTime? fromDate, DateTime? toDate, Guid? supplier_id = null)
        {
            var user_join = await _userRepository.GetQueryableAsync();
            var store_join = await _storeRepository.GetQueryableAsync();
            var supplier_join = await _supplierRepository.GetQueryableAsync();
            var good_receipt_join = await _goodReceiptRepository.GetQueryableAsync();

            var query = from good_receipt in good_receipt_join
                        join user in user_join on good_receipt.Id_user equals user.Id
                        join supplier in supplier_join on good_receipt.Id_supplier equals supplier.Id
                        join store in store_join on good_receipt.Id_store equals store.Id
                        where (filter == null || good_receipt.Receipt_code.Contains(filter)) && (supplier_id == null || supplier.Id == supplier_id)
                        select new GoodsReceiptDto
                        {
                            Id = good_receipt.Id,
                            Debt = good_receipt.Debt,
                            Receipt_code = good_receipt.Receipt_code,
                            Receipt_date = good_receipt.Receipt_date,
                            Total_price = good_receipt.Total_price,
                            Store_name = store.Store_name,
                            Supplier_name = supplier.Name,
                            User_name = user.U_name
                        };

            int pageCout = !query.Any() ? 0 : query.Count() / perPage + 1;

            var data_page = query.Skip((page - 1) * perPage).Take(perPage).ToList();

            return new ReponseListDataDto<GoodsReceiptDto>()
            {
                Count = query.Count(),
                Page = page,
                Per_page = perPage,
                Total_pages = pageCout,
                Data = data_page
            };
        }
    }
}
