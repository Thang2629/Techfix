using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants.User;
using TechFix.EntityModels;
using TechFix.Services.Common;

namespace TechFix.API.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
	[Authorize(Roles = UserRole.AllRole)]
    public class CustomController : ControllerBase
	{
		protected readonly DataContext _context;
		protected readonly IMapper _mapper;
		protected readonly AppSettings _appSettings;
		protected readonly IWebHostEnvironment _env;
		protected readonly CommonService _commonService;

		public CustomController(
			IMapper mapper,
			IOptions<AppSettings> appSettings,
			DataContext context,
			IWebHostEnvironment env,
			CommonService commonService)
		{
			_mapper = mapper;
			_appSettings = appSettings.Value;
			_context = context;
			_env = env;
			_commonService = commonService;
		}
	}
}
