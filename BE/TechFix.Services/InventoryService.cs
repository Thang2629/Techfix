using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

namespace TechFix.Services
{
    public interface IInventoryService
    {
        List<Product> GetAllInventoryByFilter(PagingParams param);
        MemoryStream GenerateExcel(List<Product> data);
    }
    public class InventoryService : IInventoryService
    {
        private DataContext _context;
        private IHelperService _helperService;
        public readonly IWebHostEnvironment _env;

        public InventoryService(IWebHostEnvironment env, DataContext context, IHelperService helperService)
        {
            _env = env;
            _context = context;
            _helperService = helperService;
        }

        public List<Product> GetAllInventoryByFilter(PagingParams param)
        {
            var queryable = _context.Products
                .Include(p => p.Supplier)
                .Where(x => !x.IsDeleted && x.IsInventoryTracking)
                .AsNoTracking();

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            return queryable.ToList();
        }
        public MemoryStream GenerateExcel(List<Product> data)
        {
            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Inventory");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 2;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "Mã sản phẩm";
                worksheet.Cells["B1"].Value = "Tên sản phẩm";
                worksheet.Cells["C1"].Value = "Nhà cung cấp";
                worksheet.Cells["D1"].Value = "Số lượng";
                worksheet.Cells["E1"].Value = "Vốn tồn kho";
                worksheet.Cells["F1"].Value = "Giá trị tồn";

                row = 2;
                foreach (var product in data)
                {
                    worksheet.Cells[row, 1].Value = product.Code;
                    worksheet.Cells[row, 2].Value = product.Name;
                    worksheet.Cells[row, 3].Value = product.Supplier?.Name;
                    worksheet.Cells[row, 4].Value = product.Quantity;
                    worksheet.Cells[row, 5].Value = (product.OriginalPrice * product.Quantity).Round(0).ToString();
                    worksheet.Cells[row, 6].Value = (product.WebPrice * product.Quantity).Round(0).ToString();
                    
                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Inventory List";
                xlPackage.Workbook.Properties.Author = "";
                xlPackage.Workbook.Properties.Subject = "Inventory List";
                // save the new spreadsheet
                xlPackage.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }
}
