using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.EntityModels.Abstracts;
using TechFix.EntityModels.Enums;
using TechFix.Services;
using TechFix.Services.Common;

namespace TechFix.API.Controllers
{
    /// <summary>
    /// Controller handle for Manufacturer, ProductCondition, ProductUnit, Store, Supplier
    /// </summary>
    [Route("/product-associated")]
    public class ProductAssociatedController : CustomController
	{
        private readonly IProductAssociatedService _productAssociatedService;
        public ProductAssociatedController(
			IMapper mapper, 
			IOptions<AppSettings> appSettings, 
			DataContext context, 
			IWebHostEnvironment env, 
			CommonService commonService, IProductAssociatedService productAssociatedService) : base(mapper, appSettings, context, env, commonService)
        {
            _productAssociatedService = productAssociatedService;
        }

        [HttpGet]
        [Route("combobox-data/{type}")]
        public async Task<IActionResult> GetComboboxData(ProductAssociatedType type)
        {
            try
            {
                var comboboxData = await _productAssociatedService.GetComboboxDataAsync(type);
                return Ok(comboboxData);
            }
            catch (Exception ex)
            {
                return BadRequest(new {status = "FAIL", message = ex.Message});
            }
        }



    }
}