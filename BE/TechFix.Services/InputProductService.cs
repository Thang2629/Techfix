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
    public interface IInputProductService
    {
        List<InputProduct> GetAllInputProductByFilter(PagingParams param);
        MemoryStream GenerateExcel(List<InputProduct> data);
    }
    public class InputProductService : IInputProductService
    {
        private DataContext _context;
        private IHelperService _helperService;
        public readonly IWebHostEnvironment _env;

        public InputProductService(IWebHostEnvironment env, DataContext context, IHelperService helperService)
        {
            _env = env;
            _context = context;
            _helperService = helperService;
        }

        public List<InputProduct> GetAllInputProductByFilter(PagingParams param)
        {
            var queryable = _context.InputProducts
                .Include(p => p.InputProductItems)
                .Include(p => p.Supplier)
                .Include(p => p.Store)
                .Include(p => p.User)
                .Include(p => p.PaymentMethod)
                .Where(m => !m.IsDeleted);

            queryable = QueryHelper.ApplyFilter(queryable, param.FilterParams);
            return queryable.ToList();
        }
        public MemoryStream GenerateExcel(List<InputProduct> data)
        {
            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("InputProducts");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 2;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "Ngày nhập";
                worksheet.Cells["B1"].Value = "Mã phiếu nhập";
                worksheet.Cells["C1"].Value = "Kho nhập";
                worksheet.Cells["D1"].Value = "Nhà cung cấp";
                worksheet.Cells["E1"].Value = "Mã sản phẩm";
                worksheet.Cells["F1"].Value = "Tên sản phẩm";
                worksheet.Cells["G1"].Value = "Danh mục";
                worksheet.Cells["H1"].Value = "Người nhập";
                worksheet.Cells["I1"].Value = "Số lượng nhập";
                worksheet.Cells["J1"].Value = "Giá nhập";
                worksheet.Cells["K1"].Value = "ĐVT";
                worksheet.Cells["L1"].Value = "Thành tiền";
                worksheet.Cells["M1"].Value = "Nợ";

                row = 2;
                foreach (var product in data)
                {
                    //each item in excel is one row
                    var listItems = product.InputProductItems;
                    if(listItems.Count > 0)
                    {
                        decimal debt = product.AmountOwed;
                        decimal paid = product.AmountPaid;

                        foreach(var item in listItems)
                        {
                            worksheet.Cells[row, 1].Value = product.InputDate.ToString();
                            worksheet.Cells[row, 2].Value = product.Code ?? string.Empty;
                            worksheet.Cells[row, 3].Value = product.Store.Name ?? string.Empty;
                            worksheet.Cells[row, 4].Value = product.Supplier.Name ?? string.Empty;
                            worksheet.Cells[row, 5].Value = item.Product.Code ?? string.Empty;
                            worksheet.Cells[row, 6].Value = item.Product.Name ?? string.Empty;
                            worksheet.Cells[row, 7].Value = item.Product.Category.Name ?? string.Empty;
                            worksheet.Cells[row, 8].Value = product.User.FullName ?? string.Empty;
                            worksheet.Cells[row, 9].Value = item.Quantity.ToString();
                            worksheet.Cells[row, 10].Value = item.OriginalPrice.ToString();
                            worksheet.Cells[row, 11].Value = item.Product.ProductUnit.Name ?? string.Empty;
                            worksheet.Cells[row, 12].Value = (item.Quantity * item.OriginalPrice).ToString();
                            
                            if(debt != 0)
                            {
                                var remain = paid - (item.Quantity * item.OriginalPrice);
                                if (remain >= 0)
                                {
                                    paid = remain;
                                    worksheet.Cells[row, 13].Value = 0;
                                }
                                else
                                {
                                    worksheet.Cells[row, 13].Value = (-remain).ToString();
                                    paid = 0;
                                }
                            }
                            else
                            {
                                worksheet.Cells[row, 13].Value = 0;
                            }
                            row++;
                        }
                    }
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "InputProduct List";
                xlPackage.Workbook.Properties.Author = "";
                xlPackage.Workbook.Properties.Subject = "InputProduct List";
                // save the new spreadsheet
                xlPackage.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }
}
