﻿using System;
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
    public class ProductsController : CustomController
    {
        private IHelperService _helperService;
        public ProductsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService, IHelperService helperService) : base(mapper, appSettings, context, env, commonService)
        {
            _helperService = helperService;
        }

        // GET: api/<ProductsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProducts(PagingParams param)
        {
            var queryable = _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var mapConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.Name))
                    .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            );
            var projectTo = queryable.ProjectTo<ProductDto>(mapConfig);
            var result = PagedList<ProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("detail/{id}")]
        public IActionResult GetProductDetail(Guid id)
        {
            var item = _context.Products.Find(id);
            if(item != null)
            {
                var response = new ProductDto
                {
                    Id = id,
                    Name = item.Name,
                    Code = item.Code,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    OriginalCost = item.OriginalPrice,
                    SellIn = item.FakePrice,
                    SellOut = item.WebPrice,
                    Warranty = item.Warranty,
                    MinimumNorm = item.MinimumNorm,
                    MaximumNorm = item.MaximumNorm,
                    AllowNegativeSell = item.AllowNegativeSell,
                    IsInventoryTracking = item.IsInventoryTracking,
                };
                return Ok(response);
            }
            return BadRequest();
        }

        //Helpers
        private List<Product> GetAllProductByFilter(PagingParams param)
        {
            var queryable = _context.Products
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            return queryable.ToList();
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> ExportData(PagingParams param)
        {
            if(param != null)
            {
                param.PageNumber = 1;
                param.PageSize = int.MaxValue;
            }
            var data = GetAllProductByFilter(param);
            if (data.Count > 0)
            {
                var stream = _helperService.GenerateExcel(data);
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export-" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx");
            }
            return BadRequest();
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportData(IFormFile formFile, CancellationToken cancellationToken)
        {
            var importResult = await _helperService.ImportExcel(formFile, cancellationToken);
            if (importResult) return Ok(importResult);
            return BadRequest(importResult);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task Post([FromBody] ProductTransport transport)
        {
            try
            {
                _context.Products.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = transport.Name,
                    Code = transport.Code,
                    MinimumNorm = transport.MinimumNorm,
                    MaximumNorm = transport.MaximumNorm,
                    Quantity = transport.Quantity,
                    OriginalPrice = transport.OriginalPrice,
                    WebPrice = transport.WebPrice,
                    FakePrice = transport.FakePrice,
                    Description = transport.Description,
                    AllowNegativeSell = transport.AllowNegativeSell,
                    Warranty = transport.Warranty,
                    CategoryId = transport.CategoryId,
                    ProductUnitId = transport.ProductUnitId,
                    ProductConditionId = transport.ProductConditionId,
                    ManufacturerId = transport.ManufacturerId,
                    SupplierId = transport.SupplierId,
                    IsInventoryTracking = transport.IsInventoryTracking,
                    Discontinue = transport.Discontinue,
                    ImagePath = transport.ImagePath,
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

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
                model.OriginalPrice = product.OriginalPrice;
                model.WebPrice = product.WebPrice;
                model.FakePrice = product.FakePrice;
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
                model.Discontinue = product.Discontinue;
                model.ImagePath = product.ImagePath;

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

        [HttpPut]
        [Route("restore/{id}")]
        public async Task RestoreProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = false;
                await _context.SaveChangesAsync();
            }
        }

        [HttpPut]
        [Route("change-status/{id}")]
        public async Task ChangeProductStatus(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.Discontinue = !product.Discontinue;
                await _context.SaveChangesAsync();
            }
        }
    }
}