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

namespace TechFix.Services
{
    public class FixOrderService
    {
        public readonly IWebHostEnvironment _env;

        public FixOrderService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<FixProduct>> GetRemovedFixProductListItem(List<FixProduct> baseItem, List<FixProductTransport> newItem)
        {
            var result = new List<FixProduct>();
            if (baseItem != null && baseItem.Count > 0 && newItem != null && newItem.Count > 0)
            {
                var removedItem = baseItem.Where(x => !x.IsDeleted && !newItem.Any(y => y.Code == x.Code)).ToList();

                if (removedItem.Count > 0)
                {
                    result.AddRange(removedItem);
                }
            }
            return result;
        }

        public async Task<List<FixProductTransport>> GetAddedFixProductListItem(List<FixProduct> baseItem, List<FixProductTransport> newItem)
        {
            var result = new List<FixProductTransport>();
            if (baseItem != null && baseItem.Count > 0 && newItem != null && newItem.Count > 0)
            {
                var addedItem = newItem.Where(x => !baseItem.Any(y => y.Code == x.Code && !y.IsDeleted)).ToList();

                if (addedItem.Count > 0)
                {
                    result.AddRange(addedItem);
                }
            }
            return result;
        }
    }
}
