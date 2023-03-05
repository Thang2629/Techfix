using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : CustomController
    {
        private readonly SequenceService _sequenceService;
        private readonly IHistoryServices _historyServices;
        public BillsController(IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context,
            IWebHostEnvironment env,
            CommonService commonService, SequenceService sequenceService, IHistoryServices historyServices) : base(mapper, appSettings, context, env, commonService)
        {
            _sequenceService = sequenceService;
            _historyServices = historyServices;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(PostBillDto request)
        {
            var bill = new Bill
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                SellerId = request.SellerId,
                Note = request.Note,
                PaymentMethodId = request.PaymentMethodId,
                Vat = request.Vat,
                TotalGoodsMoney = request.TotalGoodsMoney,
                TotalQuantity = request.TotalQuantity,
                DiscountPerItem = request.DiscountPerItem,
                Discount = request.Discount,
                TotalAmount = request.TotalAmount,
                AmountPaid = request.AmountPaid,
                AmountOwed = request.AmountOwed,
                StoreId = request.StoreId,
                Code = await _sequenceService.GetNextBillCode(),
                SaleDate = DateTime.Now
            };
            if (request.Products?.Any() == true)
            {
                var productIds = request.Products.Select(p => p.Id);
                var products = await _context.Products
                    .Include(p => p.ProductUnit)
                    .Where(p => productIds.Contains(p.Id)).ToListAsync();
                bill.BillItems = new List<BillItem>();
                foreach (var productDto in request.Products)
                {
                    var product = products.First(p => p.Id == productDto.Id);
                    if (product.Discontinue)
                    {
                        throw new Exception($"Sản phẩm {product.Code} đã ngừng kinh doanh.");
                    }

                    if (product.AllowNegativeSell == false && product.Quantity <= 0)
                    {
                        throw new Exception($"Sản phẩm {product.Code} đang hết hàng.");
                    }

                    if (product.AllowNegativeSell == false && product.Quantity <= productDto.Quantity)
                    {
                        throw new Exception($"Sản phẩm {product.Code} chỉ còn {product.Quantity} {product.ProductUnit?.Name}.");
                    }

                    product.Quantity -= productDto.Quantity;
                    bill.BillItems.Add(new BillItem
                    {
                        BillId = bill.Id,
                        Price = productDto.Price,
                        ProductId = productDto.Id,
                        Quantity = productDto.Quantity,
                        ProductSerial = productDto.Serial,
                        WarrantyPeriod = productDto.WarrantyPeriod
                    });

                    await _historyServices.WriteProductHistory(product, ProductHistoryAction.Sell, bill.Code, productDto.Quantity);
                }
            }

            if (request.FixProducts?.Any() == true)
            {
                var fixProductIds = request.FixProducts.Select(fp => fp.Id).ToList();
                var fixProducts = await _context.FixProducts
                    .Where(fp => fixProductIds.Contains(fp.Id))
                    .ToListAsync();
                foreach (var fixProduct in fixProducts)
                {
                    if (fixProduct.IsCreatedBill)
                    {
                        throw new Exception($"Sản phẩm sửa chữa {fixProduct.Name} đã được xuất hóa đơn");
                    }
                    var fixProductDto = request.FixProducts.First(f=>f.Id == fixProduct.Id);
                    fixProduct.TotalMoney = fixProductDto.Price;
                    fixProduct.ProductSerial = fixProductDto.Serial;
                    fixProduct.WarrantyPeriod = fixProductDto.WarrantyPeriod;
                    fixProduct.IsCreatedBill = true;
                    fixProduct.BillId = bill.Id;
                }
            }

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("search-products")]
        public async Task<IActionResult> SearchProduct(string str)
        {
            var result = await _context.Products.Where(p => p.Discontinue == false
                                                            && p.IsDeleted == false
                                                            && (p.Name.Contains(str) || p.Code.Contains(str)))
                .Select(p => new
                {
                    DisplayName = $"{p.Code} - {p.Name} - {p.WebPrice:N0} - Tồn: {p.Quantity}",
                    p.Id,
                    p.Code,
                    p.Name,
                    ProductCondition = p.ProductCondition.Name,
                    p.WebPrice,
                    ProductUnit = p.ProductUnit.Name
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("search-fix-products")]
        public async Task<IActionResult> SearchFixProduct(string str)
        {
            var result = await _context.FixProducts
                .Where(fp => fp.IsCreatedBill == false
                             && fp.IsDeleted == false
                             && (fp.Name.Contains(str) || fp.Code.Contains(str)))
                .Select(p => new
                {
                    DisplayName = $"{p.Code} - {p.Name} - {p.TotalMoney:N0}",
                    p.Id,
                    p.Code,
                    p.Name,
                    ProductCondition = p.Condition,
                    p.TotalMoney,
                    ProductUnit = "Cái"
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("get-all")]
        public async Task<IActionResult> GetBills(PagingParams param)
        {
            //var queryable = _context.Bills
            //    .Include(b => b.Customer)
            //    .Include(b => b.Store)
            //    .Include(b => b.Seller)
            //    .AsNoTracking();
            //queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            //queryable = queryable.OrderByDescending(b => b.SaleDate);

            //var mapConfig = new MapperConfiguration(
            //    cfg => cfg.CreateMap<Bill, BillDto>()
            //        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
            //        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Customer.FullName))
            //        .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            //        .ForMember(dest => dest.sta, opt => opt.MapFrom(src => src.Seller.FullName))
            //);
            //var projectTo = queryable.ProjectTo<BillDto>(mapConfig);
            //var result = PagedList<BillDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            //for (var i = 0; i < result.Data.Count; i++)
            //{
            //    result.Data[i].Index = i + 1;
            //}

            return Ok();
        }


    }
}
