using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants.User;
using TechFix.EntityModels;
using TechFix.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using AutoMapper.QueryableExtensions;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.TransportModels.Dtos;
using TechFix.TransportModels;

namespace TechFix.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
	[Authorize(Roles = UserRole.AllRole)]
    public class CustomerController : ControllerBase
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		private readonly AppSettings _appSettings;
		private readonly IWebHostEnvironment _env;
		private readonly CommonService _commonService;
		private readonly SequenceService _sequenceService;

		public CustomerController(
			IMapper mapper,
			IOptions<AppSettings> appSettings,
			DataContext context,
			IWebHostEnvironment env,
			CommonService commonService, SequenceService sequenceService)
		{
			_mapper = mapper;
			_appSettings = appSettings.Value;
			_context = context;
			_env = env;
			_commonService = commonService;
            _sequenceService = sequenceService;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            var result = _context.Customers
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        //GET: api/<CustomersController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllCustomers([FromBody] PagingParams param)
        {
            var queryable = _context.Customers
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);
            var result = PagedList<CustomerDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("detail/{id}")]
        public async Task<IActionResult> GetCustomerDetail(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return BadRequest();
            return Ok(customer);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto customerDto)
        {
            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            if (string.IsNullOrWhiteSpace(customer.Code))
            {
                customer.Code = await _sequenceService.GetCustomerCode();
            }
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CustomerDto customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _mapper.Map(customerDto, customer);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        [Route("pay-debt")]
        public async Task<IActionResult> CustomerPayDebt([FromBody] CustomerPayDebtTransport transport)
        {
            var customer = await _context.Customers.FindAsync(transport.CustomerId);
            if (customer != null)
            {
                decimal totalPaid = 0;
                foreach(var item in transport.PayDebtItem)
                {
                    var bill = await _context.Bills.FindAsync(item.BillId);
                    if (bill != null && bill.CustomerId == customer.Id && !bill.IsDeleted)
                    {
                        bill.AmountOwed -= item.Amount;
                        totalPaid += item.Amount;

                        if(bill.AmountOwed < 0) bill.AmountOwed = 0;
                    }
                }

                customer.AmountOwed -= totalPaid;
                if(customer.AmountOwed < 0) customer.AmountOwed = 0;

                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
