using System;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using TechFix.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixOrdersController : CustomController
    {
        private IHelperService _helperService;
        public FixOrdersController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService, IHelperService helperService) : base(mapper, appSettings, context, env, commonService)
        {
            _helperService = helperService;
        }

        // GET: api/<FixOrdersController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllFixOrders(PagingParams param)
        {
            var queryable = _context.FixOrders
                .Where(x => !x.IsDeleted)
                .Include(p => p.Customer)
                .Include(p => p.Store)
                .Include(p => p.Cashier)
                .AsNoTracking();
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var mapConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<FixOrder, FixOrderDto>()
                    .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Fullname))
                    .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer.PhoneNumber))
                    .ForMember(dest => dest.CustomerGroup, opt => opt.MapFrom(src => src.Customer.Team))
                    .ForMember(dest => dest.CustomerGroup, opt => opt.MapFrom(src => src.Customer.Team))
                    .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                    .ForMember(dest => dest.CashierName, opt => opt.MapFrom(src => src.Cashier.FullName))
                    .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.FixProducts.Count))
            );
            var projectTo = queryable.ProjectTo<FixOrderDto>(mapConfig);
            var result = PagedList<FixOrderDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("detail/{id}")]
        public IActionResult GetFixOrderDetail(Guid id)
        {
            var item = _context.FixOrders.Find(id);
            var response = new List<FixProduct>();
            if (item != null && item.FixProducts.Count > 0)
            {
                foreach(var product in item.FixProducts) 
                { 
                }

                return Ok(response);
            }
            return BadRequest();
        }

        // POST api/<FixOrdersController>
        [HttpPost]
        public async Task Post([FromBody] FixOrderTransport transport)
        {
            try
            {
                var order = new FixOrder
                {
                    //Code = auto generate base on type
                    IsFixOrder = transport.IsFixOrder,
                    CustomerId = transport.CustomerId,
                    CashierId = transport.CashierId,
                    Note = transport.Note,
                    StoreId = transport.StoreId,
                };

                if(order.FixProducts.Count > 0)
                {
                    List<FixProduct> list = new List<FixProduct>(); //change to dto
                    foreach(var item in order.FixProducts)
                    {
                        list.Add(new FixProduct
                        {
                            //logic after change dto
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


        // PUT api/<FixOrdersController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] FixOrderTransport transport)
        {
            var model = await _context.FixOrders.FindAsync(id);
            if (model != null)
            {
                if (!string.IsNullOrEmpty(transport.Code))
                {
                    var existCode = _context.FixOrders.FirstOrDefault(x => !x.IsDeleted && x.Id != id && x.Code.Equals(transport.Code.Trim()));
                    if (existCode == null)
                    {
                        model.Code = transport.Code;
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<FixOrdersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var product = await _context.FixOrders.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                if(product.FixProducts.Count > 0)
                {
                    foreach(var item in product.FixProducts)
                    {
                        item.IsDeleted = true;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
