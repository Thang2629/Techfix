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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : CustomController
    {
        public PaymentMethodsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<PaymentMethodsController>
        [HttpGet]
        public IEnumerable<PaymentMethod> Get()
        {
            var result = _context.PaymentMethods
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<PaymentMethodsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllPaymentMethods(PagingParams param)
        {
            var queryable = _context.PaymentMethods
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<PaymentMethodDto>(_mapper.ConfigurationProvider);
            var result = PagedList<PaymentMethodDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<PaymentMethodsController>
        [HttpPost]
        public void Post([FromBody] string name)
        {
            _context.PaymentMethods.Add(new PaymentMethod()
            {
                Name = name
            });
            _context.SaveChangesAsync();
        }

        // PUT api/<PaymentMethodsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] string name)
        {
            var model = await _context.PaymentMethods.FindAsync(id);
            if (model != null)
            {
                model.Name = name;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<PaymentMethodsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod != null)
            {
                paymentMethod.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
