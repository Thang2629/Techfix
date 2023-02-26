using AuthorizeNet.Api.Contracts.V1;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechFix.Common;
using TechFix.Common.AppSetting;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.Services.EmailServices;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;

namespace TechFix.Services
{
    public interface IHelperService
    {
        Task<bool> Upload(IFormFile file, string path, string oldFileName = "");
        string GetExcelValueByColumnName(ExcelWorksheet ws, string columnName, int rowNumber);
        /** Income Ticket **/
        Task<object> CalculateTotalIncome(IQueryable<IncomeTicket> queryable);
        /** End Income Ticket **/
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
        public string GetExcelValueByColumnName(ExcelWorksheet ws, string columnName, int rowNumber)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            var column = ws.Cells["1:1"].FirstOrDefault(c => c.Value.ToString() == columnName);
            if (column == null) throw new ArgumentNullException(nameof(column));
            int columnNo = column.Start.Column;
            var dataString = ws.Cells[rowNumber, columnNo].Value != null ? ws.Cells[rowNumber, columnNo].Value.ToString().Trim() : string.Empty;
            return dataString;
        }

        public async Task<bool> Upload(IFormFile file, string path, string oldFileName = "")
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                if (Path.GetExtension(file.FileName) == ".xlsx")
                {
                    string oldFilePath = Path.Combine(path, oldFileName);
                    string filePath = Path.Combine(path, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);

                        //remove old file
                        FileInfo fileInfo = new FileInfo(oldFilePath);
                        if (fileInfo.Exists) fileInfo.Delete();

                        return true;
                    }
                }
            }
            return false;
        }

        /** START INCOMETICKET **/
        public async Task<object> CalculateTotalIncome(IQueryable<IncomeTicket> queryable)
        {
            decimal customerTotal = 0, otherTotal = 0, total = 0;
            if(queryable.Count() > 0)
            {
                foreach(var item in queryable)
                {
                    var amount = item.Debt > 0 ? item.Amount - item.Debt : item.Amount;
                    //if(item.ExportId != null)
                    //{
                    //    customerTotal += amount;
                    //}
                    //else
                    //{
                    //    otherTotal += amount;
                    //}
                }

                total = customerTotal + otherTotal;
            }
            return new { Total = total, CustomerTotal = customerTotal, OtherTotal = otherTotal };
        }
        /** END INCOMETICKET **/
    }
}
