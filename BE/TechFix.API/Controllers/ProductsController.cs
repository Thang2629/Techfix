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
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using TechFix.Services;
using TechFix.Common.Constants;
using System.Security.Cryptography.Xml;
using Microsoft.Build.Framework;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomController
    {
        private readonly IMapper _mapper;
        private ProductService _productService;
        private SequenceService _sequenceService;
        public ProductsController(IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context,
            IWebHostEnvironment env,
            CommonService commonService,
            ProductService productService,
            SequenceService sequenceService) : base(mapper, appSettings, context, env, commonService)
        {
            _mapper = mapper;
            _productService = productService;
            _sequenceService = sequenceService;
        }

        // GET: api/<ProductsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProducts(PagingParams param)
        {
            try
            {
                var queryable = _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Include(p => p.ProductUnit)
                .Include(p => p.ProductCondition)
                .AsNoTracking();
                queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
                var projectTo = queryable.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
                var result = PagedList<ProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[GetAllProducts] Exception: " + ex.ToString());
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("detail/{id}")]
        public IActionResult GetProductDetail(Guid id)
        {
            try
            {
                var item = _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Include(p => p.ProductUnit)
                .Include(p => p.ProductCondition)
                .FirstOrDefault(x => x.Id == id);

                if (item != null)
                {
                    ProductDto response = new ProductDto();
                    _mapper.Map(item, response);
                    return Ok(response);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[GetProductDetail] Exception: " + ex.ToString());
                return BadRequest();
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> ExportData(PagingParams param)
        {
            try
            {
                if (param != null)
                {
                    param.PageNumber = 1;
                    param.PageSize = int.MaxValue;
                }
                var data = _productService.GetAllProductByFilter(param);
                if (data.Count > 0)
                {
                    var stream = _productService.GenerateExcel(data);
                    string time = DateTime.Now.ToString("ddMMyyyy_HHmmss");

                    return File(stream, ConstantValue.FILE_TYPE_EXCEL, $"export{ConstantValue.FILE_PRODUCT_EXCEL}" + time + ConstantValue.FILE_EXT_EXCEL);
                }
                return BadRequest();
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[Product - ExportData] Exception: " + ex.ToString());
                return BadRequest();
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> ImportData(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                var importResult = await _productService.ImportExcel(formFile, cancellationToken);
                if (importResult) return Ok(importResult);
                return BadRequest(importResult);
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[Product - ImportData] Exception: " + ex.ToString());
                return BadRequest();
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task Post([FromBody] ProductTransport transport)
        {
            try
            {
                Product product = new Product();
                _mapper.Map(transport, product);

                product.Id = Guid.NewGuid();
                product.Code = !string.IsNullOrWhiteSpace(transport.Code) ? transport.Code : await _sequenceService.GetNextProductCode();
                
                _context.Products.Add(product);
                
                if(await _context.SaveChangesAsync() == 1)
                await _productService.UpdateHistory(product, ConstantValue.ACTION_PRODUCT_CREATED);
            }
            catch (Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[Product - Create] Exception: " + ex.ToString());
            }
        }


        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] ProductTransport product)
        {
            try
            {
                var model = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Include(p => p.ProductUnit)
                .Include(p => p.ProductCondition)
                .FirstOrDefaultAsync(x => x.Id == id);

                if (model != null)
                {
                    if (string.IsNullOrWhiteSpace(product.Code)) product.Code = model.Code;
                    _mapper.Map(product, model);

                    if (await _context.SaveChangesAsync() == 1)
                    await _productService.UpdateHistory(model, ConstantValue.ACTION_PRODUCT_UPDATED);
                }
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[Product - Update] Exception: " + ex.ToString());
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.IsDeleted = true;
                    if(await _context.SaveChangesAsync() == 1)
                    await _productService.UpdateHistory(product, ConstantValue.ACTION_PRODUCT_REMOVED);
                }
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[Product - Delete] Exception: " + ex.ToString());
            }
        }

        [HttpPut]
        [Route("restore/{id}")]
        public async Task RestoreProduct(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.IsDeleted = false;
                    if (await _context.SaveChangesAsync() == 1)
                    await _productService.UpdateHistory(product, ConstantValue.ACTION_PRODUCT_RESTORED);
                }
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[Product - Restore] Exception: " + ex.ToString());
            }
        }

        [HttpPut]
        [Route("change-status/{id}")]
        public async Task ChangeProductStatus(Guid id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    product.Discontinue = !product.Discontinue;
                    if (await _context.SaveChangesAsync() == 1)
                    await _productService.UpdateHistory(product, ConstantValue.ACTION_PRODUCT_STATUSCHANGED);
                }
            }
            catch(Exception ex)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, "[ChangeProductStatus] Exception: " + ex.ToString());
            }
        }
    }
}
