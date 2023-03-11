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
using Microsoft.EntityFrameworkCore;
using TechFix.Services;
using TechFix.Common.Constants;
using Microsoft.AspNetCore.Components.Forms;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputProductsController : CustomController
    {
        private SequenceService _sequenceService;
        private IInputProductService _inputProductService;
        public InputProductsController(IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context,
            IWebHostEnvironment env,
            CommonService commonService,
            SequenceService sequenceService,
            IInputProductService inputProductService) : base(mapper, appSettings, context, env, commonService)
        {
            _sequenceService = sequenceService;
            _inputProductService = inputProductService;
        }

        // GET: api/<InputProductsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllInputProducts(PagingParams param)
        {
            var queryable = _context.InputProducts
                .Where(x => !x.IsDeleted)
                .Include(p => p.InputProductItems)
                .Include(p => p.Store)
                .Include(p => p.Supplier)
                .Include(p => p.User)
                .AsNoTracking();
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var mapConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<InputProduct, InputProductDto>()
                    .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                    .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                    .ForMember(dest => dest.InputUserName, opt => opt.MapFrom(src => src.User.FullName))
                    .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.InputProductItems.Where(x => !x.IsDeleted).Select(x => new InputProductItemDto
                    {
                        ProductId = x.ProductId,
                        ProductCode = x.Product.Code,
                        ProductName = x.Product.Name,
                        ImageUrl = x.Product.ImagePath,
                        OriginalPrice = x.Product.OriginalPrice,
                        ProductCondition = x.Product.ProductCondition.Name,
                        ProductUnit = x.Product.ProductUnit.Name,
                        Quantity = x.Quantity,
                        Warranty = x.Product.Warranty
                    })))
            );
            var projectTo = queryable.ProjectTo<InputProductDto>(mapConfig);
            var result = PagedList<InputProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("detail/{id}")]
        public async Task<IActionResult> GetInputProductDetail(Guid id)
        {
            try
            {
                var item = await _context.InputProducts
                .Include(x => x.InputProductItems)
                .Include(x => x.Store)
                .Include(x => x.Supplier)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

                if (item == null) return BadRequest();

                var result = new InputProductDto
                {
                    Id = id,
                    AmountOwed = item.AmountOwed,
                    AmountPaid = item.AmountPaid,
                    Discount = item.Discount,
                    InputDate = item.InputDate,
                    InputUserName = item.UserId != null ? item.User.FullName : string.Empty,
                    Note = item.Note,
                    StoreName = item.StoreId != null ? item.Store.Name : string.Empty,
                    SupplierName = item.SupplierId != null ? item.Supplier.Name : string.Empty,
                    TotalAmount = item.TotalAmount,
                    TotalGoodsMoney = item.TotalGoodsMoney,
                };

                List<InputProductItemDto> items = new List<InputProductItemDto>();
                var availableItems = item.InputProductItems.Where(x => !x.IsDeleted).ToList();
                foreach (var product in availableItems)
                {
                    var inputProductInclude = await _context.InputProductItems
                        .Include(x => x.Product)
                        .Where(x => x.Id == product.Id)
                        .FirstOrDefaultAsync();

                    var inputItem = new InputProductItemDto();
                    inputItem.ProductId = inputProductInclude.ProductId;
                    inputItem.Quantity = inputProductInclude.Quantity;

                    var inputProductIncludeWithProductDetail = await _context.Products
                        .Include(p => p.ProductCondition)
                        .Include(p => p.ProductUnit)
                        .Where(x => x.Id == inputProductInclude.ProductId)
                        .FirstOrDefaultAsync();

                    inputItem.ImageUrl = inputProductIncludeWithProductDetail.ImagePath;
                    inputItem.OriginalPrice = inputProductIncludeWithProductDetail.OriginalPrice;
                    inputItem.ProductCode = inputProductIncludeWithProductDetail.Code;
                    inputItem.ProductCondition = inputProductIncludeWithProductDetail.ProductCondition?.Name;
                    inputItem.ProductName = inputProductIncludeWithProductDetail.Name;
                    inputItem.ProductUnit = inputProductIncludeWithProductDetail.ProductUnit?.Name;
                    inputItem.Warranty = inputProductIncludeWithProductDetail.Warranty;

                    items.Add(inputItem);
                }

                result.Items = items;

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // POST api/<InputProductsController>
        [HttpPost]
        public async Task Post([FromBody] InputProductTransport transport)
        {
            try
            {
                var inputProduct = new InputProduct
                {
                    SupplierId = transport.SupplierId,
                    PaymentMethodId = transport.PaymentMethodId,
                    UserId = transport.UserId,
                    StoreId = transport.StoreId,

                    Code = await _sequenceService.GetNextInputProductCode(),
                    InputDate = transport.InputDate ?? DateTime.Now,
                    Note = transport.Note,
                    TotalGoodsMoney = transport.TotalGoodsMoney,
                    Discount = transport.Discount,
                    TotalAmount = transport.TotalAmount,
                    AmountPaid = transport.AmountPaid,
                    AmountOwed = transport.AmountOwed,
                };

                //add the InputProduct in DB first to get it Id for the items
                await _context.InputProducts.AddAsync(inputProduct);
                await _context.SaveChangesAsync();

                //Add the items
                List<InputProductItem> inputProductItems = new List<InputProductItem>();
                if(transport.Items != null && transport.Items.Count > 0)
                {
                    foreach(var item in transport.Items)
                    {
                        inputProductItems.Add(new InputProductItem
                        {
                            InputProductId = inputProduct.Id,
                            ProductId = item.ProductId,
                            OriginalPrice = item.OriginalPrice,
                            Quantity = item.Quantity,
                        });
                    }
                }

                if(inputProductItems.Count > 0)
                {
                    await _context.InputProductItems.AddRangeAsync(inputProductItems);
                    await _context.SaveChangesAsync();
                }

                //create copies of changed products to ProductHistories
                foreach(var item in inputProductItems)
                {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    if(product != null && product.OriginalPrice != item.OriginalPrice)
                    {
                        //check if the price changes then create the copy
                    }

                    //then update the current price to product
                    product.OriginalPrice = item.OriginalPrice;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }

        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> Export(PagingParams param)
        {
            try
            {
                if (param != null)
                {
                    param.PageNumber = 1;
                    param.PageSize = int.MaxValue;
                }
                var data = _inputProductService.GetAllInputProductByFilter(param);
                if (data.Count > 0)
                {
                    var stream = _inputProductService.GenerateExcel(data);
                    string time = DateTime.Now.ToString("ddMMyyyy_HHmmss");

                    return File(stream, ConstantValue.FILE_TYPE_EXCEL, $"export{ConstantValue.FILE_INPUTPRODUCT_EXCEL}" + time + ConstantValue.FILE_EXT_EXCEL);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }


        // PUT api/<InputProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] InputProductTransport transport)
        {
            try
            {
                var model = await _context.InputProducts.Include(x => x.InputProductItems).FirstOrDefaultAsync(x => x.Id == id);
                if (model != null)
                {
                    model.SupplierId = transport.SupplierId;
                    model.PaymentMethodId = transport.PaymentMethodId;
                    model.UserId = transport.UserId;
                    model.StoreId = transport.StoreId;
                    model.InputDate = transport.InputDate ?? DateTime.Now;
                    model.Note = transport.Note;
                    model.TotalGoodsMoney = transport.TotalGoodsMoney;
                    model.Discount = transport.Discount;
                    model.TotalAmount = transport.TotalAmount;
                    model.AmountPaid = transport.AmountPaid;
                    model.AmountOwed = transport.AmountOwed;

                    await _context.SaveChangesAsync();

                    //get the transport items
                    var inputItems = transport.Items;
                    foreach (var item in inputItems)
                    {
                        var exist = model.InputProductItems
                            .Where(x => x.ProductId == item.ProductId && !x.IsDeleted).FirstOrDefault();

                        if (exist != null)
                        {
                            //update the existing
                            exist.ProductId = item.ProductId;
                            exist.OriginalPrice = item.OriginalPrice;
                            exist.Quantity = item.Quantity;

                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            //add new the data
                            InputProductItem newItem = new InputProductItem
                            {
                                ProductId = item.ProductId,
                                OriginalPrice = item.OriginalPrice,
                                Quantity = item.Quantity,
                                InputProductId = model.Id,
                            };

                            await _context.InputProductItems.AddAsync(newItem);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var deleteItems = model.InputProductItems
                        .Where(x => !x.IsDeleted && !inputItems.Select(y => y.ProductId)
                        .ToList()
                        .Contains(x.ProductId)).ToList();

                    if (deleteItems.Any())
                    {
                        foreach (var item in deleteItems) { item.IsDeleted = true; }

                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        // DELETE api/<InputProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            try
            {
                var inputProduct = await _context.InputProducts.Include(x => x.InputProductItems).FirstOrDefaultAsync(x => x.Id == id);
                if (inputProduct != null)
                {
                    inputProduct.IsDeleted = true;
                    if (inputProduct.InputProductItems.Count > 0)
                    {
                        foreach (var item in inputProduct.InputProductItems)
                        {
                            item.IsDeleted = true;
                        }
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
