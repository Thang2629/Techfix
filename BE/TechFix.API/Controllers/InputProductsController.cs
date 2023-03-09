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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputProductsController : CustomController
    {
        private SequenceService _sequenceService;
        public InputProductsController(IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context,
            IWebHostEnvironment env,
            CommonService commonService,
            SequenceService sequenceService) : base(mapper, appSettings, context, env, commonService)
        {
            _sequenceService = sequenceService;
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


        // PUT api/<InputProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] InputProductTransport transport)
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

                var currentItems = model.InputProductItems;
                List<InputProductItem> updateItems = new List<InputProductItem>();
                foreach (var item in transport.Items) 
                {
                    var input = _context.InputProductItems.Find(item.ProductId);
                    if (input != null)
                    {
                        input.ProductId = item.ProductId;
                        input.OriginalPrice = item.OriginalPrice;
                        input.Quantity = item.Quantity;
                        input.InputProductId = model.Id;

                        updateItems.Add(input);
                    }
                }

                await _context.SaveChangesAsync();

                //Filter out the section of each item
                var newItems = updateItems.Where(x => !currentItems.Contains(x)).ToList();
                var removedItems = currentItems
                    .Where(x => !updateItems.Contains(x))
                    .Where(x => !newItems.Contains(x)).ToList();

                //Handle new items
                if (newItems.Count > 0)
                {
                    await _context.InputProductItems.AddRangeAsync(newItems);
                    await _context.SaveChangesAsync();
                }

                //Handle removed items
                if(removedItems.Count > 0)
                {
                    foreach(var item in removedItems)
                    {
                        item.IsDeleted = true;
                    }
                    await _context.SaveChangesAsync();
                }
            }
        }

        // DELETE api/<InputProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var inputProduct = await _context.InputProducts.Include(x => x.InputProductItems).FirstOrDefaultAsync(x => x.Id == id);
            if (inputProduct != null)
            {
                inputProduct.IsDeleted = true;
                if(inputProduct.InputProductItems.Count > 0)
                {
                    foreach(var item in inputProduct.InputProductItems)
                    {
                        item.IsDeleted = true;
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
