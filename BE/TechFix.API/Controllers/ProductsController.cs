using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using TechFix.Common.AppSetting;
using TechFix.Common.Helper;
using TechFix.Common.Paging;
using TechFix.EntityModels;
using TechFix.Services.Common;
using TechFix.TransportModels;
using TechFix.TransportModels.Dtos;
using TechFix.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechFix.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomController
    {
        public ProductsController(IMapper mapper, IOptions<AppSettings> appSettings, DataContext context, IWebHostEnvironment env, CommonService commonService) : base(mapper, appSettings, context, env, commonService)
        {
        }

        // GET: api/<ProductsController>
        [HttpPost]
        [Route("get-all")]
        public IActionResult GetAllProducts(PagingParams param)
        {
            var queryable = _context.Products
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            var projectTo = queryable.ProjectTo<ProductDto>(_mapper.ConfigurationProvider);
            var result = PagedList<ProductDto>.ToPagedList(projectTo, param.PageNumber, param.PageSize);
            return Ok(result);
        }

        private List<Product> GetAllProductByFilter(PagingParams param)
        {
            var queryable = _context.Products
                .Where(m => !m.IsDeleted);
            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            return queryable.ToList();
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Route("export")]
        public IActionResult ExportData(PagingParams param)
        {
            var data = GetAllProductByFilter(param);
            if (data.Count > 0)
            {
                var stream = GenerateExcel(data);
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export-" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx");
            }
            return BadRequest();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task Post([FromBody] ProductTransport product)
        {
            _context.Products.Add(new Product()
            {
                Name = product.Name,
                Code = product.Code,
                MinimumNorm = product.MinimumNorm,
                MaximumNorm = product.MaximumNorm,
                Quantity = product.Quantity,
                OriginalCost = product.OriginalCost,
                SellIn = product.SellIn,
                SellOut = product.SellOut,
                Description = product.Description,
                AllowNegativeSell = product.AllowNegativeSell,
                Warranty = product.Warranty,
                IsDeleted = product.IsDeleted,
                CategoryId = product.CategoryId,
                ProductUnitId = product.ProductUnitId,
                ProductConditionId = product.ProductConditionId,
                ManufacturerId = product.ManufacturerId,
                SupplierId = product.SupplierId,
                IsInventoryTracking = product.IsInventoryTracking,
                StoreId = product.StoreId,
            });
            await _context.SaveChangesAsync();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] ProductTransport product)
        {
            var model = await _context.Products.FindAsync(id);
            if (model != null)
            {
                model.Name = product.Name;
                model.Code = product.Code;
                model.MinimumNorm = product.MinimumNorm;
                model.MaximumNorm = product.MaximumNorm;
                model.Quantity = product.Quantity;
                model.OriginalCost = product.OriginalCost;
                model.SellIn = product.SellIn;
                model.SellOut = product.SellOut;
                model.Description = product.Description;
                model.AllowNegativeSell = product.AllowNegativeSell;
                model.Warranty = product.Warranty;
                model.IsDeleted = product.IsDeleted;
                model.CategoryId = product.CategoryId;
                model.ProductUnitId = product.ProductUnitId;
                model.ProductConditionId = product.ProductConditionId;
                model.ManufacturerId = product.ManufacturerId;
                model.SupplierId = product.SupplierId;
                model.IsInventoryTracking = product.IsInventoryTracking;
                model.StoreId = product.StoreId;

                await _context.SaveChangesAsync();
            }
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        //Helpers
        private MemoryStream GenerateExcel(List<Product> data)
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
    }
    
}
