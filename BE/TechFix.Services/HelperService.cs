using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using TechFix.TransportModels.Dtos;

namespace TechFix.Services
{
    public interface IHelperService
    {
        /** Fund **/
        Task<string> GetFundCode(bool isAdd);
        Task<CalculateTotalFundDto> CalculateFund(IQueryable<Fund> queryable);
        /** End Fund **/
        /** Product **/
        List<Product> GetAllProductByFilter(PagingParams param);
        MemoryStream GenerateExcel(List<Product> data);
        Task<bool> ImportExcel(IFormFile formFile, CancellationToken cToken);
        /** End Product **/
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

        /** FUND **/
        public async Task<string> GetFundCode(bool isAdd)
        {
            int nextValue = await _context.GetNextSequenceValue("FundCodeIncrement");
            if (isAdd) return $"TQ{nextValue}";
            return $"CQ{nextValue}";
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
        /** END FUND **/

        /** START PRODUCT **/
        public List<Product> GetAllProductByFilter(PagingParams param)
        {
            var queryable = _context.Products
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            return queryable.ToList();
        }
        public MemoryStream GenerateExcel(List<Product> data)
        {
            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Products");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 2;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "Ten_San_Pham";
                //using (var r = worksheet.Cells["A1:C1"])
                //{
                //    r.Merge = true;
                //    r.Style.Font.Color.SetColor(Color.White);
                //    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                //    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                //}

                worksheet.Cells["B1"].Value = "Ma_San_Pham";
                worksheet.Cells["C1"].Value = "So_Luong";
                worksheet.Cells["D1"].Value = "Don_Vi_Tinh";
                worksheet.Cells["E1"].Value = "Thong_Tin_Them";
                worksheet.Cells["F1"].Value = "Cho_Phep_Ban_Am";
                worksheet.Cells["G1"].Value = "Cho_Phep_Sua_Gia";
                worksheet.Cells["H1"].Value = "Gia_Von";
                worksheet.Cells["I1"].Value = "Gia_Ban_Le";
                worksheet.Cells["J1"].Value = "Gia_Ban_Si";
                worksheet.Cells["K1"].Value = "Danh_Muc";
                worksheet.Cells["L1"].Value = "Nha_San_Xuat";
                //worksheet.Cells["A4:C4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                //worksheet.Cells["A4:C4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                //worksheet.Cells["A4:C4"].Style.Font.Bold = true;

                row = 2;
                foreach (var product in data)
                {
                    worksheet.Cells[row, 1].Value = product.Name;
                    worksheet.Cells[row, 2].Value = product.Code;
                    worksheet.Cells[row, 3].Value = product.Quantity;
                    worksheet.Cells[row, 4].Value = _context.ProductUnits.FirstOrDefault(x => x.Id == product.ProductUnitId)?.Name;
                    worksheet.Cells[row, 5].Value = product.Description;
                    worksheet.Cells[row, 6].Value = product.AllowNegativeSell ? "Có" : "Không";
                    worksheet.Cells[row, 7].Value = product.IsInventoryTracking ? "Có" : "Không";
                    worksheet.Cells[row, 8].Value = product.OriginalCost.Round(0);
                    worksheet.Cells[row, 9].Value = product.SellIn.Round(0);
                    worksheet.Cells[row, 10].Value = product.SellOut.Round(0);
                    worksheet.Cells[row, 11].Value = _context.Categories.FirstOrDefault(x => x.Id == product.CategoryId)?.Name;
                    worksheet.Cells[row, 12].Value = _context.Manufacturers.FirstOrDefault(x => x.Id == product.ManufacturerId)?.Name;
                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Product List";
                xlPackage.Workbook.Properties.Author = "";
                xlPackage.Workbook.Properties.Subject = "Product List";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            return stream;
        }
        public async Task<bool> ImportExcel(IFormFile formFile, CancellationToken cToken)
        {
            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var list = new List<Product>();

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream, cToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        //check if exists will not import the data
                        var code = worksheet.Cells[row, 2].Value.ToString().Trim();
                        bool isExists = _context.Products.FirstOrDefault(x => x.Code.Equals(code)) != null ? true : false;
                        if (isExists) continue;

                        var item = new Product
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            Code = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Quantity = int.Parse(worksheet.Cells[row, 3].Value.ToString().Trim()),
                            Description = worksheet.Cells[row, 5].Value.ToString().Trim(),
                            AllowNegativeSell = worksheet.Cells[row, 6].Value.ToString().Trim() == "Có" ? true : false,
                            IsInventoryTracking = worksheet.Cells[row, 7].Value.ToString().Trim() == "Có" ? true : false,
                            OriginalCost = int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()),
                            SellIn = int.Parse(worksheet.Cells[row, 9].Value.ToString().Trim()),
                            SellOut = int.Parse(worksheet.Cells[row, 10].Value.ToString().Trim())
                        };

                        //add productUnit
                        string productUnitName = worksheet.Cells[row, 4].Value.ToString().Trim();
                        var productUnit = _context.ProductUnits.FirstOrDefault(x => x.Name.Equals(productUnitName));
                        if (productUnit != null) item.ProductUnitId = productUnit.Id;

                        //add category
                        string categoryName = worksheet.Cells[row, 11].Value.ToString().Trim();
                        var category = _context.Categories.FirstOrDefault(x => x.Name.Equals(categoryName));
                        if (category != null) item.CategoryId = category.Id;

                        //add manufacturer
                        string manufacturerName = worksheet.Cells[row, 12].Value.ToString().Trim();
                        var manufacturer = _context.Manufacturers.FirstOrDefault(x => x.Name.Equals(manufacturerName));
                        if (manufacturer != null) item.ManufacturerId = manufacturer.Id;

                        list.Add(item);
                    }
                }
                if (list.Count > 0)
                {
                    _context.Products.AddRange(list);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }
        /** END PRODUCT **/
    }
}
