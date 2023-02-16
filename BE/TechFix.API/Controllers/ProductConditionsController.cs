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
    public class ProductConditionsController : CustomController
    {
        public ProductConditionsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<ProductConditionsController>
        [HttpGet]
        public IEnumerable<ProductCondition> Get()
        {
            var result = _context.ProductConditions
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<ProductConditionsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProductConditions(PagingParams param)
        {
            var queryable = _context.ProductConditions
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<ProductConditionDto>(_mapper.ConfigurationProvider);
            var result = PagedList<ProductConditionDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<ProductConditionsController>
        [HttpPost]
        public async Task Post([FromBody] string name)
        {
            _context.ProductConditions.Add(new ProductCondition()
            {
                Name = name
            });
            await _context.SaveChangesAsync();
        }

        // PUT api/<ProductConditionsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] string name)
        {
            var productCondition = await _context.ProductConditions.FindAsync(id);
            if (productCondition != null)
            {
                productCondition.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<ProductConditionsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var productCondition = await _context.ProductConditions.FindAsync(id);
            if (productCondition != null)
            {
                productCondition.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
