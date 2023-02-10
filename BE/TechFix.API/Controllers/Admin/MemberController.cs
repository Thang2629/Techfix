using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.Services.Helpers.Paging;
using TechFix.TransportModels;
using Microsoft.Extensions.Localization;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants.User;
using TechFix.Common.Helper;

namespace TechFix.API.Controllers.Admin
{
	[Route("admin/member")]
	[Authorize(Roles = "ADMIN, SUPPORTER, MEMBER")]

	[ApiController]
	public class MemberController : VlinkController
	{
		private readonly IAuthService _authService;
		private readonly IStringLocalizer _localizer;

		public MemberController(
			IMapper mapper, 
			IOptions<AppSettings> appSettings, 
			DataContext context, 
			IWebHostEnvironment env, 
			CommonService commonService, 
			IAuthService authService, 
			IStringLocalizer localizer) : base(mapper, appSettings, context, env, commonService)
		{
			_authService = authService;
			_localizer = localizer;
		}
		// GET: api/Members
		[HttpGet]
        [Route("search")]
		[Authorize(Roles = "ADMIN, SUPPORTER")]
		public IActionResult SearchMember([FromQuery] PagingParams param)
        {
	        try
	        {
		        if (param.PageNumber < 1)
			        throw new Exception("PageNumberMustBeGreaterThan0");

				var searchField = param.SearchField;
				if (string.IsNullOrWhiteSpace(param.SearchField))
					searchField = nameof(EntityModels.User.SearchData);

				var page = PagedList<User>.ToPagedList(_context.Users.AsQueryable()
					.Where(QueryHelper.BuildPredicate<User>(searchField, param.Comparison, param.SearchString))
					.OrderBy(on => on.Username), param.PageNumber, param.PageSize);
		        var memberTransports = _mapper.Map<List<MemberTransport>>(page.ToList());
		        var result = new PagedList<MemberTransport>(memberTransports, page.TotalCount, page.CurrentPage, page.PageSize);

		        return Ok(
			        new
			        {
				        Result = result,
				        TotalCount = result.TotalCount,
				        PageSize = result.PageSize,
				        CurrentPage = result.CurrentPage,
				        TotalPages = result.TotalPages,
				        HasNext = result.HasNext,
				        HasPrevious = result.HasPrevious
			        });
	        }
	        catch (Exception ex)
	        {
		        return BadRequest(new {message = _localizer[ex.Message].Value});
	        }

        }

        // GET: api/Members/5
        [HttpGet("{id}")]
		[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ProfileTransport>> GetMember(Guid? id)
        {
	        try
	        {
                var user = await _context.Users.FindAsync(id);

		        if (user == null)
		        {
			        throw new Exception(_localizer["UserIsNotFound"].Value);
		        }

		        var result = _mapper.Map<ProfileTransport>(user);

                return result;
	        }
	        catch (Exception e)
	        {
		        return BadRequest(new {message = _localizer[e.Message].Value});
	        }
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
		[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PutMember(Guid? id, [FromBody] UpdateMemberTransport transport)
        {
	        try
	        {
		        if (id != transport.Id)
		        {
			        return BadRequest();
		        }

		        var user = await _context.FindAsync<User>(transport.Id);
		        if (transport.PhoneNumber != user.PhoneNumber)
		        {
			        var isExistPhone = _context.Users.Any(u => u.Id != user.Id 
			                                                   && u.PhoneNumber == transport.PhoneNumber
			                                                   && u.DialCode == transport.DialCode);
			        if (isExistPhone)
			        {
				        throw new Exception(_localizer["PhoneNumberIsAlreadyTaken"].Value);
			        }
		        }

		        if (user.Type != UserType.Business)
		        {
			        transport.BusinessDescription = null;
			        transport.BusinessLogo = null;
			        transport.BusinessLatitude = null;
					transport.BusinessLongitude = null;
			        transport.BusinessName = null;
					transport.BusinessWebsite = null;
		        }


		        _mapper.Map(transport, user);

		        var error = _authService.ValidateUser(user, false);
		        if (error != null)
			        throw new Exception(error);

		        _context.Users.Update(user);
		        await _context.SaveChangesAsync();

		        var userResult = await _context.Users.FindAsync(user.Id);
		        var result = _mapper.Map<UpdateMemberTransport>(userResult);

		        _authService.LogOut(user.Id);
				return Ok(result);
	        }
	        catch (Exception e)
	        {
		        return BadRequest(new {message = _localizer[e.Message].Value});
	        }
        }
		

        // POST: api/Members
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
		[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> PostMember([FromBody] AddMemberTransport transport)
        {
	        try
	        {
		        var model = new RegisterTransport()
		        {
			        Username = transport.Username,
			        Email = transport.Email,
			        Address = transport.Address,
			        Birthday = transport.Birthday,
			        Country = transport.Country,
			        FirstName = transport.FirstName,
			        LastName = transport.LastName,
			        Gender = transport.Gender,
			        Password = transport.Password,
			        Type = string.IsNullOrWhiteSpace(transport.Type) ? UserType.Affiliate : transport.Type,
			        ReferralId = transport.ReferralId
		        };
		        var result = await _authService.AddUser(model, UserRole.Member);

		        return Ok(result);
	        }
	        catch (Exception e)
	        {
		        return BadRequest(new {message = _localizer[e.Message].Value});
	        }

        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteMember(Guid? id)
        {
            try
            {
                var member = await _context.Users.FirstOrDefaultAsync(b => b.Id == id);
                if (member == null)
                {
                    throw new Exception(_localizer["UserIsNotFound"].Value);
                }

                member.Status = UserStatus.Deleted;
                _context.Update(member);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = _localizer[e.Message].Value });
            }
        }

       
	}
}
