using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.Common.Constants.Packages;
using TechFix.Common.Constants.User;
using TechFix.EntityModels;
using TechFix.EntityModels.Configs;
using TechFix.Services.ScheduleServices;

namespace TechFix.Services.Common
{
	public class CommonService
	{
		private DataContext _context;
		private IMapper _mapper;
		private AppSettings _appSettings;
		private readonly ILogger<AutomationServices> _logger;
		private readonly IStringLocalizer _localizer;

        public CommonService(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, ILogger<AutomationServices> logger, IStringLocalizer localizer)
		{
			_context = context;
			_mapper = mapper;
			_appSettings = appSettings.Value;
			_logger = logger;
            _localizer = localizer;
        }
      
    }
}
