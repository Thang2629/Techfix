using System;
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
using TechFix.TransportModels.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitsController : CustomController
    {
        public ProductUnitsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<ProductUnitsController>
        [HttpGet]
        public IEnumerable<ProductUnit> Get()
        {
            var result = _context.ProductUnits
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<ProductUnitsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProductUnits(PagingParams param)
        {
            var queryable = _context.ProductUnits
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<ProductUnitDto>(_mapper.ConfigurationProvider);
            var result = PagedList<ProductUnitDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<ProductUnitsController>
        [HttpPost]
        public void Post([FromBody] string name)
        {
            _context.ProductUnits.Add(new ProductUnit()
            {
                Name = name
            });
            _context.SaveChangesAsync();
        }

        // PUT api/<ProductUnitsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] string name)
        {
            var productUnit = await _context.ProductUnits.FindAsync(id);
            if (productUnit != null)
            {
                productUnit.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<ProductUnitsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var productUnit = await _context.ProductUnits.FindAsync(id);
            if (productUnit != null)
            {
                productUnit.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
