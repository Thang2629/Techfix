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
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;

namespace TechFix.Services
{
    public class FundService
    {
        public readonly IWebHostEnvironment _env;

        public FundService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<CalculateTotalFundDto> CalculateFund(IQueryable<Fund> queryable)
        {
            CalculateTotalFundDto result = new CalculateTotalFundDto();
            if (queryable != null)
            {
                foreach (var item in queryable)
                {
                    if (item.IsAdd)
                    {
                        result.PositiveFund += item.Amount;
                    }
                    else
                    {
                        result.NegativeFund += item.Amount;
                    }
                }
            }

            return result;
        }
    }
}
