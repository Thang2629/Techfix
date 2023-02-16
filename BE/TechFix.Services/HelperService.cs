using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.Services.EmailServices;

namespace TechFix.Services
{
    public interface IHelperService
    {
        Task<string> GetFundCode(bool isAdd);
    }
    public class HelperService : IHelperService
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private CommonService _commonService;
        private readonly IDistributedCache _distributedCache;
        public readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;

        public HelperService(
            DataContext db,
            IOptions<AppSettings> appSettings,
            IMapper mapper,
            CommonService commonService,
            IDistributedCache distributedCache,
            IWebHostEnvironment env,
            IEmailService emailService)
        {
            _context = db;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _commonService = commonService;
            _emailService = emailService;
            _distributedCache = distributedCache;
            _env = env;
        }

        public async Task<string> GetFundCode(bool isAdd)
        {
            int nextValue = await _context.GetNextSequenceValue("FundCodeIncrement");
            if (isAdd) return $"TQ{nextValue}";
            return $"CQ{nextValue}";
        }
    }
}
