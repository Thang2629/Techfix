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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : CustomController
    {
        public SuppliersController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<SuppliersController>
        //[HttpGet]
        //public IEnumerable<Supplier> Get()
        //{
        //    var result = _context.Suppliers
        //        .Where(m => !m.IsDeleted)
        //        .ToList();
        //    return result;
        //}

        // GET: api/<SuppliersController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllSuppliers(PagingParams param)
        {
            var queryable = _context.Suppliers
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<SupplierDto>(_mapper.ConfigurationProvider);
            var result = PagedList<SupplierDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public async Task Post([FromBody] SupplierTransport supplier)
        {
            _context.Suppliers.Add(new Supplier()
            {
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address,
                InDebt = supplier.Indebt,
                Note= supplier.Note,
                //UrlImage = supplier.UrlImage
        });
            await _context.SaveChangesAsync();
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] SupplierTransport supplier)
        {
            var model = await _context.Suppliers.FindAsync(id);
            if (model != null)
            {
                model.Name = supplier.Name;
                model.Email = supplier.Email;
                model.Phone = supplier.Phone;
                model.Address = supplier.Address;
                model.InDebt = supplier.Indebt;
                model.Note = supplier.Note;
                //model.UrlImage = supplier.UrlImage;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                supplier.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
