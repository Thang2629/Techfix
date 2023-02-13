using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bogus.DataSets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.TransportModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomController
    {
        public ProductsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<ProductsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProducts(PagingParams param)
        {
            var queryable = _context.Products
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
            var result = PagedList<ProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task Post([FromBody] ProductTransport product)
        {
            _context.Products.Add(new Product()
            {
                Name = product.Name,
                Code = product.Code,
                MinimumNorm = product.MinimumNorm,
                MaximumNorm = product.MaximumNorm,
                Quantity = product.Quantity,
                OriginalCost = product.OriginalCost,
                SellIn = product.SellIn,
                SellOut = product.SellOut,
                Description = product.Description,
                AllowNegativeSell = product.AllowNegativeSell,
                Warranty = product.Warranty,
                IsDeleted = product.IsDeleted,
                CategoryId = product.CategoryId,
                ProductUnitId = product.ProductUnitId,
                ProductConditionId = product.ProductConditionId,
                ManufacturerId = product.ManufacturerId,
                SupplierId = product.SupplierId,
                IsInventoryTracking = product.IsInventoryTracking,
                StoreId = product.StoreId,
            });
            await _context.SaveChangesAsync();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] ProductTransport product)
        {
            var model = await _context.Products.FindAsync(id);
            if (model != null)
            {
                model.Name = product.Name;
                model.Code = product.Code;
                model.MinimumNorm = product.MinimumNorm;
                model.MaximumNorm = product.MaximumNorm;
                model.Quantity = product.Quantity;
                model.OriginalCost = product.OriginalCost;
                model.SellIn = product.SellIn;
                model.SellOut = product.SellOut;
                model.Description = product.Description;
                model.AllowNegativeSell = product.AllowNegativeSell;
                model.Warranty = product.Warranty;
                model.IsDeleted = product.IsDeleted;
                model.CategoryId = product.CategoryId;
                model.ProductUnitId = product.ProductUnitId;
                model.ProductConditionId = product.ProductConditionId;
                model.ManufacturerId = product.ManufacturerId;
                model.SupplierId = product.SupplierId;
                model.IsInventoryTracking = product.IsInventoryTracking;
                model.StoreId = product.StoreId;

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
