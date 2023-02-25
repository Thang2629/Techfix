﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TechFix.Common.AppSetting;
using TechFix.EntityModels;
using TechFix.Services.ScheduleServices;

namespace TechFix.Services.Common
{
	public class SequenceService
	{
		private DataContext _context;
		private IMapper _mapper;
		private AppSettings _appSettings;
		private readonly ILogger<AutomationServices> _logger;

		public SequenceService(DataContext context, IMapper mapper, IOptions<AppSettings> appSettings, ILogger<AutomationServices> logger)
		{
			_context = context;
			_mapper = mapper;
			_appSettings = appSettings.Value;
			_logger = logger;
		}

        public async Task<string> GetNextProductCode()
        {
            var sequence = await _context.GetNextSequenceValue("ProductCode");
            return $"SP{sequence}";
        }

        public async Task<string> GetNextBillCode()
        {
            var sequence = await _context.GetNextSequenceValue("BillCode");
            return $"PX{sequence}";
        }
    }
	
}