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

        //// GET: api/<FixProductsController>
        //[HttpPost]
        //[Route("get-all-by-customer")]
        //public IActionResult GetAllFixProductsByCustomer(PagingParams param)
        //{
        //    var queryable = _context.FixProducts
        //        .Include(p => p.Bill)
        //        .Include(p => p.FixStaff)
        //        .Include(p => p.FixOrder)
        //        .Where(x => !x.IsDeleted)
        //        .AsNoTracking();
        //    queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
        //    var mapConfig = new MapperConfiguration(
        //        cfg => cfg.CreateMap<FixProduct, FixProductDto>());
        //    var projectTo = queryable.ProjectTo<FixProductDto>(mapConfig);
        //    var result = PagedList<FixProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
        //    return Ok(result);
        //}

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
