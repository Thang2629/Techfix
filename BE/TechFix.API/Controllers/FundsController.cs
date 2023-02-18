using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizeNet.Api.Contracts.V1;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants.User;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRole.Admin)]
    public class FundsController : CustomController
    {
        private IHelperService _helperService;
        public FundsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService, IHelperService helperService) : base(mapper, appSettings, context, env, commonService)
        {
            _helperService = helperService;
        }

        // GET: api/<FundsController>
        [HttpGet]
        public IEnumerable<Fund> Get()
        {
            var result = _context.Funds
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<FundsController>
        [HttpGet]
        [Route("info")]
        public async Task<CalculateTotalFundDto> GetTotalFundInfo()
        {
            var result = _context.Funds
                .Where(m => !m.IsDeleted);
            var total = await _helperService.CalculateFund(result);
            return total;
        }

        // GET: api/<FundsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllFunds(PagingParams param)
        {
            var queryable = _context.Funds
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<FundDto>(_mapper.ConfigurationProvider);
            var result = PagedList<FundDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        // POST api/<FundsController>
        [HttpPost]
        public async Task Post([FromBody] FundTransport transport)
        {
            if(transport != null)
            {
                var nextCodeSequence = await _helperService.GetFundCode(transport.IsAdd);
                _context.Funds.Add(new Fund()
                {
                    Code = nextCodeSequence,
                    Cashier = transport.Cashier,
                    Amount = transport.Amount,
                    ImageUrl = transport.ImageUrl,
                    IsAdd = transport.IsAdd,
                    Note = transport.Note,
                    PaymentDate = transport.PaymentDate,
                    PaymentMethod = transport.PaymentMethod,
                    StoreId = transport.StoreId
                });
            }
            await _context.SaveChangesAsync();
        }

        // PUT api/<FundsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] FundTransport transport)
        {
            var model = await _context.Funds.FindAsync(id);
            if (model != null)
            {
                model.Cashier = transport.Cashier;
                model.Amount = transport.Amount;
                model.ImageUrl = transport.ImageUrl;
                model.IsAdd = transport.IsAdd;
                model.Note = transport.Note;
                model.PaymentDate = transport.PaymentDate;
                model.PaymentMethod = transport.PaymentMethod;
                model.StoreId = transport.StoreId;

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<FundsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var fund = await _context.Funds.FindAsync(id);
            if (fund != null)
            {
                fund.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
