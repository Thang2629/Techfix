using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.Common.AppSetting;
using TechFix.TransportModels;
using System.Linq;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.TransportModels.Dtos;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using TechFix.Common.Constants.User;

namespace TechFix.API.Controllers.Admin
{
	[Route("members")]
    [ApiController]
    [AllowAnonymous]
	public class MemberController : CustomController
	{
		private readonly IAuthService _authService;

		public MemberController(
			IMapper mapper, 
			IOptions<AppSettings> appSettings, 
			DataContext context, 
			IWebHostEnvironment env, 
			CommonService commonService, 
			IAuthService authService) : base(mapper, appSettings, context, env, commonService)
		{
			_authService = authService;
		}

        // GET: api/<MembersController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllMembers(PagingParams param)
        {
            var queryable = _context.Users
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<MemberDto>(_mapper.ConfigurationProvider);
            var result = PagedList<MemberDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transport"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMember(RegisterTransport transport)
        {
            try
            {
                await _authService.InsertUserAsync(transport);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {e.Message});
            }

        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] MemberTransport transport)
        {
            var model = await _context.Users.FindAsync(id);
            if (model != null)
            {
                model.FullName = transport.FullName;
                model.Email = transport.Email;
                model.BonusPercent = transport.BonusPercent;
                model.Role = transport.Role;
                model.Status = transport.Status;

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var member = await _context.Users.FindAsync(id);
            if (member != null)
            {
                member.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

    }
}
