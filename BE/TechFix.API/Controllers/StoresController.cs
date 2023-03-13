using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class StoresController : CustomController
    {
        public StoresController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<StoresController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllStores(PagingParams param)
        {
            var queryable = _context.Stores
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<StoreDto>(_mapper.ConfigurationProvider);
            var result = PagedList<StoreDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<StoresController>
        [HttpPost]
        public async Task Post([FromBody] StoreTransport transport)
        {
            _context.Stores.Add(new Store()
            {
                Name = transport.Name,
                Phone = transport.Phone,
                Address = transport.Address,
            });
            await _context.SaveChangesAsync();
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] StoreTransport transport)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                store.Name = transport.Name;
                store.Phone = transport.Phone;
                store.Address = transport.Address;

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                store.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
