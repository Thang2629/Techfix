using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Auth;

namespace TechFix.API.Controllers
{
	[Route("/api/auth")]
    public class AuthController : CustomController
	{
        private IAuthService _authService;

        public AuthController(
			IAuthService services, 
			IMapper mapper, 
			IOptions<AppSettings> appSettings, 
			DataContext context, 
			IWebHostEnvironment env, 
			CommonService commonService) : base(mapper, appSettings, context, env, commonService)
		{
			_authService = services;
            var fixProduct = new FixProduct();
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginTransport model)
        {
            try
            {
                var result = await _authService.GetLoginTokenAsync(model.Username, model.Password);
                result.Status = "SUCCESS";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new {status = "FAIL", message = ex.Message});
            }
        }




        [HttpPut]
		[AllowAnonymous]
		[Route("reset-password")]
		public async Task<IActionResult> ResetPassword(ResetPasswordTransport model)
		{
			try
			{
				await _authService.ResetPasswordAsync(model);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        [HttpPost]
		[Route("log-out")]
		public IActionResult LogOut()
		{
			try
			{
				_authService.LogOut();
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = ex.Message });
			}
		}

        [HttpGet]
		[AllowAnonymous]
		[Route("test")]
		public IActionResult TestApi()
        {
            return Ok("API is ok!");
        }


    }
}
