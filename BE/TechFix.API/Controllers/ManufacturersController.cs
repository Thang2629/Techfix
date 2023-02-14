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
    public class ManufacturersController : CustomController
    {
        public ManufacturersController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<ManufacturerController>
        [HttpGet]
        public IEnumerable<Manufacturer> Get()
        {
            var result = _context.Manufacturers
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<ManufacturersController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllManufacturers(PagingParams param)
        {
            var queryable = _context.Manufacturers
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider);
            var result = PagedList<ManufacturerDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<ManufacturerController>
        [HttpPost]
        public void Post([FromBody] string name)
        {
            _context.Manufacturers.Add(new Manufacturer()
            {
                Name = name
            });
            _context.SaveChangesAsync();
        }

        // PUT api/<ManufacturerController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] string name)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                manufacturer.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<ManufacturerController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                manufacturer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
