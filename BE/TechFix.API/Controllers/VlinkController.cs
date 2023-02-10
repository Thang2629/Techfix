using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services.Common;

namespace TechFix.API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
	public class VlinkController : ControllerBase
	{
		protected readonly DataContext _context;
		protected IMapper _mapper;
		protected readonly AppSettings _appSettings;
		protected IWebHostEnvironment _env;
		protected CommonService _commonService;

		public VlinkController(
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
