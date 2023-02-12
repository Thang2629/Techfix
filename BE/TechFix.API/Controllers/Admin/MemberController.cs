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

namespace TechFix.API.Controllers.Admin
{
	[Route("members")]

	[ApiController]
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

    }
}
