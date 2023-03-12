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
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomController
    {
        public CategoriesController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            var result = _context.Categories
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<CategoriesController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllCategories(PagingParams param)
        {
            var queryable = _context.Categories
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);
            var result = PagedList<CategoryDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task Post([FromBody] CategoryTransport category)
        {
            _context.Categories.Add(new Category()
            {
                Name = category.Name,
                Path = category.Path
            });
            await _context.SaveChangesAsync();
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] CategoryTransport category)
        {
            var model = await _context.Categories.FindAsync(id);
            if (model != null)
            {
                model.Name = category.Name;
                model.Path = category.Path;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                category.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
