using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;
using Microsoft.EntityFrameworkCore;
using TechFix.Common.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixProductsController : CustomController
    {
        public FixProductsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<FixProductsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllFixProducts(PagingParams param)
        {
            var queryable = _context.FixProducts
                .Where(x => !x.IsDeleted)
                .Include(p => p.Bill)
                .Include(p => p.FixStaff)
                .AsNoTracking();
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var mapConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<FixProduct, FixProductDto>());
            var projectTo = queryable.ProjectTo<FixProductDto>(mapConfig);
            var result = PagedList<FixProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("get-sale")]
        public IActionResult GetAllTimeSales(PagingParams param)
        {
            var queryable = _context.FixProducts
                .Where(x => !x.IsDeleted)
                .AsNoTracking();

            param.PageNumber = 1;
            param.PageSize = int.MaxValue;

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var list = queryable.ToList();
            int TotalFixed = 0, TotalFailed = 0;
            decimal TotalMoney = 0;

            TotalFixed = list.Where(x => !x.IsDeleted && x.Process.Equals(ConstantValue.PROCESS_DONE)).Count();
            TotalFailed = list.Where(x => x.IsDeleted && x.Process.Equals(ConstantValue.PROCESS_RETURN_CUSTOMER)).Count();
            TotalMoney = list.Sum(x => x.TotalMoney);

            return Ok(new { TotalFixed, TotalFailed, TotalMoney });
        }

        [HttpPost]
        [Route("get-fixproduct-revenue")]
        public IActionResult GetAllTimeBills(PagingParams param)
        {
            var queryable = _context.GetRepairProductReportViews
                .Where(x => !x.IsDeleted)
                .AsNoTracking();

            param.PageNumber = 1;
            param.PageSize = int.MaxValue;

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var list = queryable.ToList();
            var TotalRecords = list.Select(x => x.Id).Distinct().Count();
            var TotalProducts = list.Select(x => x.FixProductId).Count();
            var TotalMoney = list.Sum(x => x.TotalMoney);

            return Ok(new { TotalRecords, TotalProducts, TotalMoney });
        }

        // GET: api/<FixProductsController>
        [HttpPost]
        [Route("get-view-by-customer")]
        public IActionResult GetAllFixProductsByCustomer(PagingParams param)
        {
            var queryable = _context.RepairProductByCustomerViews
                .Where(x => !x.IsDeleted)
                .AsNoTracking();

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);

            //group logic
            var group = queryable.ToList().GroupBy(x => x.Id)
            .Select(record => new RepairProductByCustomerViewDto()
             {
                 Id = record.Key,
                 CustomerName = record.Select(x => x.CustomerName).FirstOrDefault(),
                 FixOrderCode = record.Select(x => x.FixOrderCode).FirstOrDefault(),
                 Count = record.ToList().Count(),
                 TotalMoney = record.Sum(x => x.TotalMoney),
                 FixProducts = record.Select(x => new FixProductViewDto()
                 {
                     SearchData = x.SearchData,
                     Adapter = x.Adapter,
                     Code = x.FixProductCode,
                     Condition = x.FixProductCondition,
                     Cpu = x.Cpu,
                     ErrorDescription = x.FixProductErrorDescription,
                     EstimatedReturnDate = x.FixProductEstimatedReturnDate,
                     FinishDate = x.FixProductFinishDate,
                     FixStaffName = x.FixStaffName,
                     CustomerName = x.CustomerName,
                     Hdd = x.Hdd,
                     Id = x.FixProductId,
                     Keyboard = x.Keyboard,
                     Lcd = x.Lcd,
                     Name = x.FixProductName,
                     Other = x.Other,
                     Pin = x.Pin,
                     Process = x.FixProductProcess,
                     Psu = x.Psu,
                     Ram = x.Ram,
                     ReceiptDate = x.FixProductReceiptDate,
                     ReturnDate = x.FixProductReturnDate,
                     TotalMoney = x.TotalMoney,
                     Wifi = x.Wifi
                 }).ToList()
             }).Skip((param.PageNumber -1) * param.PageSize).Take(param.PageSize).ToList();
            return Ok(group);
        }

        [HttpPost]
        [Route("get-view-by-fixstaff")]
        public IActionResult GetAllFixProductsByFixStaff(PagingParams param)
        {
            var queryable = _context.RepairProductByFixStaffViews
                .Where(x => !x.IsDeleted)
                .AsNoTracking();

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);

            //group logic
            var group = queryable.ToList().GroupBy(x => x.Id)
            .Select(record => new RepairProductByFixStaffViewDto()
            {
                Id = record.Key,
                Name = record.Select(x => x.FixStaffName).FirstOrDefault(),
                Count = record.ToList().Count(),
                TotalMoney = record.Sum(x => x.TotalMoney),
                FixProducts = record.Select(x => new FixProductViewDto()
                {
                    SearchData = x.SearchData,
                    Adapter = x.Adapter,
                    Code = x.FixProductCode,
                    Condition = x.FixProductCondition,
                    Cpu = x.Cpu,
                    ErrorDescription = x.FixProductErrorDescription,
                    EstimatedReturnDate = x.FixProductEstimatedReturnDate,
                    FinishDate = x.FixProductFinishDate,
                    FixStaffName = x.FixStaffName,
                    CustomerName = x.CustomerName,
                    Hdd = x.Hdd,
                    Id = x.FixProductId,
                    Keyboard = x.Keyboard,
                    Lcd = x.Lcd,
                    Name = x.FixProductName,
                    Other = x.Other,
                    Pin = x.Pin,
                    Process = x.FixProductProcess,
                    Psu = x.Psu,
                    Ram = x.Ram,
                    ReceiptDate = x.FixProductReceiptDate,
                    ReturnDate = x.FixProductReturnDate,
                    TotalMoney = x.TotalMoney,
                    Wifi = x.Wifi
                }).ToList()
            })
            .Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToList();

            return Ok(group);
        }

        [HttpPost]
        [Route("get-view-report")]
        public IActionResult GetFixProductRevenueReport(PagingParams param)
        {
            var queryable = _context.GetRepairProductReportViews
                .Where(x => !x.IsDeleted)
                .AsNoTracking();

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);

            //group logic
            var group = queryable.ToList().GroupBy(x => x.Id)
                .Select(record => new GetRepairProductReportViewDto
                {
                    Id = record.Key,
                    CustomerName = record.FirstOrDefault()?.CustomerName,
                    FixStaffName = record.FirstOrDefault()?.FixStaffName,
                    Code = record.FirstOrDefault()?.Code,
                    AmountOwed = record.FirstOrDefault()?.AmountOwed,
                    AmountPaid = record.FirstOrDefault()?.AmountPaid,
                    CreatedDate = record.FirstOrDefault()?.CreatedDate,
                    StoreName = record.FirstOrDefault()?.StoreName,
                    TotalAmount = record.FirstOrDefault()?.TotalAmount,
                    TotalQuantity = record.FirstOrDefault()?.TotalQuantity,
                    IsDeleted = record.FirstOrDefault()?.IsDeleted,
                    FixProducts = record.Select(x => new FixProductReportDto
                    {
                        FixProductId = x.Id,
                        Condition = x.Condition,
                        FixProductCode = x.FixProductCode,
                        FixProductName = x.FixProductName,
                        FixProductQuantity = x.FixProductQuantity,
                        FixProductUnitName = x.FixProductUnitName,
                        FixStaffName = record.FirstOrDefault()?.FixStaffName,
                        ProductSerial = x.ProductSerial,
                        TotalMoney = x.TotalMoney,
                        WarrantyPeriod = x.WarrantyPeriod
                    }).ToList()
                })
                .Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToList();

            return Ok(group);
        }

        [HttpPost]
        [Route("detail/{id}")]
        public IActionResult GetFixProductDetail(Guid id)
        {
            var item = _context.FixProducts.FirstOrDefault(x => x.Id == id);
            if (item == null) return BadRequest();
            var response = new FixProductDto
            {
                Id = id,
                Code = item.Code,
                ErrorDescription = item.ErrorDescription,
                Condition = item.Condition,
                EstimatedReturnDate = item.EstimatedReturnDate,
                FinishDate = item.FinishDate,
                FixOrderId = item.FixOrderId,
                FixStaffId = item.FixStaffId,
                IsFixOrder = item.IsFixOrder,
                NumberOfTimes = item.NumberOfTimes,
                Adapter = item.Adapter,
                Cpu = item.Cpu,
                Hdd = item.Hdd,
                Keyboard = item.Keyboard,
                Lcd = item.Lcd,
                Name = item.Name,
                Other = item.Other,
                Pin = item.Pin,
                Process = item.Process,
                ProductSerial = item.ProductSerial,
                Psu = item.Psu,
                Ram = item.Ram,
                ReceiptDate = item.ReceiptDate,
                ReturnDate = item.ReturnDate,
                TotalMoney = item.TotalMoney,
                Type = item.Type,
                WarrantyPeriod = item.WarrantyPeriod,
                Wifi = item.Wifi,
                IsCreatedBill = item.IsCreatedBill
            };

            return Ok(response);
        }


        // PUT api/<FixProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] FixProductTransport transport)
        {
            var model = await _context.FixProducts
                .Include(p => p.Bill)
                .Include(p => p.FixStaff)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model != null)
            {
                model.Condition = transport.Condition;
                model.FinishDate = transport.FinishDate;
                model.Process = transport.Process;
                model.FixStaffId = transport.FixStaffId;
                model.TotalMoney = transport.TotalMoney;
                model.ReceiptDate = transport.ReceiptDate;
                model.ReturnDate = transport.ReturnDate;

                await _context.SaveChangesAsync();
            }
        }
    }
}
