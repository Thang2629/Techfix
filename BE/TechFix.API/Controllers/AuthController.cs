using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
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
    public class AuthController : VlinkController
	{
		private IAuthService _authService;
		private readonly IStringLocalizer _localizer;

        public AuthController(
			IAuthService services, 
			IMapper mapper, 
			IOptions<AppSettings> appSettings, 
			DataContext context, 
			IWebHostEnvironment env, 
			CommonService commonService,
			IStringLocalizer localizer) : base(mapper, appSettings, context, env, commonService)
		{
			_authService = services;
			_mapper = mapper;
			_env = env;
			_localizer = localizer;
        }


		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login([FromBody] LoginTransport model)
		{
			try
			{

				var result = await _authService.Authenticate(model.Username, model.Password, false, model.GoogleCode, model.SmsCode, model.EmailCode);

				switch (result.Status) {
					case "WAITING":
						return BadRequest(new {status = result.Status, message = _localizer["YourEmailIsNotConfirmedYet"].Value });
					case "NEED CODE":
						return Ok(new {status = result.Status, message = _localizer["YourVerificationCodeIsRequired"].Value });
					case "LOCK":
						return BadRequest(new {status = result.Status, message = _localizer["YourAccountIsLocked", _appSettings.CurrentUrl].Value });
	                default:
						result.Status = "SUCCESS";
						return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new {status = "FAIL", message = _localizer[ex.Message].Value});
			}

		}


		[AllowAnonymous]
		[HttpPost]
		[Route("admin/login")]
		public async Task<IActionResult> LoginAdmin([FromBody] LoginTransport model)
		{
			try
			{

				var result = await _authService.Authenticate(model.Username, model.Password, true, model.GoogleCode, model.SmsCode, model.EmailCode);
				if (result.Status == "WAITING")
				{
					return BadRequest(new { status = result.Status, message = _localizer["YourEmailIsNotConfirmedYet"].Value });
				}

				if (result.Status == "NEED CODE")
				{
					return Ok(new { status = result.Status, message = _localizer["YourVerificationCodeIsRequired"].Value });
				}

                result.Status = "SUCCESS";
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new { status = "FAIL", message = _localizer[ex.Message].Value });
			}

		}

		[HttpPost]
		[AllowAnonymous]
		[Route("forgot-password")]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordTransport model)
		{
			try
			{

				var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
				var result = await _authService.ForgotPassword(model.Username, ipAddress?.ToString());
				return Ok(new { email = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = _localizer[ex.Message].Value });
			}
		}

		[HttpPut]
		[AllowAnonymous]
		[Route("reset-password")]
		public async Task<IActionResult> ResetPassword(Guid token, ResetPasswordTransport model)
		{
			try
			{
				_authService.ResetPassword(token, model);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = _localizer[ex.Message].Value });
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
				return BadRequest(new { message = _localizer[ex.Message].Value });
			}
		}


    }
}
