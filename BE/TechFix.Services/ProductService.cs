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
using TechFix.TransportModels;

namespace TechFix.Services
{
    public class ProductService
    {
        private DataContext _context;
        private IHelperService _helperService;
        public readonly IWebHostEnvironment _env;

        public ProductService(IWebHostEnvironment env, DataContext context, IHelperService helperService)
        {
            _env = env;
            _context = context;
            _helperService = helperService;
        }

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
                worksheet.Cells["B1"].Value = "Ma_San_Pham";
                worksheet.Cells["C1"].Value = "So_Luong";
                worksheet.Cells["D1"].Value = "Don_Vi_Tinh";
                worksheet.Cells["E1"].Value = "Thong_Tin_Them";
                worksheet.Cells["F1"].Value = "Cho_Phep_Ban_Am";
                worksheet.Cells["G1"].Value = "Cho_Phep_Sua_Gia";
                worksheet.Cells["H1"].Value = "Gia_Nhap";
                worksheet.Cells["I1"].Value = "Gia_Von";
                worksheet.Cells["J1"].Value = "Gia_Web";
                worksheet.Cells["K1"].Value = "Danh_Muc";
                worksheet.Cells["L1"].Value = "Nha_San_Xuat";

                row = 2;
                foreach (var product in data)
                {
                    worksheet.Cells[row, 1].Value = !string.IsNullOrEmpty(product.Name) ? product.Name : string.Empty;
                    worksheet.Cells[row, 2].Value = !string.IsNullOrEmpty(product.Code) ? product.Code : string.Empty;
                    worksheet.Cells[row, 3].Value = product.Quantity;
                    worksheet.Cells[row, 4].Value = _context.ProductUnits.FirstOrDefault(x => x.Id == product.ProductUnitId)?.Name;
                    worksheet.Cells[row, 5].Value = !string.IsNullOrEmpty(product.Description) ? product.Description : string.Empty;
                    worksheet.Cells[row, 6].Value = product.AllowNegativeSell ? 1 : 0;
                    worksheet.Cells[row, 7].Value = product.IsInventoryTracking ? 1 : 0;
                    worksheet.Cells[row, 8].Value = product.OriginalPrice.Round(0);
                    worksheet.Cells[row, 9].Value = product.WebPrice.Round(0);
                    worksheet.Cells[row, 10].Value = product.FakePrice.Round(0);
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
                        //build up data
                        var productName = _helperService.GetExcelValueByColumnName(worksheet, "Ten_San_Pham", row);
                        var productCode = _helperService.GetExcelValueByColumnName(worksheet, "Ma_San_Pham", row);
                        var quantity = _helperService.GetExcelValueByColumnName(worksheet, "So_Luong", row);
                        var productUnitName = _helperService.GetExcelValueByColumnName(worksheet, "Don_Vi_Tinh", row);
                        var description = _helperService.GetExcelValueByColumnName(worksheet, "Thong_Tin_Them", row);
                        var allowNegativeSell = _helperService.GetExcelValueByColumnName(worksheet, "Cho_Phep_Ban_Am", row);
                        var allowChangePrice = _helperService.GetExcelValueByColumnName(worksheet, "Cho_Phep_Sua_Gia", row);
                        var originalPrice = _helperService.GetExcelValueByColumnName(worksheet, "Gia_Nhap", row);
                        var fakePrice = _helperService.GetExcelValueByColumnName(worksheet, "Gia_Von", row);
                        var webPrice = _helperService.GetExcelValueByColumnName(worksheet, "Gia_Web", row);
                        var categoryName = _helperService.GetExcelValueByColumnName(worksheet, "Danh_Muc", row);
                        var manufacturerName = _helperService.GetExcelValueByColumnName(worksheet, "Nha_San_Xuat", row);

                        //model
                        var existItem = _context.Products.FirstOrDefault(x => x.Code.Equals(productCode));
                        var productUnit = _context.ProductUnits.FirstOrDefault(x => x.Name.Equals(productUnitName));
                        var category = _context.Categories.FirstOrDefault(x => x.Name.Equals(categoryName));
                        var manufacturer = _context.Manufacturers.FirstOrDefault(x => x.Name.Equals(manufacturerName));

                        if (existItem != null)
                        {
                            existItem.Name = productName;
                            existItem.Quantity = int.Parse(quantity);
                            existItem.ProductUnitId = productUnit?.Id;
                            existItem.Description = description;
                            existItem.AllowNegativeSell = allowNegativeSell == "1" ? true : false;
                            existItem.IsInventoryTracking = allowChangePrice == "1" ? true : false;
                            existItem.OriginalPrice = decimal.Parse(originalPrice);
                            existItem.FakePrice = decimal.Parse(fakePrice);
                            existItem.WebPrice = decimal.Parse(webPrice);
                            existItem.CategoryId = category?.Id;
                            existItem.ManufacturerId = manufacturer?.Id;

                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var item = new Product
                            {
                                Name = productName,
                                Code = productCode,
                                Quantity = int.Parse(quantity),
                                Description = description,
                                AllowNegativeSell = allowNegativeSell == "1" ? true : false,
                                IsInventoryTracking = allowChangePrice == "1" ? true : false,
                                OriginalPrice = decimal.Parse(originalPrice),
                                WebPrice = decimal.Parse(webPrice),
                                FakePrice = decimal.Parse(fakePrice),
                                ProductUnitId = productUnit?.Id,
                                CategoryId = category?.Id,
                                ManufacturerId = manufacturer?.Id
                            };
                            list.Add(item);
                        }
                    }
                }
                if (list.Count == 0)
                {
                    _context.Products.AddRange(list);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
        }
    }
}
