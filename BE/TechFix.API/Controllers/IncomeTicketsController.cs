using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;
using static QRCoder.PayloadGenerator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTicketsController : CustomController
    {
        private IHelperService _helperService;
        public IncomeTicketsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService, IHelperService helperService) : base(mapper, appSettings, context, env, commonService)
        {
            _helperService = helperService;
        }

        // GET: api/<IncomeTicketsController>
        [HttpGet]
        public IEnumerable<IncomeTicket> Get()
        {
            var result = _context.IncomeTickets
                .Where(m => !m.IsDeleted)
                .ToList();
            return result;
        }

        // GET: api/<IncomeTicketsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllIncomes(PagingParams param)
        {
            var queryable = _context.IncomeTickets
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<IncomeTicketDto>(_mapper.ConfigurationProvider);
            var result = PagedList<IncomeTicketDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [Route("info")]
        public async Task<IActionResult> CalculateAllIncomes()
        {
            var queryable = _context.IncomeTickets
                .Where(m => !m.IsDeleted);
            var result = await _helperService.CalculateTotalIncome(queryable);
            return Ok(result);
        }

        // POST api/<IncomeTicketsController>
        [HttpPost]
        public async Task Post([FromBody] IncomeTicketTransport transport)
        {
            _context.IncomeTickets.Add(new IncomeTicket()
            {
                Code = transport.Code,
                //ExportId = transport.ExportId,
                PhoneNumber = transport.PhoneNumber,
                SupplierId = transport.SupplierId,
                ImageUrl = transport.ImageUrl,
                StoreId = transport.StoreId,
                PaymentDate = transport.PaymentDate,
                PaymentTypeId = transport.PaymentTypeId,
                CashierId = transport.CashierId,
                Note = transport.Note,
                Debt = transport.Debt,
                Amount = transport.Amount,
                
            });
            await _context.SaveChangesAsync();
        }

        // PUT api/<IncomeTicketsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] IncomeTicket ticket)
        {
            var model = await _context.IncomeTickets.FindAsync(id);
            if (model != null)
            {
                model.Code = ticket.Code;
                //model.ExportId = ticket.ExportId;
                model.PhoneNumber = ticket.PhoneNumber;
                model.SupplierId = ticket.SupplierId;
                model.ImageUrl = ticket.ImageUrl;
                model.StoreId = ticket.StoreId;
                model.PaymentDate = ticket.PaymentDate;
                model.PaymentTypeId = ticket.PaymentTypeId;
                model.CashierId = ticket.CashierId;
                model.Note = ticket.Note;
                model.Debt = ticket.Debt;
                model.Amount = ticket.Amount;
                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<IncomeTicketsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var ticket = await _context.IncomeTickets.FindAsync(id);
            if (ticket != null)
            {
                ticket.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
