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
using AuthorizeNet;
using Bogus.DataSets;
using Microsoft.AspNetCore.JsonPatch.Internal;
using StackExchange.Redis;
using static QRCoder.PayloadGenerator;
using System.Diagnostics;

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
                    .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                    .ForMember(dest => dest.CashierName, opt => opt.MapFrom(src => src.Cashier.FullName))
                    .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.FixProducts.Count))
                    .ForMember(dest => dest.FixProducts, opt => opt.MapFrom(src => src.FixProducts.Where(x => !x.IsDeleted).ToList()))
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
            var response = new FixOrderDto
            {
                Code = item.Code,
                CustomerPhoneNumber = item.Customer.PhoneNumber,
                CustomerName = item.Customer.Fullname,
                CustomerGroup = item.Customer.Team,
                CashierName = item.Cashier.FullName,
                StoreName = item.Store.Name,
                ReceivedDate = item.ReceivedDate,
            };

            if (item != null && item.FixProducts.Count > 0)
            {
                var listItem = new List<FixProductDto>();
                foreach(var product in item.FixProducts.Where(x => !x.IsDeleted)) 
                {
                    var data = new FixProductDto
                    {
                        Code = product.Code,
                        Name = product.Name,
                        ErrorDescription = product.ErrorDescription,
                        Condition = product.Condition,
                        Process = product.Process,
                        NumberOfTimes = product.NumberOfTimes,
                        Type = product.Type,
                        IsFixOrder = product.IsFixOrder,
                        EstimatedReturnDate = product.EstimatedReturnDate,
                        FinishDate = product.FinishDate,
                        Cpu = product.Cpu,
                        Ram = product.Ram,
                        Hdd = product.Hdd,
                        Adapter = product.Adapter,
                        Wifi = product.Wifi,
                        Pin = product.Pin,
                        Keyboard = product.Keyboard,
                        Psu = product.Psu,
                        Lcd = product.Lcd,
                        Other = product.Other,
                        FixStaffId = product.FixStaffId,
                        FixOrderId = product.FixOrderId,
                        ReceiptDate = product.ReceiptDate,
                        ReturnDate = product.ReturnDate,
                        TotalMoney = product.TotalMoney,
                        ProductSerial = product.ProductSerial,
                        WarrantyPeriod = product.WarrantyPeriod,
                        IsCreatedBill = product.IsCreatedBill,
                    };

                    listItem.Add(data);
                }
                
                response.FixProducts = listItem;
                response.TotalItems = listItem.Count;

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
                    Code = await _helperService.GetFixOrderCode(transport.IsFixOrder),
                    IsFixOrder = transport.IsFixOrder,
                    CustomerId = transport.CustomerId,
                    CashierId = transport.CashierId,
                    Note = transport.Note,
                    StoreId = transport.StoreId,
                    ReceivedDate = transport.ReceivedDate,
                };

                _context.FixOrders.Add(order);

                if (order.FixProducts.Count > 0)
                {
                    List<FixProduct> list = new List<FixProduct>();
                    foreach(var item in transport.FixProducts)
                    {
                        var insertData = new FixProduct
                        {
                            Code = await _helperService.GetFixProductCode(),
                            Name = item.Name,
                            ErrorDescription = item.ErrorDescription,
                            Condition = item.Condition,
                            Process = item.Process,
                            NumberOfTimes = item.NumberOfTimes,
                            Type = item.Type,
                            IsFixOrder = item.IsFixOrder,
                            EstimatedReturnDate = item.EstimatedReturnDate,
                            FinishDate = item.FinishDate,
                            Cpu = item.Cpu,
                            Ram = item.Ram,
                            Hdd = item.Hdd,
                            Adapter = item.Adapter,
                            Wifi = item.Wifi,
                            Pin = item.Pin,
                            Keyboard = item.Keyboard,
                            Psu = item.Psu,
                            Lcd = item.Lcd,
                            Other = item.Other,
                            FixStaffId = item.FixStaffId,
                            FixOrderId = item.FixOrderId,
                            ReceiptDate = item.ReceiptDate,
                            ReturnDate = item.ReturnDate,
                            TotalMoney = item.TotalMoney,
                            ProductSerial = item.ProductSerial,
                            WarrantyPeriod = item.WarrantyPeriod,
                            IsCreatedBill = item.IsCreatedBill,
                        };

                        list.Add(insertData);
                    }

                    if(list.Count > 0) _context.FixProducts.AddRange(list);
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
                model.IsFixOrder = transport.IsFixOrder;
                model.CustomerId = transport.CustomerId;
                model.CashierId = transport.CashierId;
                model.Note = transport.Note;
                model.StoreId = transport.StoreId;
                model.ReceivedDate = transport.ReceivedDate;

                //STEP 1: remove deleted data from the edit
                var removedData = await _helperService.GetRemovedFixProductListItem(model.FixProducts, transport.FixProducts);
                if (removedData != null)
                {
                    foreach(var item in removedData)
                    {
                        var deleteItemTarget = _context.FixProducts.FirstOrDefault(x => x.Code.Equals(item.Code));
                        if(deleteItemTarget != null) deleteItemTarget.IsDeleted = true;

                        await _context.SaveChangesAsync();
                    }
                }

                //STEP 2: add new data from the edit
                var addedData = await _helperService.GetAddedFixProductListItem(model.FixProducts, transport.FixProducts);
                if (addedData != null)
                {
                    foreach(var item in addedData)
                    {
                        var product = new FixProduct
                        {
                            Code = await _helperService.GetFixProductCode(),
                            Name = item.Name,
                            ErrorDescription = item.ErrorDescription,
                            Condition = item.Condition,
                            Process = item.Process,
                            NumberOfTimes = item.NumberOfTimes,
                            Type = item.Type,
                            IsFixOrder = item.IsFixOrder,
                            EstimatedReturnDate = item.EstimatedReturnDate,
                            FinishDate = item.FinishDate,
                            Cpu = item.Cpu,
                            Ram = item.Ram,
                            Hdd = item.Hdd,
                            Adapter = item.Adapter,
                            Wifi = item.Wifi,
                            Pin = item.Pin,
                            Keyboard = item.Keyboard,
                            Psu = item.Psu,
                            Lcd = item.Lcd,
                            Other = item.Other,
                            FixStaffId = item.FixStaffId,
                            FixOrderId = item.FixOrderId,
                            ReceiptDate = item.ReceiptDate,
                            ReturnDate = item.ReturnDate,
                            TotalMoney = item.TotalMoney,
                            ProductSerial = item.ProductSerial,
                            WarrantyPeriod = item.WarrantyPeriod,
                            IsCreatedBill = item.IsCreatedBill,
                        };
                        _context.FixProducts.Add(product);
                    }
                }

                //STEP 3: Update the changes
                var changedData = model.FixProducts.Where(x => transport.FixProducts.Any(y => y.Code == x.Code)).ToList();
                if (changedData != null)
                {
                    foreach(var item in changedData)
                    {
                        var productTarget = model.FixProducts.FirstOrDefault(x => x.Code == item.Code);
                        if (productTarget != null)
                        {
                            productTarget.Name = item.Name;
                            productTarget.ErrorDescription = item.ErrorDescription;
                            productTarget.Condition = item.Condition;
                            productTarget.Process = item.Process;
                            productTarget.NumberOfTimes = item.NumberOfTimes;
                            productTarget.Type = item.Type;
                            productTarget.IsFixOrder = item.IsFixOrder;
                            productTarget.EstimatedReturnDate = item.EstimatedReturnDate;
                            productTarget.FinishDate = item.FinishDate;
                            productTarget.Cpu = item.Cpu;
                            productTarget.Ram = item.Ram;
                            productTarget.Hdd = item.Hdd;
                            productTarget.Adapter = item.Adapter;
                            productTarget.Wifi = item.Wifi;
                            productTarget.Pin = item.Pin;
                            productTarget.Keyboard = item.Keyboard;
                            productTarget.Psu = item.Psu;
                            productTarget.Lcd = item.Lcd;
                            productTarget.Other = item.Other;
                            productTarget.FixStaffId = item.FixStaffId;
                            productTarget.FixOrderId = item.FixOrderId;
                            productTarget.ReceiptDate = item.ReceiptDate;
                            productTarget.ReturnDate = item.ReturnDate;
                            productTarget.TotalMoney = item.TotalMoney;
                            productTarget.ProductSerial = item.ProductSerial;
                            productTarget.WarrantyPeriod = item.WarrantyPeriod;
                            productTarget.IsCreatedBill = item.IsCreatedBill;
                        }

                        await _context.SaveChangesAsync();
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
