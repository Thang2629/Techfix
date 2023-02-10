using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.Services.EmailServices;
using Guid = System.Guid;
using VlinkSequence = TechFix.Services.Common.VlinkSequence;

namespace TechFix.Services.ScheduleServices
{
    public interface IAutomationServices
    {
        
    }

    public class AutomationServices : IAutomationServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly ILogger<AutomationServices> _logger;
        private readonly CommonService _commonService;
        private readonly VlinkSequence _vlinkSequence;
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailService _emailService;

        public AutomationServices(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, ILogger<AutomationServices> logger, CommonService commonService, VlinkSequence vlinkSequence, IWebHostEnvironment environment, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _commonService = commonService;
            _vlinkSequence = vlinkSequence;
            _environment = environment;
            _emailService = emailService;
            _appSettings = appSettings.Value;

            _context.AuthenticatedUserId = Guid.Parse("FFFFFFFF-DDDD-EEEE-CCCC-AAAAAAAAAAAA");
        }
        

    }
}
