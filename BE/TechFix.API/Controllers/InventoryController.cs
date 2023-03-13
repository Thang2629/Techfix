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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Finance.Implementations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : CustomController
    {
        private SequenceService _sequenceService;
        private IInventoryService _inventoryService;
        public InventoryController(IMapper mapper,
            IOptions<AppSettings> appSettings,
            DataContext context,
            IWebHostEnvironment env,
            CommonService commonService,
            SequenceService sequenceService,
            IInventoryService inventoryService) : base(mapper, appSettings, context, env, commonService)
        {
            _sequenceService = sequenceService;
            _inventoryService = inventoryService;
        }

        // GET: api/<InventoryController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllInventoryItem(PagingParams param)
        {
            var queryable = _context.Products
                .Where(x => !x.IsDeleted && x.IsInventoryTracking)
                .Include(p => p.Supplier)
                .AsNoTracking();
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var mapConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<Product, InventoryDto>()
                    .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
            );
            var projectTo = queryable.ProjectTo<InventoryDto>(mapConfig);
            var result = PagedList<InventoryDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("report")]
        public IActionResult Report(PagingParams param)
        {
            var queryable = _context.Products
                .Where(x => !x.IsDeleted && x.IsInventoryTracking)
                .AsNoTracking();

            param.PageNumber = 1;
            param.PageSize = int.MaxValue;

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            
            var listData = queryable.ToList();
            int TotalProduct = 0;
            decimal TotalValue = 0, TotalStock = 0;

            if (listData.Count > 0)
            {
                foreach(var item in listData)
                {
                    TotalProduct += item.Quantity;
                    TotalValue += item.OriginalPrice * item.Quantity;
                    TotalStock += item.WebPrice * item.Quantity;
                }
            }

            return Ok(new { TotalProduct, TotalValue, TotalStock });
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
                var data = _inventoryService.GetAllInventoryByFilter(param);
                if (data.Count > 0)
                {
                    var stream = _inventoryService.GenerateExcel(data);
                    string time = DateTime.Now.ToString("ddMMyyyy_HHmmss");

                    return File(stream, ConstantValue.FILE_TYPE_EXCEL, $"export{ConstantValue.FILE_INVENTORY_EXCEL}" + time + ConstantValue.FILE_EXT_EXCEL);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
